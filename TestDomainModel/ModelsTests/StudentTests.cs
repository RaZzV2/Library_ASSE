using DomainModel;
using DomainModel.CustomValidationHelpers;
using Library.models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks.Constraints;
using System;
using System.ComponentModel.DataAnnotations;

namespace TestDomainModel.ModelsTests
{
    [TestClass]
    public class StudentTests
    {
        private Student student;

        private ValidationContext CreateValidationContext(object instance)
        {
            return new ValidationContext(instance, null, null);
        }
        private void AssertValidationException<T>(T instance, string expectedErrorMessage)
        {
            ModelValidationHelper.AssertValidationException(instance, expectedErrorMessage);
        }

        [TestInitialize]
        public void SetUp()
        {
            this.student = new Student
            {
                FirstName = "Razvan",
                LastName = "Teste",
                Age = 33,

            };
        }


        [TestMethod]
        public void StudentAgeTooLow()
        {
            this.student.Age = 3;
            AssertValidationException(this.student, "You must have at least 7 and at most 99 years to borrow a book!");
        }

        [TestMethod]
        public void StudentAgeTooHigh()
        {
            this.student.Age = 101;
            AssertValidationException(this.student, "You must have at least 7 and at most 99 years to borrow a book!");
        }

        [TestMethod]
        public void StudentAgeCorrect()
        {
            this.student.Age = 32;
            try
            {
                Validator.ValidateObject(this.student, CreateValidationContext(this.student), true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }
        }

        [TestMethod]
        public void StudentFirstNameIsEmpty()
        {
            this.student.FirstName = string.Empty;
            AssertValidationException(this.student, "First name is required!");
        }

        [TestMethod]
        public void StudentFirstNameContainsSpecialCharacters()
        {
            this.student.FirstName = "Test@_+";
            AssertValidationException(this.student, "First name must not have special characters!");
        }

        [TestMethod]
        public void StudentFirstNameTooShort()
        {
            this.student.FirstName = "ads";
            AssertValidationException(this.student, "First name must have at least 4 characters!");
        }

        [TestMethod]
        public void StudentLastNameIsEmpty()
        {
            this.student.LastName = string.Empty;
            AssertValidationException(this.student, "Last name is required!");
        }

        [TestMethod]
        public void StudentLastNameContainsSpecialCharacters()
        {
            this.student.LastName = "Test@_+";
            AssertValidationException(this.student, "Last name must not have special characters!");
        }

        [TestMethod]
        public void StudentLastNameTooShort()
        {
            this.student.LastName = "ads";
            AssertValidationException(this.student, "Last name must have at least 4 characters!");
        }

        [TestMethod]
        public void StudentNegativeAge()
        {
            this.student.Age = -1;
            AssertValidationException(this.student, "You must have at least 7 and at most 99 years to borrow a book!");
        }
    }
}
