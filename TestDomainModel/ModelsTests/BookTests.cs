// <copyright file="BookTests.cs" company="Transilvania University of Brasov">
// Dragomir Razvan
// </copyright>

namespace TestDomainModel.ModelsTests
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using DomainModel.CustomValidationHelpers;
    using Library.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test class for validating the Book model.
    /// </summary>
    [TestClass]
    public class BookTests
    {
        /// <summary>
        /// Gets or sets the instance of the Book associated with this object.
        /// </summary>
        private Book book;

        /// <summary>
        /// Set up method to initialize common objects for tests.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            this.book = new Book()
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
                Editions = new List<Edition>(),
            };
        }

        /// <summary>
        /// Validates that there is at least one author and returns success.
        /// </summary>
        [TestMethod]
        public void AtLeastOneAuthor()
        {
            this.book.Author = new List<Author>
               {
                   new Author
                   {
                       FirstName = "Ion",
                       LastName = "Creanga",
                   },
               };
            try
            {
                Validator.ValidateObject(this.book, this.CreateValidationContext(this.book), true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }
        }

        /// <summary>
        /// Validates that authors collection is null and returns an error.
        /// </summary>
        [TestMethod]
        public void NullAuthors()
        {
            this.book.Author = null;
            this.AssertValidationException(this.book, "The collection must contain at least one element.");
        }

        /// <summary>
        /// Validates that authors collection is empty and returns an error.
        /// </summary>
        [TestMethod]
        public void NoAuthors()
        {
            this.book.Author = new List<Author>();
            this.AssertValidationException(this.book, "The collection must contain at least one element.");
        }

        /// <summary>
        /// Validates that the title is too short and returns an error.
        /// </summary>
        [TestMethod]
        public void TitleTooShort()
        {
            this.book.Title = "sa";
            this.AssertValidationException(this.book, "Title must have at least 4 characters!");
        }

        /// <summary>
        /// Validates that the title is null and returns an error.
        /// </summary>
        [TestMethod]
        public void TitleNull()
        {
            this.book.Title = null;
            this.AssertValidationException(this.book, "Title is required!");
        }

        /// <summary>
        /// Validates that the title is empty and returns an error.
        /// </summary>
        [TestMethod]
        public void TitleEmpty()
        {
            this.book.Title = string.Empty;
            this.AssertValidationException(this.book, "Title is required!");
        }

        /// <summary>
        /// Validates that the title contains special characters and returns an error.
        /// </summary>
        [TestMethod]
        public void TitleWithSpecialCharacters()
        {
            this.book.Title = "Amin31!!!$+@";
            this.AssertValidationException(this.book, "Title must not have special characters!");
        }

        /// <summary>
        /// Validates that the title is correct and returns success.
        /// </summary>
        [TestMethod]
        public void CorrectTitle()
        {
            this.book.Title = "Amintiri din copilarie";
            try
            {
                Validator.ValidateObject(this.book, this.CreateValidationContext(this.book), true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }
        }

        /// <summary>
        /// Validates that domains collection is null and returns an error.
        /// </summary>
        [TestMethod]
        public void NoDomains()
        {
            this.book.Domains = null;
            this.AssertValidationException(this.book, "The collection must contain at least one element.");
        }

        /// <summary>
        /// Validates that there is at least one domain and returns success.
        /// </summary>
        [TestMethod]
        public void AtLeastOneDomain()
        {
            this.book.Domains = new List<BookDomain>
            {
                new BookDomain(),
            };
            try
            {
                Validator.ValidateObject(this.book, this.CreateValidationContext(this.book), true);
                Validator.ValidateObject(this.book.Domains, this.CreateValidationContext(this.book.Domains), true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }
        }

        /// <summary>
        /// Sets the domains for the book and asserts equality.
        /// </summary>
        [TestMethod]
        public void SetDomains()
        {
            var list = new List<BookDomain>
            {
                new BookDomain
                {
                    DomainName = "Test",
                },
                new BookDomain
                {
                    DomainName = "Test3",
                },
            };
            this.book.Domains = list;
            Assert.AreEqual(this.book.Domains, list);
        }

        /// <summary>
        /// Sets the authors for the book and asserts equality.
        /// </summary>
        [TestMethod]
        public void SetAuthors()
        {
            var list = new List<Author>
            {
                new Author
                {
                    FirstName = "Test",
                    LastName = "Creanga",
                },
            };
            this.book.Author = list;
            Assert.AreEqual(this.book.Author, list);
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
