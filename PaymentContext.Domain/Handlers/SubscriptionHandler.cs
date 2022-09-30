using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Entities.ValueObjects;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler :
        Notifiable,
        IHandler<CreateBoletoSubscriptionCommand>,
        IHandler<CreatePaypalSubscritptionCommand>
    {
        private readonly IStudentRepository _repository;
        private readonly IEmailService _emailService;

        public SubscriptionHandler(IStudentRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }

        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {

            // Fail Fast Validations
            command.Validate();

            if (command.Invalid)
            {
                AddNotifications(command);
                return  new CommandResult(false, "Não foi possível realizar sua assinatura.");
            }
               
            // Verificar se Documento já está cadastrado.

            if (_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF já está em uso.");

            // Verificar se E-mail já está cadastrado.
            if (_repository.DocumentExists(command.Email))
                AddNotification("Email", "Este E-mail já está em uso.");
            // Gerar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);
           ;
            // Só muda a implementação do pagamento
            var student  = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new BoletoPayment(command.BarCode,
                command.BoletoNumber,
                email,command.PaidDate,
                command.ExpireDate,
                command.Total,
                command.TotalPaid,
                address,
                new Document(command.PayerDocument,command.PayerDocumentType),
                command.Payer);

            // Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // Agrupar as Validações
            AddNotifications(name,document, email,address,student,subscription,payment);

            // Salvar as informações 
            _repository.CreateSubscription(student);
            // Enviar E-mail de boas vindas
            _emailService.Send(student.Name.LastName.ToString(), student.Email.Address, "Bem-Vindo ao Balta.io", "Sua assinatura foi criada");
            // Retornar informações

            return new CommandResult(true, "Assinatura realizada com Sucesso !");
        }

        public ICommandResult Handle(CreatePaypalSubscritptionCommand command)
        {
            // Verificar se Documento já está cadastrado
            if (_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF já está em uso");

            // Verificar se E-mail já está cadastrado
            if (_repository.EmailExists(command.Email))
                AddNotification("Email", "Este E-mail já está em uso");

            // Gerar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);

            // Gerar as Entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            // Só muda a implementação do Pagamento
            var payment = new PayPalPayment(
                 
                 command.TransactionCode,
                 command.PaidDate,
                 command.ExpireDate,
                 command.Total,
                 command.TotalPaid,
                 address,
                 new Document(command.PayerDocument, command.PayerDocumentType),
                 command.Payer,
                 email);


            // Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // Agrupar as Validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            // Salvar as Informações
            _repository.CreateSubscription(student);

            // Enviar E-mail de boas vindas
            _emailService.Send(student.Name.ToString(), student.Email.Address, "bem vindo ao balta.io", "Sua assinatura foi criada");

            // Retornar informações
            return new CommandResult(true, "Assinatura realizada com sucesso");
        }
    }
}
