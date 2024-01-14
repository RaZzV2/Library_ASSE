namespace TestDomainModel.ModelsTests
{
    using System.ComponentModel.DataAnnotations;
    using DomainModel;
    using DomainModel.CustomValidationHelpers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test class for validating the Reader model.
    /// </summary>
    [TestClass]
    public class ReaderTests
    {
        private Reader reader;

        /// <summary>
        /// Set up method to initialize common objects for tests.
        /// </summary>
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
                PhoneNumber = "0732139910",
            };
        }

        /// <summary>
        /// Validates that an empty first name returns an error.
        /// </summary>
        [TestMethod]
        public void ReaderFirstNameIsEmpty()
        {
            this.reader.ReaderFirstName = string.Empty;
            this.AssertValidationException(this.reader, "First name is required!");
        }

        /// <summary>
        /// Validates that a first name containing special characters returns an error.
        /// </summary>
        [TestMethod]
        public void ReaderFirstNameContainsSpecialCharacters()
        {
            this.reader.ReaderFirstName = "12312@@@";
            this.AssertValidationException(this.reader, "First name must not have special characters!");
        }

        /// <summary>
        /// Validates that a short first name returns an error.
        /// </summary>
        [TestMethod]
        public void ReaderFirstNameTooShort()
        {
            this.reader.ReaderFirstName = "rzz";
            this.AssertValidationException(this.reader, "First name must have at least 4 characters!");
        }

        /// <summary>
        /// Validates that a correct first name passes validation.
        /// </summary>
        [TestMethod]
        public void ReaderFirstNameCorrect()
        {
            this.reader.ReaderFirstName = "Razvan";
            try
            {
                Validator.ValidateObject(this.reader, this.CreateValidationContext(this.reader), true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }
        }

        /// <summary>
        /// Validates that a incorrect last name returns an error.
        /// </summary>
        [TestMethod]
        public void ReaderLastNameIsEmpty()
        {
            this.reader.ReaderLastName = string.Empty;
            this.AssertValidationException(this.reader, "Last name is required!");
        }

        /// <summary>
        /// Validates that a short last name returns an error.
        /// </summary>
        [TestMethod]
        public void ReaderLastNameTooShort()
        {
            this.reader.ReaderLastName = "abc";
            this.AssertValidationException(this.reader, "Last name must have at least 4 characters!");
        }

        /// <summary>
        /// Validates that a last name containing special characters returns an error.
        /// </summary>
        [TestMethod]
        public void ReaderLastNameContainsSpecialCharacters()
        {
            this.reader.ReaderLastName = "Special@Name";
            this.AssertValidationException(this.reader, "Last name must not have special characters!");
        }

        /// <summary>
        /// Validates that a correct last name passes validation.
        /// </summary>
        [TestMethod]
        public void ReaderLastNameCorrect()
        {
            this.reader.ReaderLastName = "Dragomir";
            try
            {
                Validator.ValidateObject(this.reader, this.CreateValidationContext(this.reader), true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }
        }

        /// <summary>
        /// Validates that an empty address returns an error.
        /// </summary>
        [TestMethod]
        public void AddressIsRequired()
        {
            this.reader.Address = null;
            this.AssertValidationException(this.reader, "Address is required!");
        }

        /// <summary>
        /// Validates that an invalid email address returns an error.
        /// </summary>
        [TestMethod]
        public void InvalidEmailAddress()
        {
            this.reader.EmailAddress = "invalid_email";
            this.AssertValidationException(this.reader, "Invalid Email Address");
        }

        /// <summary>
        /// Validates that a correct email address passes validation.
        /// </summary>
        [TestMethod]
        public void CorrectEmailAddress()
        {
            this.reader.EmailAddress = "d_raszvan01@yahoo.com";
            try
            {
                Validator.ValidateObject(this.reader, this.CreateValidationContext(this.reader), true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }
        }

        /// <summary>
        /// Validates setting the role to true.
        /// </summary>
        [TestMethod]
        public void SetRoleToTrue()
        {
            this.reader.Role = true;
            Assert.IsTrue(this.reader.Role);
        }

        /// <summary>
        /// Validates setting the role to false.
        /// </summary>
        [TestMethod]
        public void SetRoleToFalse()
        {
            this.reader.Role = false;
            Assert.IsFalse(this.reader.Role);
        }

        /// <summary>
        /// Validates that an invalid phone number returns an error.
        /// </summary>
        [TestMethod]
        public void InvalidPhoneNumber()
        {
            this.reader.PhoneNumber = "invalid_phone";
            this.AssertValidationException(this.reader, "Invalid Phone Number!");
        }

        /// <summary>
        /// Validates that a correct phone number passes validation.
        /// </summary>
        [TestMethod]
        public void CorrectPhoneNumber()
        {
            this.reader.PhoneNumber = "0238432132";
            try
            {
                Validator.ValidateObject(this.reader, this.CreateValidationContext(this.reader), true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }
        }

        /// <summary>
        /// Validates that either phone number or email address is required.
        /// </summary>
        [TestMethod]
        public void ReaderPhoneNumberOrEmailEmpty()
        {
            this.reader.PhoneNumber = string.Empty;
            this.reader.EmailAddress = string.Empty;
            var validationContext = new ValidationContext(this.reader, null, null);
            var validationResult = new PhoneOrEmailReq().GetValidationResult(this.reader, validationContext);
            Assert.IsNotNull(validationResult);
            Assert.AreEqual("Either phone number or email address is required.", validationResult.ErrorMessage);
        }

        /// <summary>
        /// Validates the scenario when email is empty and phone number is correct.
        /// </summary>
        [TestMethod]
        public void ReaderEmailEmptyPhoneNumberCorrect()
        {
            this.reader.PhoneNumber = "0238413091";

            try
            {
                Validator.ValidateObject(this.reader, this.CreateValidationContext(this.reader), true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }

            this.reader.EmailAddress = string.Empty;

            var validationContext = new ValidationContext(this.reader, null, null);
            var validationResult = new PhoneOrEmailReq().GetValidationResult(this.reader, validationContext);

            Assert.IsNull(validationResult);
            Assert.AreEqual(ValidationResult.Success, validationResult);
        }

        /// <summary>
        /// Validates the scenario when phone number is empty and email is correct.
        /// </summary>
        [TestMethod]

        public void ReaderPhoneNumberEmptyEmailCorrect()
        {
            this.reader.EmailAddress = "test@gmail.com";

            try
            {
                Validator.ValidateObject(this.reader, this.CreateValidationContext(this.reader), true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }

            this.reader.PhoneNumber = string.Empty;

            var validationContext = new ValidationContext(this.reader, null, null);
            var validationResult = new PhoneOrEmailReq().GetValidationResult(this.reader, validationContext);

            Assert.IsNull(validationResult);
            Assert.AreEqual(ValidationResult.Success, validationResult);
        }

        /// <summary>
        /// Validates the scenario when phone number is empty and email is incorrect.
        /// </summary>
        [TestMethod]
        public void ReaderPhoneNumberEmptyEmailIncorrect()
        {
            this.reader.EmailAddress = "email_gresit";

            this.AssertValidationException(this.reader, "Invalid Email Address");

            this.reader.PhoneNumber = string.Empty;

            var validationContext = new ValidationContext(this.reader, null, null);
            var validationResult = new PhoneOrEmailReq().GetValidationResult(this.reader, validationContext);

            Assert.IsNull(validationResult);
            Assert.AreEqual(ValidationResult.Success, validationResult);
        }

        /// <summary>
        /// Validates the scenario when email is empty and phone number is incorrect.
        /// </summary>
        [TestMethod]
        public void ReaderEmailEmptyPhoneNumberIncorrect()
        {
            this.reader.PhoneNumber = "numar_gresit";

            this.AssertValidationException(this.reader, "Invalid Phone Number!");

            this.reader.EmailAddress = string.Empty;

            var validationContext = new ValidationContext(this.reader, null, null);
            var validationResult = new PhoneOrEmailReq().GetValidationResult(this.reader, validationContext);

            Assert.IsNull(validationResult);
            Assert.AreEqual(ValidationResult.Success, validationResult);
        }

        /// <summary>
        /// Creates a validation context for the specified instance.
        /// </summary>
        private ValidationContext CreateValidationContext(object instance)
        {
            return new ValidationContext(instance, null, null);
        }

        /// <summary>
        /// Asserts that a validation exception is thrown for the specified instance and expected error message.
        /// </summary>
        private void AssertValidationException<T>(T instance, string expectedErrorMessage)
        {
            ModelValidationHelper.AssertValidationException(instance, expectedErrorMessage);
        }
    }
}
