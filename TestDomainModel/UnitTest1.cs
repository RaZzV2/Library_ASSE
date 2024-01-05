using Library.models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ComponentModel.DataAnnotations;

namespace TestDomainModel
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void StudentAgeToLow()
        {
            var user = new Student
            {
                FirstName = "Test",
                LastName = "Testare",
                Age = 3,
            };
            var validationContext = new ValidationContext(user, null, null);
            var exception = Assert.ThrowsException<ValidationException>(() => Validator.ValidateObject(user, validationContext, true));
            Assert.AreEqual("You must have at least 7 and at most 99 years to borrow a book!", exception.Message);
        }

        [TestMethod]
        public void StudentAgeToHigh()
        {
            var user = new Student
            {
                FirstName = "Test",
                LastName = "Testare",
                Age = 101,
            };
            var validationContext = new ValidationContext(user, null, null);
            var exception = Assert.ThrowsException<ValidationException>(() => Validator.ValidateObject(user, validationContext, true));
            Assert.AreEqual("You must have at least 7 and at most 99 years to borrow a book!", exception.Message);
        }

        [TestMethod]
        public void StudentAgeCorrect()
        {
            var user = new Student
            {
                FirstName = "Test",
                LastName = "Testare",
                Age = 33,
            };
            var validationContext = new ValidationContext(user, null, null);
            try
            {
                Validator.ValidateObject(user, validationContext, true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }
        }
    }
}
