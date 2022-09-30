using PaymentContext.Domain.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentContext.Domain.Entities
{
    public class BoletoPayment : Payment
    {
        public BoletoPayment(string boletoNumber,
            string barCode,
            Email email, 
            DateTime paidDate,
            DateTime expireDate,
            decimal total,
            decimal totalPaid,
            Address address, 
            Document document,
            string payer) : base(
                paidDate,
                expireDate,
                total,
                totalPaid,
                address,
                document,
                payer,
                email)
        {
            BoletoNumber = boletoNumber;
            BarCode = barCode;
        }

        public string BoletoNumber { get; private set; }
        public string BarCode { get; private set; }
    }
}
