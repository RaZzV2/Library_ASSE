// <copyright file="ExtendedBorrowTests.cs" company="Transilvania University of Brasov">
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
    /// Test class for validating the ExtendedBorrow model.
    /// </summary>
    [TestClass]
    public class ExtendedBorrowTests
    {
        /// <summary>
        /// Represents extended borrowing periods in a library system, providing information about the duration and associated borrow details.
        /// </summary>
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
