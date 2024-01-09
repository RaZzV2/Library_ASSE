using DomainModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TestDomainModel.CustomValidatorsTests
{
    [TestClass]
    public class PhoneOrEmailReqTests
    {
        [TestMethod]
        public void EitherPhoneNumberOrEmailAddressIsProvided_ReturnsSuccess()
        {
            var reader = new Reader { PhoneNumber = "123456789", EmailAddress = "" };
            var validator = new PhoneOrEmailReq();

            var validationResult = validator.GetValidationResult(reader, new ValidationContext(reader));

            Assert.AreEqual(ValidationResult.Success, validationResult);
        }

        [TestMethod]
        public void NeitherPhoneNumberNorEmailAddressIsProvided_ReturnsError()
        {
            var reader = new Reader { PhoneNumber = "", EmailAddress = "" };
            var validator = new PhoneOrEmailReq();

            var validationResult = validator.GetValidationResult(reader, new ValidationContext(reader));

            Assert.AreNotEqual(ValidationResult.Success, validationResult);
            Assert.AreEqual("Either phone number or email address is required.", validationResult.ErrorMessage);
        }

        [TestMethod]
        public void BothPhoneNumberAndEmailAddressAreProvided_ReturnsSuccess()
        {
            var reader = new Reader { PhoneNumber = "123456789", EmailAddress = "test@example.com" };
            var validator = new PhoneOrEmailReq();

            var validationResult = validator.GetValidationResult(reader, new ValidationContext(reader));

            Assert.AreEqual(ValidationResult.Success, validationResult);
        }

        [TestMethod]
        public void PhoneNumberIsProvidedAndEmailAddressIsEmpty_ReturnsSuccess()
        { 
            var reader = new Reader { PhoneNumber = "123456789", EmailAddress = "" };
            var validator = new PhoneOrEmailReq();

            var validationResult = validator.GetValidationResult(reader, new ValidationContext(reader));

            Assert.AreEqual(ValidationResult.Success, validationResult);
        }

        [TestMethod]
        public void PhoneNumberIsEmptyAndEmailAddressIsProvided_ReturnsSuccess()
        {
            var reader = new Reader { PhoneNumber = "", EmailAddress = "test@example.com" };
            var validator = new PhoneOrEmailReq();

            var validationResult = validator.GetValidationResult(reader, new ValidationContext(reader));

            Assert.AreEqual(ValidationResult.Success, validationResult);
        }

        [TestMethod]
        public void PhoneNumberContainsWhitespaceAndEmailAddressIsProvided_ReturnsSuccess()
        {
            var reader = new Reader { PhoneNumber = " 123 456 789 ", EmailAddress = "test@example.com" };
            var validator = new PhoneOrEmailReq();

            var validationResult = validator.GetValidationResult(reader, new ValidationContext(reader));

            Assert.AreEqual(ValidationResult.Success, validationResult);
        }

        [TestMethod]
        public void BothPhoneNumberAndEmailAddressContainWhitespace_ReturnsError()
        {
            var reader = new Reader { PhoneNumber = "  ", EmailAddress = "  " };
            var validator = new PhoneOrEmailReq();

            var validationResult = validator.GetValidationResult(reader, new ValidationContext(reader));

            Assert.AreNotEqual(ValidationResult.Success, validationResult);
            Assert.AreEqual("Either phone number or email address is required.", validationResult.ErrorMessage);
        }

        [TestMethod]
        public void PhoneNumberAndEmailAddressContainSpecialCharacters_ReturnsSuccess()
        {
            var reader = new Reader { PhoneNumber = "+1 (123) 456-7890", EmailAddress = "test+user@example.com" };
            var validator = new PhoneOrEmailReq();

            var validationResult = validator.GetValidationResult(reader, new ValidationContext(reader));

            Assert.AreEqual(ValidationResult.Success, validationResult);
        }

        [TestMethod]
        public void PhoneNumberIsNullAndEmailAddressIsProvided_ReturnsSuccess()
        {
            var reader = new Reader { PhoneNumber = null, EmailAddress = "test@example.com" };
            var validator = new PhoneOrEmailReq();

            var validationResult = validator.GetValidationResult(reader, new ValidationContext(reader));

            Assert.AreEqual(ValidationResult.Success, validationResult);
        }
    }
}
