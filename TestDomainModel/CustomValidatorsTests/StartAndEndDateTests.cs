using DomainModel.CustomValidators;
using DomainModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TestDomainModel.CustomValidatorsTests
{
    [TestClass]
    public class StartAndEndDateTests
    {
        [TestMethod]
        public void StartDateIsEarlierThanEndDate_ReturnsSuccess()
        {
            var borrow = new Borrow { BorrowStartDate = DateTime.Now, BorrowEndDate = DateTime.Now.AddDays(5) };
            var validator = new StartEndDateValid();

            var validationResult = validator.GetValidationResult(borrow, new ValidationContext(borrow));

            Assert.AreEqual(ValidationResult.Success, validationResult);
        }

        [TestMethod]
        public void StartDateEqualsEndDate_ReturnsSuccess()
        {
            var borrow = new Borrow { BorrowStartDate = DateTime.Now, BorrowEndDate = DateTime.Now };
            var validator = new StartEndDateValid();

            var validationResult = validator.GetValidationResult(borrow, new ValidationContext(borrow));

            Assert.AreEqual(ValidationResult.Success, validationResult);
        }

        [TestMethod]
        public void StartDateIsLaterThanEndDate_ReturnsError()
        {
            var borrow = new Borrow { BorrowStartDate = DateTime.Now.AddDays(5), BorrowEndDate = DateTime.Now };
            var validator = new StartEndDateValid();

            var validationResult = validator.GetValidationResult(borrow, new ValidationContext(borrow));

            Assert.AreNotEqual(ValidationResult.Success, validationResult);
            Assert.AreEqual("End date must be later than start date.", validationResult.ErrorMessage);
        }

        [TestMethod]
        public void WrongObjectType_ReturnsError()
        {
            var notBorrowObject = new object();
            var validator = new StartEndDateValid();

            var validationResult = validator.GetValidationResult(notBorrowObject, new ValidationContext(notBorrowObject));

            Assert.AreNotEqual(ValidationResult.Success, validationResult);
            Assert.AreEqual("Wrong object type!", validationResult.ErrorMessage);
        }

        [TestMethod]
        public void MinValueStartDateAndEndDate_ReturnsSuccess()
        {
            var borrow = new Borrow { BorrowStartDate = DateTime.MinValue, BorrowEndDate = DateTime.MinValue };
            var validator = new StartEndDateValid();

            var validationResult = validator.GetValidationResult(borrow, new ValidationContext(borrow));

            Assert.AreEqual(ValidationResult.Success, validationResult);
        }

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
