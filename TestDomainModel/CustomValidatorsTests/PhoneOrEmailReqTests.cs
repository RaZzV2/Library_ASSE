// <copyright file="PhoneOrEmailReqTests.cs" company="Transilvania University of Brasov">
// Dragomir Razvan
// </copyright>

namespace TestDomainModel.CustomValidatorsTests
{
    using System.ComponentModel.DataAnnotations;
    using DomainModel;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test class for validating the PhoneOrEmailReq custom validator.
    /// </summary>
    [TestClass]
    public class PhoneOrEmailReqTests
    {
        /// <summary>
        /// Validates that either phone number or email address is provided, and returns success.
        /// </summary>
        [TestMethod]
        public void EitherPhoneNumberOrEmailAddressIsProvided_ReturnsSuccess()
        {
            var reader = new Reader { PhoneNumber = "123456789", EmailAddress = string.Empty };
            var validator = new PhoneOrEmailReq();

            var validationResult = validator.GetValidationResult(reader, new ValidationContext(reader));

            Assert.AreEqual(ValidationResult.Success, validationResult);
        }

        /// <summary>
        /// Validates that neither phone number nor email address is provided, and returns an error.
        /// </summary>
        [TestMethod]
        public void NeitherPhoneNumberNorEmailAddressIsProvided_ReturnsError()
        {
            var reader = new Reader { PhoneNumber = string.Empty, EmailAddress = string.Empty };
            var validator = new PhoneOrEmailReq();

            var validationResult = validator.GetValidationResult(reader, new ValidationContext(reader));

            Assert.AreNotEqual(ValidationResult.Success, validationResult);
            Assert.AreEqual("Either phone number or email address is required.", validationResult.ErrorMessage);
        }

        /// <summary>
        /// Validates that both phone number and email address are provided, and returns success.
        /// </summary>
        [TestMethod]
        public void BothPhoneNumberAndEmailAddressAreProvided_ReturnsSuccess()
        {
            var reader = new Reader { PhoneNumber = "123456789", EmailAddress = "test@example.com" };
            var validator = new PhoneOrEmailReq();

            var validationResult = validator.GetValidationResult(reader, new ValidationContext(reader));

            Assert.AreEqual(ValidationResult.Success, validationResult);
        }

        /// <summary>
        /// Validates that phone number is provided, and email address is empty, and returns success.
        /// </summary>
        [TestMethod]
        public void PhoneNumberIsProvidedAndEmailAddressIsEmpty_ReturnsSuccess()
        {
            var reader = new Reader { PhoneNumber = "123456789", EmailAddress = string.Empty };
            var validator = new PhoneOrEmailReq();

            var validationResult = validator.GetValidationResult(reader, new ValidationContext(reader));

            Assert.AreEqual(ValidationResult.Success, validationResult);
        }

        /// <summary>
        /// Validates that phone number is empty, and email address is provided, and returns success.
        /// </summary>
        [TestMethod]
        public void PhoneNumberIsEmptyAndEmailAddressIsProvided_ReturnsSuccess()
        {
            var reader = new Reader { PhoneNumber = string.Empty, EmailAddress = "test@example.com" };
            var validator = new PhoneOrEmailReq();

            var validationResult = validator.GetValidationResult(reader, new ValidationContext(reader));

            Assert.AreEqual(ValidationResult.Success, validationResult);
        }

        /// <summary>
        /// Validates that phone number contains whitespace, and email address is provided, and returns success.
        /// </summary>
        [TestMethod]
        public void PhoneNumberContainsWhitespaceAndEmailAddressIsProvided_ReturnsSuccess()
        {
            var reader = new Reader { PhoneNumber = " 123 456 789 ", EmailAddress = "test@example.com" };
            var validator = new PhoneOrEmailReq();

            var validationResult = validator.GetValidationResult(reader, new ValidationContext(reader));

            Assert.AreEqual(ValidationResult.Success, validationResult);
        }

        /// <summary>
        /// Validates that both phone number and email address contain whitespace, and returns an error.
        /// </summary>
        [TestMethod]
        public void BothPhoneNumberAndEmailAddressContainWhitespace_ReturnsError()
        {
            var reader = new Reader { PhoneNumber = "  ", EmailAddress = "  " };
            var validator = new PhoneOrEmailReq();

            var validationResult = validator.GetValidationResult(reader, new ValidationContext(reader));

            Assert.AreNotEqual(ValidationResult.Success, validationResult);
            Assert.AreEqual("Either phone number or email address is required.", validationResult.ErrorMessage);
        }

        /// <summary>
        /// Validates that phone number and email address contain special characters, and returns success.
        /// </summary>
        [TestMethod]
        public void PhoneNumberAndEmailAddressContainSpecialCharacters_ReturnsSuccess()
        {
            var reader = new Reader { PhoneNumber = "+1 (123) 456-7890", EmailAddress = "test+user@example.com" };
            var validator = new PhoneOrEmailReq();

            var validationResult = validator.GetValidationResult(reader, new ValidationContext(reader));

            Assert.AreEqual(ValidationResult.Success, validationResult);
        }

        /// <summary>
        /// Validates that phone number is null, and email address is provided, and returns success.
        /// </summary>
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
