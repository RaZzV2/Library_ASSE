using DomainModel;
using DomainModel.CustomValidationHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TestDomainModel
{
   
    [TestClass]
    public class ReaderTests
    {
        private Reader reader;
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
        public void ReaderFirstNameCorrect()
        {
            this.reader.ReaderFirstName = "Razvan";
            try
            {
                Validator.ValidateObject(this.reader, CreateValidationContext(this.reader), true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }
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
        public void ReaderLastNameCorrect()
        {
            this.reader.ReaderLastName = "Dragomir";
            try
            {
                Validator.ValidateObject(this.reader, CreateValidationContext(this.reader), true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }
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
        public void CorrectEmailAddress()
        {
            this.reader.EmailAddress = "d_raszvan01@yahoo.com";
            try
            {
                Validator.ValidateObject(this.reader, CreateValidationContext(this.reader), true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }
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
        public void CorrectPhoneNumber()
        {
            this.reader.PhoneNumber = "0238432132";
            try
            {
                Validator.ValidateObject(this.reader, CreateValidationContext(this.reader), true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }
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
