// <copyright file="BorrowTests.cs" company="Transilvania University of Brasov">
// Dragomir Razvan
// </copyright>

namespace TestDomainModel.ModelsTests
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using DomainModel;
    using DomainModel.CustomValidationHelpers;
    using Library.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test class for validating the Borrow model.
    /// </summary>
    [TestClass]
    public class BorrowTests
    {
        /// <summary>
        /// Gets or sets the instance of the Borrow associated with this object.
        /// </summary>
        private Borrow borrow;

        /// <summary>
        /// Set up method to initialize common objects for tests.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            var dateTime = new DateTime(2022, 1, 1, 12, 0, 0);
            this.borrow = new Borrow
            {
                Id = 1,
                Reader = new Reader { },
                Edition = new Edition { },
                BorrowStartDate = dateTime,
                BorrowEndDate = dateTime.AddDays(7),
                IsReturned = false,
            };
        }

        /// <summary>
        /// Validates that the start date is valid and returns success.
        /// </summary>
        [TestMethod]
        public void ValidStartDate()
        {
            this.borrow.BorrowStartDate = new DateTime(2022, 1, 1, 12, 0, 0);
            try
            {
                Validator.ValidateObject(this.borrow, this.CreateValidationContext(this.borrow), true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }
        }

        /// <summary>
        /// Validates that the end date is valid and returns success.
        /// </summary>
        [TestMethod]
        public void ValidEndDate()
        {
            this.borrow.BorrowStartDate = new DateTime(2022, 1, 1, 12, 0, 0);
            this.borrow.BorrowEndDate = this.borrow.BorrowStartDate.AddDays(7);
            try
            {
                Validator.ValidateObject(this.borrow, this.CreateValidationContext(this.borrow), true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }
        }

        /// <summary>
        /// Validates that an incorrect date format results in an error.
        /// </summary>
        [TestMethod]
        public void IncorrectDate()
        {
            bool invalid_date = DateTime.TryParse("not_a_date", out _);
            Assert.IsFalse(invalid_date);
        }

        /// <summary>
        /// Validates that the start date is greater than the end date and returns an error.
        /// </summary>
        [TestMethod]
        public void StartDateGreaterThanEndDate()
        {
            this.borrow.BorrowEndDate = new DateTime(2022, 1, 1, 12, 0, 0);
            this.borrow.BorrowStartDate = this.borrow.BorrowStartDate.AddDays(7);
            this.AssertValidationException(this.borrow, "End date must be later than start date.");
        }

        /// <summary>
        /// Validates that the start date is lower than the end date and returns success.
        /// </summary>
        [TestMethod]
        public void StartDateLowerThanEndDate()
        {
            this.borrow.BorrowStartDate = new DateTime(2022, 1, 1, 12, 0, 0);
            this.borrow.BorrowEndDate = this.borrow.BorrowStartDate.AddDays(7);
            try
            {
                Validator.ValidateObject(this.borrow, this.CreateValidationContext(this.borrow), true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }
        }

        /// <summary>
        /// Validates that a reader is required and returns success.
        /// </summary>
        [TestMethod]
        public void ReaderRequired()
        {
            this.borrow.Reader = new Reader
            {
                ReaderFirstName = "Andrei",
            };
            try
            {
                Validator.ValidateObject(this.borrow, this.CreateValidationContext(this.borrow), true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }
        }

        /// <summary>
        /// Validates that no reader is required and returns an error.
        /// </summary>
        [TestMethod]
        public void NoReaderRequired()
        {
            this.borrow.Reader = null;
            this.AssertValidationException(this.borrow, "Reader is required!");
        }

        /// <summary>
        /// Validates that an edition is required and returns success.
        /// </summary>
        [TestMethod]
        public void EditionRequired()
        {
            this.borrow.Edition = new Edition
            {
                EditionName = "Editia 1",
            };
            try
            {
                Validator.ValidateObject(this.borrow, this.CreateValidationContext(this.borrow), true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }
        }

        /// <summary>
        /// Validates that no edition is required and returns an error.
        /// </summary>
        [TestMethod]
        public void NoEditionRequired()
        {
            this.borrow.Edition = null;
            this.AssertValidationException(this.borrow, "Edition is required!");
        }

        /// <summary>
        /// Sets the return status to true and asserts equality.
        /// </summary>
        [TestMethod]
        public void SetReturnStatusToTrue()
        {
            this.borrow.IsReturned = true;
            Assert.IsTrue(this.borrow.IsReturned);
        }

        /// <summary>
        /// Sets the return status to false and asserts equality.
        /// </summary>
        [TestMethod]
        public void SetReturnStatusToFalse()
        {
            this.borrow.IsReturned = false;
            Assert.IsFalse(this.borrow.IsReturned);
        }

        /// <summary>
        /// Validates that the start date is before the minimum range and returns an error.
        /// </summary>
        [TestMethod]
        public void StartDateBeforeMinRange()
        {
            this.borrow.BorrowStartDate = new DateTime(1800, 1, 1, 12, 0, 0);
            this.AssertValidationException(this.borrow, "Date must be between 1900 and 2100.");
        }

        /// <summary>
        /// Validates that the end date is after the maximum range and returns an error.
        /// </summary>
        [TestMethod]
        public void EndDateAfterMaxRange()
        {
            this.borrow.BorrowEndDate = new DateTime(2122, 1, 1, 12, 0, 0);
            this.AssertValidationException(this.borrow, "Date must be between 1900 and 2100.");
        }

        /// <summary>
        /// Validates that the start date is after the maximum range and returns an error.
        /// </summary>
        [TestMethod]
        public void StartDateAfterMaxRange()
        {
            this.borrow.BorrowStartDate = new DateTime(2101, 1, 1, 12, 0, 0);
            this.AssertValidationException(this.borrow, "Date must be between 1900 and 2100.");
        }

        /// <summary>
        /// Validates that the end date is before the minimum range and returns an error.
        /// </summary>
        [TestMethod]
        public void EndDateBeforeMinRange()
        {
            this.borrow.BorrowStartDate = new DateTime(1500, 1, 1, 12, 0, 0);
            this.AssertValidationException(this.borrow, "Date must be between 1900 and 2100.");
        }

        /// <summary>
        /// Sets the ID of the borrow object and asserts equality.
        /// </summary>
        [TestMethod]
        public void IdIsSetCorrectly()
        {
            int expectedId = 42;

            this.borrow.Id = expectedId;

            Assert.AreEqual(expectedId, this.borrow.Id);
        }

        /// <summary>
        /// Creates a new <see cref="ValidationContext"/> for the specified instance with optional service provider and items.
        /// </summary>
        /// <param name="instance">The object to be validated.</param>
        /// <returns>A <see cref="ValidationContext"/> for the specified instance.</returns>
        private ValidationContext CreateValidationContext(object instance)
        {
            return new ValidationContext(instance, null, null);
        }

        /// <summary>
        /// Asserts that a validation exception is thrown for the specified instance with the expected error message.
        /// </summary>
        /// <typeparam name="T">The type of the object to be validated.</typeparam>
        /// <param name="instance">The object to be validated.</param>
        /// <param name="expectedErrorMessage">The expected error message that should be thrown.</param>
        private void AssertValidationException<T>(T instance, string expectedErrorMessage)
        {
            ModelValidationHelper.AssertValidationException(instance, expectedErrorMessage);
        }
    }
}
