using DomainModel;
using Library.models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks.Constraints;
using System;
using System.ComponentModel.DataAnnotations;

namespace TestDomainModel
{
    [TestClass]
    public class UnitTest1
    {

        [TestClass]
        public class ProductTests
        {
            private Student student;
            private Reader reader;

            private ValidationContext CreateValidationContext(object instance)
            {
                return new ValidationContext(instance, null, null);
            }
            private void AssertValidationException<T>(T instance, string expectedErrorMessage)
            {
                var validationContext = CreateValidationContext(instance);
                var exception = Assert.ThrowsException<ValidationException>(() =>
                    Validator.ValidateObject(instance, validationContext, true));
                Assert.AreEqual(expectedErrorMessage, exception.Message);
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

                this.reader = new Reader
                {
                    ReaderFirstName = "Ionel",
                    ReaderLastName = "Mirel",
                    Address = "Strada x Da",
                    EmailAddress = "razzkk@gmail.com",
                    Role = true,
                    PhoneNumber = "0732139910"
                };
            }


            [TestMethod]
            public void StudentAgeToLow()
            {
                this.student.Age = 3;
                AssertValidationException(this.student, "You must have at least 7 and at most 99 years to borrow a book!");
            }

            [TestMethod]
            public void StudentAgeToHigh()
            {
                this.student.Age = 101;
                AssertValidationException(this.student, "You must have at least 7 and at most 99 years to borrow a book!");
            }

            [TestMethod]
            public void StudentAgeCorrect()
            {
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
            public void ReaderFirstNameIsEmpty()
            {
                this.reader.ReaderFirstName = string.Empty;
                AssertValidationException(this.reader, "First name is required!");
            }

            [TestMethod]
            public void ReaderFirstNameContainsSpecialCharacters()
            {
                this.reader.ReaderFirstName = "12312@@@";
                AssertValidationException(this.reader, "First name must not have special characters!");
            }

            [TestMethod]
            public void ReaderFirstNameTooShort()
            {
                this.reader.ReaderFirstName = "rzz";
                AssertValidationException(this.reader, "First name must have at least 4 characters!");
            }

            [TestMethod]
            public void ReaderLastNameIsEmpty()
            {
                this.reader.ReaderLastName = string.Empty;
                AssertValidationException(this.reader, "Last name is required!");
            }

            [TestMethod]
            public void ReaderLastNameTooShort()
            {
                this.reader.ReaderLastName = "abc";
                AssertValidationException(this.reader, "Last name must have at least 4 characters!");
            }

            [TestMethod]
            public void ReaderLastNameContainsSpecialCharacters()
            {
                this.reader.ReaderLastName = "Special@Name";
                AssertValidationException(this.reader, "Last name must not have special characters!");
            }

            [TestMethod]
            public void AddressIsRequired()
            {
                this.reader.Address = null;
                AssertValidationException(this.reader, "Address is required!");
            }


            [TestMethod]
            public void InvalidEmailAddress()
            {
                this.reader.EmailAddress = "invalid_email";
                AssertValidationException(this.reader, "Invalid Email Address");
            }

            [TestMethod]
            public void SetRoleToTrue()
            {
                this.reader.Role = true;
                Assert.IsTrue(this.reader.Role);
            }

            [TestMethod]
            public void SetRoleToFalse()
            {
                this.reader.Role = false;
                Assert.IsFalse(this.reader.Role);
            }

          

            [TestMethod]
            public void InvalidPhoneNumber()
            {
                this.reader.PhoneNumber = "invalid_phone";
                AssertValidationException(this.reader, "Invalid Phone Number!");
            }

            [TestMethod]
            public void ReaderPhoneNumberOrEmailEmpty()
            {
                this.reader.PhoneNumber = string.Empty;
                this.reader.EmailAddress = string.Empty;
                var validationContext = new ValidationContext(reader, null, null);
                var validationResult = new PhoneOrEmailReq().GetValidationResult(reader, validationContext);
                Assert.IsNotNull(validationResult);
                Assert.AreEqual("Either phone number or email address is required.", validationResult.ErrorMessage);
            }

            [TestMethod]
            public void ReaderEmailEmptyPhoneNumberCorrect()
            {
                this.reader.PhoneNumber = "0238413091";

                try
                {
                    Validator.ValidateObject(this.reader, CreateValidationContext(this.reader), true);
                }
                catch (ValidationException ex)
                {
                    Assert.Fail($"An exception has been thrown!: {ex.Message}");
                }

                this.reader.EmailAddress = string.Empty;

                var validationContext = new ValidationContext(reader, null, null);
                var validationResult = new PhoneOrEmailReq().GetValidationResult(reader, validationContext);

                Assert.IsNull(validationResult);
                Assert.AreEqual(ValidationResult.Success, validationResult);
            }

            [TestMethod]

            public void ReaderPhoneNumberEmptyEmailCorrect()
            {
                this.reader.EmailAddress = "test@gmail.com";

                try
                {
                    Validator.ValidateObject(this.reader, CreateValidationContext(this.reader), true);
                }
                catch (ValidationException ex)
                {
                    Assert.Fail($"An exception has been thrown!: {ex.Message}");
                }

                this.reader.PhoneNumber = string.Empty;

                var validationContext = new ValidationContext(reader, null, null);
                var validationResult = new PhoneOrEmailReq().GetValidationResult(reader, validationContext);

                Assert.IsNull(validationResult);
                Assert.AreEqual(ValidationResult.Success, validationResult);
            }

            [TestMethod]
            public void ReaderPhoneNumberEmptyEmailIncorrect()
            {
                this.reader.EmailAddress = "email_gresit";

                AssertValidationException(this.reader, "Invalid Email Address");

                this.reader.PhoneNumber = string.Empty;

                var validationContext = new ValidationContext(reader, null, null);
                var validationResult = new PhoneOrEmailReq().GetValidationResult(reader, validationContext);

                Assert.IsNull(validationResult);
                Assert.AreEqual(ValidationResult.Success, validationResult);
            }

            [TestMethod]
            public void ReaderEmailEmptyPhoneNumberIncorrect()
            {
                this.reader.PhoneNumber = "numar_gresit";

                AssertValidationException(this.reader, "Invalid Phone Number!");

                this.reader.EmailAddress = string.Empty;

                var validationContext = new ValidationContext(reader, null, null);
                var validationResult = new PhoneOrEmailReq().GetValidationResult(reader, validationContext);

                Assert.IsNull(validationResult);
                Assert.AreEqual(ValidationResult.Success, validationResult);
            }
        }
    }
}
