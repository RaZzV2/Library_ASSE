using DomainModel;
using DomainModel.CustomValidationHelpers;
using Library.models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;
using System.Text;

namespace TestDomainModel.ModelsTests
{
    [TestClass]
    public class EditionTests
    {

        private Edition edition;

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

                }
            };
        }

        [TestMethod]
        public void EditionNameEmpty()
        {
            this.edition.EditionName = string.Empty;
            AssertValidationException(this.edition, "Edition name is required!");
        }

        [TestMethod]
        public void EditionNameWithSpecialCharacters()
        {
            this.edition.EditionName = "Editia@@23++;";
            AssertValidationException(this.edition, "Edition name must not have special characters!");
        }

        [TestMethod]
        public void EditionNameTooShort()
        {
            this.edition.EditionName = "Ed";
            AssertValidationException(this.edition, "Edition name must have at least 4 characters!");
        }

        [TestMethod]

        public void EditionNameCorrect()
        {
            this.edition.EditionName = "Editia de Craciun";
            try
            {
                Validator.ValidateObject(this.edition, CreateValidationContext(this.edition), true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }
        }

        [TestMethod]
        public void EditionYearTooLow()
        {
            this.edition.EditionYear = 1300;
            AssertValidationException(this.edition, "Invalid edition year. Must be between 1900 and 2024.");
        }

        [TestMethod]
        public void EditionYearTooHigh()
        {
            this.edition.EditionYear = 2899;
            AssertValidationException(this.edition, "Invalid edition year. Must be between 1900 and 2024.");
        }

        [TestMethod]
        public void EditionYearCorrect()
        {
            this.edition.EditionYear = 2020;
            try
            {
                Validator.ValidateObject(this.edition, CreateValidationContext(this.edition), true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }
        }

        [TestMethod]
        public void NegativeNumberOfPages()
        {
            this.edition.PagesNumber = -3;
            AssertValidationException(this.edition, "Number of pages must be at least 1.");
        }

        [TestMethod]
        public void CorrectNumberOfPages()
        {
            this.edition.PagesNumber = 100;
            try
            {
                Validator.ValidateObject(this.edition, CreateValidationContext(this.edition), true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }
        }

        [TestMethod]
        public void NegativeBorrowableBooks()
        {
            this.edition.BorrowableBooks = -3;
            AssertValidationException(this.edition, "Number of borrowable books cannot be negative.");
        }

        [TestMethod]
        public void CorrectBorrowableBooks()
        {
            this.edition.BorrowableBooks = 32;
            try
            {
                Validator.ValidateObject(this.edition, CreateValidationContext(this.edition), true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }
        }

        [TestMethod]
        public void NegativeUnBorrowableBooks()
        {
            this.edition.UnBorrowableBooks = -3;
            AssertValidationException(this.edition, "Number of unborrowable books cannot be negative.");
        }

        [TestMethod]
        public void CorrectUnBorrowableBooks()
        {
            this.edition.UnBorrowableBooks = 32;
            try
            {
                Validator.ValidateObject(this.edition, CreateValidationContext(this.edition), true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }
        }

        [TestMethod]
        public void CorrectBookType()
        {
            edition.BookType = Edition.Type.Hardcover;
            try
            {
                Validator.ValidateObject(this.edition, CreateValidationContext(this.edition), true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }
        }

        [TestMethod]
        public void InvalidBookType()
        {
            edition.BookType = (Edition.Type)99;
            AssertValidationException(this.edition, "Invalid book type.");
        }

        [TestMethod]
        public void AtLeastOneBook()
        {
            edition.Book = new Book()
            {
                Title = "Amintiri din copilarie",
                Author = new List<Author>
                {
                    new Author
                    {
                        FirstName = "Ion",
                        LastName = "Creanga"
                    }
                },
                Domains = new List<BookDomain>
                {
                    new BookDomain
                    {

                    }
                }
            };
            try
            {
                Validator.ValidateObject(this.edition.Book, CreateValidationContext(this.edition.Book), true);
                Validator.ValidateObject(this.edition, CreateValidationContext(this.edition), true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }
        }

        [TestMethod]
        public void NoBook()
        {
            edition.Book = null;
            AssertValidationException(this.edition, "A edition must have at least one book!");
        }
    }
}
