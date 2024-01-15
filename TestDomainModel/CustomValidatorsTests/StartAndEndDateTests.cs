// <copyright file="StartAndEndDateTests.cs" company="Transilvania University of Brasov">
// Dragomir Razvan
// </copyright>

namespace TestDomainModel.CustomValidatorsTests
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using DomainModel;
    using DomainModel.CustomValidators;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test class for validating the StartEndDateValid custom validator.
    /// </summary>
    [TestClass]
    public class StartAndEndDateTests
    {
        /// <summary>
        /// Validates that the start date is earlier than the end date, and returns success.
        /// </summary>
        [TestMethod]
        public void StartDateIsEarlierThanEndDate_ReturnsSuccess()
        {
            var borrow = new Borrow { BorrowStartDate = DateTime.Now, BorrowEndDate = DateTime.Now.AddDays(5) };
            var validator = new StartEndDateValid();

            var validationResult = validator.GetValidationResult(borrow, new ValidationContext(borrow));

            Assert.AreEqual(ValidationResult.Success, validationResult);
        }

        /// <summary>
        /// Validates that the start date equals the end date, and returns success.
        /// </summary>
        [TestMethod]
        public void StartDateEqualsEndDate_ReturnsSuccess()
        {
            var borrow = new Borrow { BorrowStartDate = DateTime.Now, BorrowEndDate = DateTime.Now };
            var validator = new StartEndDateValid();

            var validationResult = validator.GetValidationResult(borrow, new ValidationContext(borrow));

            Assert.AreEqual(ValidationResult.Success, validationResult);
        }

        /// <summary>
        /// Validates that the start date is later than the end date, and returns an error.
        /// </summary>
        [TestMethod]
        public void StartDateIsLaterThanEndDate_ReturnsError()
        {
            var borrow = new Borrow { BorrowStartDate = DateTime.Now.AddDays(5), BorrowEndDate = DateTime.Now };
            var validator = new StartEndDateValid();

            var validationResult = validator.GetValidationResult(borrow, new ValidationContext(borrow));

            Assert.AreNotEqual(ValidationResult.Success, validationResult);
            Assert.AreEqual("End date must be later than start date.", validationResult.ErrorMessage);
        }

        /// <summary>
        /// Validates that the object type is not of type 'Borrow', and returns an error.
        /// </summary>
        [TestMethod]
        public void WrongObjectType_ReturnsError()
        {
            var notBorrowObject = new object();
            var validator = new StartEndDateValid();

            var validationResult = validator.GetValidationResult(notBorrowObject, new ValidationContext(notBorrowObject));

            Assert.AreNotEqual(ValidationResult.Success, validationResult);
            Assert.AreEqual("Wrong object type!", validationResult.ErrorMessage);
        }

        /// <summary>
        /// Validates that the start and end dates are set to DateTime.MinValue, and returns success.
        /// </summary>
        [TestMethod]
        public void MinValueStartDateAndEndDate_ReturnsSuccess()
        {
            var borrow = new Borrow { BorrowStartDate = DateTime.MinValue, BorrowEndDate = DateTime.MinValue };
            var validator = new StartEndDateValid();

            var validationResult = validator.GetValidationResult(borrow, new ValidationContext(borrow));

            Assert.AreEqual(ValidationResult.Success, validationResult);
        }

        /// <summary>
        /// Validates that the start and end dates are set to DateTime.MaxValue, and returns success.
        /// </summary>
        [TestMethod]
        public void MaxValueStartDateAndEndDate_ReturnsSuccess()
        {
            var borrow = new Borrow { BorrowStartDate = DateTime.MaxValue, BorrowEndDate = DateTime.MaxValue };
            var validator = new StartEndDateValid();

            var validationResult = validator.GetValidationResult(borrow, new ValidationContext(borrow));

            Assert.AreEqual(ValidationResult.Success, validationResult);
        }
    }
}
