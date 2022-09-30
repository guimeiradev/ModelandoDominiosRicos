using Flunt.Validations;
using PaymentContext.Domain.Entities.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities
{
    public class Student : Entity
    {
        private IList<Subscription> _subscriptions;
        public Student(Name name, Document document, Email email)
        {
            Name = name;
            Document = document;
            Email = email;
            _subscriptions = new List<Subscription>();

            AddNotifications(name, document, email);
        }

        public Name Name { get; private set; }
        public Name LastName { get; private set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public Address Address { get; private set; }
        public IReadOnlyCollection<Subscription> Subscriptions { get { return _subscriptions.ToArray(); } }


        public void AddSubscription(Subscription subscription)
        {
            var hasSubscriptionActive = false;

            foreach (var sub in _subscriptions)
            {
                if (sub.Active)
                    hasSubscriptionActive = true;


                // Uma op��o como os outros 
                AddNotifications(new Contract()
                    .Requires()
                    .IsFalse(hasSubscriptionActive, "Student.Subscriptions", "Voc� j� tem um assinatura ativa")
                    .AreNotEquals(0,subscription.Payments.Count,"Student.Subscription.Payments","Esta assinatura n�o possui pagamentos.")
                    );

                // OU

                //if (hasSubscriptionActive)
                //    AddNotification("Student.Subscriptions", "Voc� j� tem um assinatura ativa");
            }
        }
    }
}