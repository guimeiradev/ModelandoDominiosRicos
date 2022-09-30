using PaymentContext.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentContext.Tests.Command
{
    public class CreateBoletoSubscriptionCommandTests
    {
        [TestMethod] // Assinatura invalida
        public void ShouldReturnErrorWhenNameIsInvalid()
        {
            var command = new CreateBoletoSubscriptionCommand();
            command.FirstName = "";

            command.Validate();

            Assert.AreEqual(false, command.Valid);
        }
    }
}
