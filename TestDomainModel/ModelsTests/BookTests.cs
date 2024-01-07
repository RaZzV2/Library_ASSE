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
    public class BookTests
    {
        private Book book;

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
            book = new Book()
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
                },
                Editions = new List<Edition>()
            };
        }

        [TestMethod]
        public void AtLeastOneAuthor()
        {
            this.book.Author = new List<Author>
               {
                   new Author
                   {
                       FirstName = "Ion",
                       LastName = "Creanga"
                   },
               };
            try
            {
                Validator.ValidateObject(this.book, CreateValidationContext(this.book), true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }
        }

        [TestMethod]
        public void NullAuthors()
        {
            this.book.Author = null;
            AssertValidationException(this.book, "The book must have at least one author!");
        }

        [TestMethod]
        public void NoAuthors()
        {
            this.book.Author = new List<Author>();
            AssertValidationException(this.book, "The book must have at least one author!");
        }

        [TestMethod]
        public void TitleTooShort()
        {
            this.book.Title = "sa";
            AssertValidationException(this.book, "Title must have at least 4 characters!");
        }

        [TestMethod]
        public void TitleNull()
        {
            this.book.Title = null;
            AssertValidationException(this.book, "Title is required!");
        }

        [TestMethod]
        public void TitleEmpty()
        {
            this.book.Title = string.Empty;
            AssertValidationException(this.book, "Title is required!");
        }

        [TestMethod]
        public void TitleWithSpecialCharacters()
        {
            this.book.Title = "Amin31!!!$+@";
            AssertValidationException(this.book, "Title must not have special characters!");
        }

        [TestMethod]
        public void CorrectTitle()
        {
            this.book.Title = "Amintiri din copilarie";
            try
            {
                Validator.ValidateObject(this.book, CreateValidationContext(this.book), true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }
        }

        [TestMethod]
        public void NoDomains()
        {
            this.book.Domains = null;
            AssertValidationException(this.book, "The book must have at least one domain!");
        }

        [TestMethod]
        public void AtLeastOneDomain()
        {
            this.book.Domains = new List<BookDomain> {
                new BookDomain()
            };
            try
            {
                Validator.ValidateObject(this.book, CreateValidationContext(this.book), true);
                Validator.ValidateObject(this.book.Domains, CreateValidationContext(this.book.Domains), true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }
        }
    }
}
