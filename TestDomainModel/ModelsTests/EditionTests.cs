namespace TestDomainModel.ModelsTests
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using DomainModel.CustomValidationHelpers;
    using Library.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test class for validating the Edition model.
    /// </summary>
    [TestClass]
    public class EditionTests
    {

        private Edition edition;

        /// <summary>
        /// Set up method to initialize common objects for tests.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            this.edition = new Edition
            {
                EditionName = "Editia Calugarului",
                EditionYear = 2003,
                PagesNumber = 99,
                BorrowableBooks = 23,
                UnBorrowableBooks = 32,
                BookType = Edition.Type.Board,
                Book = new Book()
                {
                },
            };
        }

        /// <summary>
        /// Validates that the edition name is not empty and returns an error.
        /// </summary>
        [TestMethod]
        public void EditionNameEmpty()
        {
            this.edition.EditionName = string.Empty;
            this.AssertValidationException(this.edition, "Edition name is required!");
        }

        /// <summary>
        /// Validates that the edition name does not contain special characters and returns an error.
        /// </summary>
        [TestMethod]
        public void EditionNameWithSpecialCharacters()
        {
            this.edition.EditionName = "Editia@@23++;";
            this.AssertValidationException(this.edition, "Edition name must not have special characters!");
        }

        /// <summary>
        /// Validates that the edition name has at least 4 characters and returns an error.
        /// </summary>
        [TestMethod]
        public void EditionNameTooShort()
        {
            this.edition.EditionName = "Ed";
            this.AssertValidationException(this.edition, "Edition name must have at least 4 characters!");
        }

        /// <summary>
        /// Validates that the edition name is correct and returns success.
        /// </summary>
        [TestMethod]

        public void EditionNameCorrect()
        {
            this.edition.EditionName = "Editia de Craciun";
            try
            {
                Validator.ValidateObject(this.edition, this.CreateValidationContext(this.edition), true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }
        }

        /// <summary>
        /// Validates that the edition year is too low and returns an error.
        /// </summary>
        [TestMethod]
        public void EditionYearTooLow()
        {
            this.edition.EditionYear = 1300;
            this.AssertValidationException(this.edition, "Invalid edition year. Must be between 1900 and 2024.");
        }

        /// <summary>
        /// Validates that the edition year is negative and returns an error.
        /// </summary>
        [TestMethod]
        public void EditionYearNegative()
        {
            this.edition.EditionYear = -100;
            this.AssertValidationException(this.edition, "Invalid edition year. Must be between 1900 and 2024.");
        }

        /// <summary>
        /// Validates that the edition year is too high and returns an error.
        /// </summary>
        [TestMethod]
        public void EditionYearTooHigh()
        {
            this.edition.EditionYear = 2899;
            this.AssertValidationException(this.edition, "Invalid edition year. Must be between 1900 and 2024.");
        }

        /// <summary>
        /// Validates that the edition year is correct and returns success.
        /// </summary>
        [TestMethod]
        public void EditionYearCorrect()
        {
            this.edition.EditionYear = 2020;
            try
            {
                Validator.ValidateObject(this.edition, this.CreateValidationContext(this.edition), true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }
        }

        /// <summary>
        /// Validates that the number of pages is negative and returns an error.
        /// </summary>
        [TestMethod]
        public void NegativeNumberOfPages()
        {
            this.edition.PagesNumber = -3;
            this.AssertValidationException(this.edition, "Number of pages must be at least 1.");
        }

        /// <summary>
        /// Validates that the number of pages is correct and returns success.
        /// </summary>
        [TestMethod]
        public void CorrectNumberOfPages()
        {
            this.edition.PagesNumber = 100;
            try
            {
                Validator.ValidateObject(this.edition, this.CreateValidationContext(this.edition), true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }
        }

        /// <summary>
        /// Validates that the number of borrowable books is negative and returns an error.
        /// </summary>
        [TestMethod]
        public void NegativeBorrowableBooks()
        {
            this.edition.BorrowableBooks = -3;
            this.AssertValidationException(this.edition, "Number of borrowable books cannot be negative.");
        }

        /// <summary>
        /// Validates that the number of borrowable books is correct and returns success.
        /// </summary>
        [TestMethod]
        public void CorrectBorrowableBooks()
        {
            this.edition.BorrowableBooks = 32;
            try
            {
                Validator.ValidateObject(this.edition, this.CreateValidationContext(this.edition), true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }
        }

        /// <summary>
        /// Validates that the number of unborrowable books is negative and returns an error.
        /// </summary>
        [TestMethod]
        public void NegativeUnBorrowableBooks()
        {
            this.edition.UnBorrowableBooks = -3;
            this.AssertValidationException(this.edition, "Number of unborrowable books cannot be negative.");
        }

        /// <summary>
        /// Validates that the number of unborrowable books is correct and returns success.
        /// </summary>
        [TestMethod]
        public void CorrectUnBorrowableBooks()
        {
            this.edition.UnBorrowableBooks = 32;
            try
            {
                Validator.ValidateObject(this.edition, this.CreateValidationContext(this.edition), true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }
        }

        /// <summary>
        /// Validates that the book type is correct and returns success.
        /// </summary>
        [TestMethod]
        public void CorrectBookType()
        {
            this.edition.BookType = Edition.Type.Hardcover;
            try
            {
                Validator.ValidateObject(this.edition, this.CreateValidationContext(this.edition), true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }
        }

        /// <summary>
        /// Validates that an invalid book type returns an error.
        /// </summary>
        [TestMethod]
        public void InvalidBookType()
        {
            this.edition.BookType = (Edition.Type)99;
            this.AssertValidationException(this.edition, "Invalid book type.");
        }

        /// <summary>
        /// Validates that having at least one book is correct and returns success.
        /// </summary>
        [TestMethod]
        public void AtLeastOneBook()
        {
            this.edition.Book = new Book()
            {
                Title = "Amintiri din copilarie",
                Author = new List<Author>
                {
                    new Author
                    {
                        FirstName = "Ion",
                        LastName = "Creanga",
                    },
                },
                Domains = new List<BookDomain>
                {
                    new BookDomain
                    {
                    },
                },
            };
            try
            {
                Validator.ValidateObject(this.edition.Book, this.CreateValidationContext(this.edition.Book), true);
                Validator.ValidateObject(this.edition, this.CreateValidationContext(this.edition), true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }
        }

        /// <summary>
        /// Validates that having no book returns an error.
        /// </summary>
        [TestMethod]
        public void NoBook()
        {
            this.edition.Book = null;
            this.AssertValidationException(this.edition, "A edition must have at least one book!");
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
