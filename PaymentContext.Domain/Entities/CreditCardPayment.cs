using PaymentContext.Domain.Entities.ValueObjects;

namespace PaymentContext.Domain.Entities
{
    public class CreditCardPayment : Payment
    {
        public CreditCardPayment(string cardNumber,
            string cardHolderName,
            string lastTrasactionNumber,
            DateTime paidDate,
            DateTime expireDate,
            decimal total,
            decimal totalPaid,
            Address address, 
            Document document,
            string payer,
            Email email) : base(
                paidDate,
                expireDate,
                total,
                totalPaid,
                address,
                document,
                payer,
                email)
        {
            CardNumber = cardNumber;
            CardHolderName = cardHolderName;
            LastTrasactionNumber = lastTrasactionNumber;
        }

        public string CardNumber { get; private set; }
        public string CardHolderName { get; private set; }
        public string LastTrasactionNumber { get; private set; }
    }
}
