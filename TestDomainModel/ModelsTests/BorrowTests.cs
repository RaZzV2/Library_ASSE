using DomainModel;
using DomainModel.CustomValidationHelpers;
using Library.models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TestDomainModel.ModelsTests
{
    [TestClass]
    public class BorrowTests
    {
        private Borrow borrow;

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
            var dateTime = new DateTime(2022, 1, 1, 12, 0, 0);
            this.borrow = new Borrow
            {
                Reader = new Reader {  },
                Edition = new Edition {  },
                BorrowStartDate = dateTime,
                BorrowEndDate = dateTime.AddDays(7),
                IsReturned = false
            };
        }

        [TestMethod]
        public void ValidStartDate()
        {
            this.borrow.BorrowStartDate = new DateTime(2022, 1, 1, 12, 0, 0);
            try
            {
                Validator.ValidateObject(this.borrow, CreateValidationContext(this.borrow), true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }
        }

        [TestMethod]
        public void ValidEndDate()
        {
            this.borrow.BorrowStartDate = new DateTime(2022, 1, 1, 12, 0, 0);
            this.borrow.BorrowEndDate = this.borrow.BorrowStartDate.AddDays(7);
            try
            {
                Validator.ValidateObject(this.borrow, CreateValidationContext(this.borrow), true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }
        }

        [TestMethod]
        public void IncorrectDate()
        {
            bool invalid_date = DateTime.TryParse("not_a_date", out _);
            Assert.IsFalse(invalid_date);
        }

        [TestMethod]
        public void StartDateGreaterThanEndDate()
        {
            this.borrow.BorrowEndDate = new DateTime(2022, 1, 1, 12, 0, 0);
            this.borrow.BorrowStartDate = this.borrow.BorrowStartDate.AddDays(7);
            AssertValidationException(this.borrow, "End date must be later than start date.");
        }

        [TestMethod]
        public void StartDateLowerThanEndDate()
        {
            this.borrow.BorrowStartDate = new DateTime(2022, 1, 1, 12, 0, 0);
            this.borrow.BorrowEndDate = this.borrow.BorrowStartDate.AddDays(7);
            try
            {
                Validator.ValidateObject(this.borrow, CreateValidationContext(this.borrow), true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }
        }

        [TestMethod]
        public void ReaderRequired()
        {
            this.borrow.Reader = new Reader
            {
                ReaderFirstName = "Andrei"
            };
            try
            {
                Validator.ValidateObject(this.borrow, CreateValidationContext(this.borrow), true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }
        }

        [TestMethod]
        public void NoReaderRequired()
        {
            this.borrow.Reader = null;
            AssertValidationException(this.borrow, "Reader is required!");
        }

        [TestMethod]
        public void EditionRequired()
        {
            this.borrow.Edition = new Edition
            {
                EditionName = "Editia 1"
            };
            try
            {
                Validator.ValidateObject(this.borrow, CreateValidationContext(this.borrow), true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }
        }

        [TestMethod]
        public void NoEditionRequired()
        {
            this.borrow.Edition = null;
            AssertValidationException(this.borrow, "Edition is required!");
        }

    }
}
