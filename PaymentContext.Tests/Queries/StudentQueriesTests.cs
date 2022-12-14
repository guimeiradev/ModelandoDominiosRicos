using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Entities.ValueObjects;
using PaymentContext.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentContext.Tests.Queries
{
    public class StudentQueriesTests
    {
        // Red, Green, Refactor
        private IList<Student> _students;

        public StudentQueriesTests()
        {
           for(var i = 0; i <= 10; i++)
            {
                _students.Add(new Student(
                    new Name("Aluno", i.ToString()),
                    new Document("111111111" + i.ToString(), Domain.EDocumentType.CPF),
                    new Email(i.ToString() + "@balta.io")
                    )); 
            }
        }

        [TestMethod] // Assinatura invalida
        public void ShouldReturnNullWhenDocumentNotExists()
        {
            var exp = StudentQueries.GetStudentInfo("12345678911");

            var studn = _students.AsQueryable().Where(exp).FirstOrDefault();

            Assert.AreEqual(null, studn);
        }

        [TestMethod] // Assinatura invalida
        public void ShouldReturnNullWhenDocumentExists()
        {
            var exp = StudentQueries.GetStudentInfo("1111111111");

            var studn = _students.AsQueryable().Where(exp).FirstOrDefault();

            Assert.AreNotEqual(null, studn);
        }
    }
}
