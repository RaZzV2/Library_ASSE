// <copyright file="DateRangeTests.cs" company="Transilvania University of Brasov">
// Dragomir Razvan
// </copyright>

namespace TestDomainModel.CustomValidatorsTests
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using DomainModel.CustomValidators;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test class for validating the DateRange custom validator.
    /// </summary>
    [TestClass]
    public class DateRangeTests
    {
        /// <summary>
        /// Validates that a date within the specified range returns success.
        /// </summary>
        [TestMethod]
        public void ValidDateWithinRange_ReturnsSuccess()
        {
            var validator = new DateRange(2000, 2030);
            var validDate = new DateTime(2025, 1, 1);

            var validationResult = validator.GetValidationResult(validDate, new ValidationContext(validDate));

            Assert.AreEqual(validationResult, ValidationResult.Success);
        }

        /// <summary>
        /// Validates that a date before the specified minimum year returns an error.
        /// </summary>
        [TestMethod]
        public void DateBeforeMinYear_ReturnsError()
        {
            var validator = new DateRange(2000, 2030);
            var invalidDate = new DateTime(1999, 1, 1);

            var validationResult = validator.GetValidationResult(invalidDate, new ValidationContext(invalidDate));

            Assert.AreNotEqual(ValidationResult.Success, validationResult);
            Assert.AreEqual("Date must be between 2000 and 2030.", validationResult.ErrorMessage);
        }

        /// <summary>
        /// Validates that a date after the specified maximum year returns an error.
        /// </summary>
        [TestMethod]
        public void DateAfterMaxYear_ReturnsError()
        {
            var validator = new DateRange(2000, 2030);
            var invalidDate = new DateTime(2031, 1, 1);

            var validationResult = validator.GetValidationResult(invalidDate, new ValidationContext(invalidDate));

            Assert.AreNotEqual(ValidationResult.Success, validationResult);
            Assert.AreEqual("Date must be between 2000 and 2030.", validationResult.ErrorMessage);
        }

        /// <summary>
        /// Validates that a valid date on the minimum year boundary returns success.
        /// </summary>
        [TestMethod]
        public void ValidDateOnMinYearBoundary_ReturnsSuccess()
        {
            var validator = new DateRange(2000, 2030);
            var validDate = new DateTime(2000, 1, 1);

            var validationResult = validator.GetValidationResult(validDate, new ValidationContext(validDate));

            Assert.AreEqual(ValidationResult.Success, validationResult);
        }

        /// <summary>
        /// Validates that a valid date on the maximum year boundary returns success.
        /// </summary>
        [TestMethod]
        public void ValidDateOnMaxYearBoundary_ReturnsSuccess()
        {
            var validator = new DateRange(2000, 2030);
            var validDate = new DateTime(2030, 1, 1);

            var validationResult = validator.GetValidationResult(validDate, new ValidationContext(validDate));

            Assert.AreEqual(ValidationResult.Success, validationResult);
        }

        /// <summary>
        /// Validates that validating a string DateTime value returns an error.
        /// </summary>
        [TestMethod]
        public void ValidatingStringDateTimeValue_ReturnsError()
        {
            var validator = new DateRange(2000, 2030);
            var invalidValue = "InvalidValue";

            var validationResult = validator.GetValidationResult(invalidValue, new ValidationContext(invalidValue));

            Assert.AreNotEqual(ValidationResult.Success, validationResult);
            Assert.AreEqual("The value must be a valid DateTime.", validationResult.ErrorMessage);
        }

        /// <summary>
        /// Validates that validating an int DateTime value returns an error.
        /// </summary>
        [TestMethod]
        public void ValidatingIntDateTimeValue_ReturnsError()
        {
            var validator = new DateRange(2000, 2030);
            var invalidValue = 33;

            var validationResult = validator.GetValidationResult(invalidValue, new ValidationContext(invalidValue));

            Assert.AreNotEqual(ValidationResult.Success, validationResult);
            Assert.AreEqual("The value must be a valid DateTime.", validationResult.ErrorMessage);
        }
    }
}
