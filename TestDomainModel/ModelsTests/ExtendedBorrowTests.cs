namespace TestDomainModel.ModelsTests
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using DomainModel;
    using DomainModel.CustomValidationHelpers;
    using Library.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test class for validating the ExtendedBorrow model.
    /// </summary>
    [TestClass]
    public class ExtendedBorrowTests
    {
        private ExtendedBorrow extendedBorrow;

        /// <summary>
        /// Creates a validation context for the specified instance.
        /// </summary>
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
                    IsReturned = false,
                },
                Date = new DateTime(2023, 1, 1, 12, 0, 0),
            };
        }

        /// <summary>
        /// Validates that a valid extended borrow returns success.
        /// </summary>
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
                    IsReturned = false,
                },
                Date = new DateTime(2022, 1, 5),
            };
            try
            {
                Validator.ValidateObject(this.extendedBorrow, this.CreateValidationContext(this.extendedBorrow), true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }
        }

        /// <summary>
        /// Validates that a missing borrow returns an error.
        /// </summary>
        [TestMethod]
        public void MissingBorrow_ReturnsError()
        {
            this.extendedBorrow = new ExtendedBorrow
            {
                Borrow = null,
                Date = new DateTime(2022, 1, 5),
            };
            this.AssertValidationException(this.extendedBorrow, "A borrow is required!");
        }

        /// <summary>
        /// Validates that an invalid range returns an error.
        /// </summary>
        [TestMethod]
        public void InvalidRange_ReturnsError()
        {
            this.extendedBorrow = new ExtendedBorrow
            {
                Borrow = new Borrow
                {
                    BorrowStartDate = new DateTime(2022, 1, 1),
                    BorrowEndDate = new DateTime(2022, 1, 10),
                },
                Date = new DateTime(1800, 1, 5),
            };
            this.AssertValidationException(this.extendedBorrow, "Date must be between 1900 and 2100.");
        }

        private ValidationContext CreateValidationContext(object instance)
        {
            return new ValidationContext(instance, null, null);
        }

        private void AssertValidationException<T>(T instance, string expectedErrorMessage)
        {
            ModelValidationHelper.AssertValidationException(instance, expectedErrorMessage);
        }
    }
}
