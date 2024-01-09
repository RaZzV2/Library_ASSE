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
    /// <summary>
    /// Summary description for ExtendedBorrowTests
    /// </summary>
    [TestClass]
    public class ExtendedBorrowTests
    {
        private ExtendedBorrow extendedBorrow;

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
            this.extendedBorrow = new ExtendedBorrow()
            {

                Borrow = new Borrow
                {
                    Reader = new Reader { },
                    Edition = new Edition { },
                    BorrowStartDate = dateTime,
                    BorrowEndDate = dateTime.AddDays(7),
                    IsReturned = false
                },
                Date = new DateTime(2023, 1, 1, 12, 0, 0)
            };
        }

        [TestMethod]
        public void ValidExtendedBorrow_ReturnsSuccess()
        {
            var dateTime = new DateTime(2022, 1, 1, 12, 0, 0);
            this.extendedBorrow = new ExtendedBorrow
            {
                Borrow = new Borrow
                {
                    Reader = new Reader { },
                    Edition = new Edition { },
                    BorrowStartDate = dateTime,
                    BorrowEndDate = dateTime.AddDays(7),
                    IsReturned = false
                },
                Date = new DateTime(2022, 1, 5)
            };
            try
            {
                Validator.ValidateObject(this.extendedBorrow, CreateValidationContext(this.extendedBorrow), true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }
        }

        [TestMethod]
        public void MissingBorrow_ReturnsError()
        {
            var dateTime = new DateTime(2022, 1, 1, 12, 0, 0);
            this.extendedBorrow = new ExtendedBorrow
            {
                Borrow = null,
                Date = new DateTime(2022, 1, 5)
            };
            AssertValidationException(this.extendedBorrow, "A borrow is required!");
        }

        [TestMethod]
        public void InvalidRange_ReturnsError()
        {
            var dateTime = new DateTime(2022, 1, 1, 12, 0, 0);
            this.extendedBorrow = new ExtendedBorrow
            {
                Borrow = new Borrow
                {
                    BorrowStartDate = new DateTime(2022, 1, 1),
                    BorrowEndDate = new DateTime(2022, 1, 10)
                },
                Date = new DateTime(1800, 1, 5)
            };
            AssertValidationException(this.extendedBorrow, "Date must be between 1900 and 2100.");
        }
    }
}
