using DomainModel.CustomValidators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks.Constraints;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TestDomainModel.CustomValidatorsTests
{
    [TestClass]
    public class DateRangeTests
    {
        [TestMethod]
        public void ValidDateWithinRange_ReturnsSuccess()
        {
            var validator = new DateRange(2000, 2030);
            var validDate = new DateTime(2025, 1, 1);

            var validationResult = validator.GetValidationResult(validDate, new ValidationContext(validDate));

            Assert.AreEqual(validationResult, ValidationResult.Success);
        }

        [TestMethod]
        public void DateBeforeMinYear_ReturnsError()
        {
            var validator = new DateRange(2000, 2030);
            var invalidDate = new DateTime(1999, 1, 1);

            var validationResult = validator.GetValidationResult(invalidDate, new ValidationContext(invalidDate));

            Assert.AreNotEqual(ValidationResult.Success, validationResult);
            Assert.AreEqual("Date must be between 2000 and 2030.", validationResult.ErrorMessage);
        }

        [TestMethod]
        public void DateAfterMaxYear_ReturnsError()
        {
            var validator = new DateRange(2000, 2030);
            var invalidDate = new DateTime(2031, 1, 1);

            var validationResult = validator.GetValidationResult(invalidDate, new ValidationContext(invalidDate));

            Assert.AreNotEqual(ValidationResult.Success, validationResult);
            Assert.AreEqual("Date must be between 2000 and 2030.", validationResult.ErrorMessage);
        }

        [TestMethod]
        public void ValidDateOnMinYearBoundary_ReturnsSuccess()
        {
            var validator = new DateRange(2000, 2030);
            var validDate = new DateTime(2000, 1, 1);

            var validationResult = validator.GetValidationResult(validDate, new ValidationContext(validDate));

            Assert.AreEqual(ValidationResult.Success, validationResult);
        }

        [TestMethod]
        public void ValidDateOnMaxYearBoundary_ReturnsSuccess()
        {
            var validator = new DateRange(2000, 2030);
            var validDate = new DateTime(2030, 1, 1);

            var validationResult = validator.GetValidationResult(validDate, new ValidationContext(validDate));

            Assert.AreEqual(ValidationResult.Success, validationResult);
        }

        [TestMethod]
        public void ValidatingStringDateTimeValue_ReturnsError()
        {
            var validator = new DateRange(2000, 2030);
            var invalidValue = "InvalidValue";

            var validationResult = validator.GetValidationResult(invalidValue, new ValidationContext(invalidValue));

            Assert.AreNotEqual(ValidationResult.Success, validationResult);
            Assert.AreEqual("The value must be a valid DateTime.", validationResult.ErrorMessage);
        }

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
