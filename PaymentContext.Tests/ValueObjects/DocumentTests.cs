using PaymentContext.Domain;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentContext.Tests.Entities
{
    [TestClass]
    public class DocumentTests
    {
        // Red, Green, Refactor

        [TestMethod] // Assinatura invalida
        public void ShouldReturnErrorWhenCNPJIsInvalid()
        {
            var doc = new Document("123", EDocumentType.CNPJ);
            Assert.IsTrue(doc.Invalid);
        }

        [TestMethod] // Assinatura Valida
        public void ShouldReturnSucessWhenCNPJIsValid()
        {
            var doc = new Document("78777667000192",EDocumentType.CNPJ);

            Assert.IsTrue(doc.Valid);
        }

        [TestMethod] // Assinatura invalida
        public void ShouldReturnErrorWhenCPFIsInvalid()
        {
            var doc = new Document("123", EDocumentType.CPF);
            Assert.IsTrue(doc.Invalid);
        }

        // Testando mais de um cpf
        [TestMethod] 
        [DataTestMethod]
        [DataRow("62079893092")]
        [DataRow("39240969020")]
        [DataRow("18374763060")]
        public void ShouldReturnSucessWhenCPFIsValid(string cpf)
        {
            var doc = new Document(cpf, EDocumentType.CPF);
            Assert.IsTrue(doc.Valid);
        }
    }
}
