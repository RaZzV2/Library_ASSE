namespace TestDomainModel.ModelsTests
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using DomainModel.CustomValidationHelpers;
    using Library.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test class for validating the Author model.
    /// </summary>
    [TestClass]
    public class AuthorTests
    {
        private Author author;

        /// <summary>
        /// Set up method to initialize common objects for tests.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            this.author = new Author()
            {
                FirstName = "Ionel",
                LastName = "Dorel",
            };
        }

        /// <summary>
        /// Validates that the first name is empty and returns an error.
        /// </summary>
        [TestMethod]
        public void AuthorFirstNameIsEmpty()
        {
            this.author.FirstName = string.Empty;
            this.AssertValidationException(this.author, "First name is required!");
        }

        /// <summary>
        /// Validates that the first name contains special characters and returns an error.
        /// </summary>
        [TestMethod]
        public void AuthorFirstNameContainsSpecialCharacters()
        {
            this.author.FirstName = "12312@@@";
            this.AssertValidationException(this.author, "First name must not have special characters!");
        }

        /// <summary>
        /// Validates that the first name is too short and returns an error.
        /// </summary>
        [TestMethod]
        public void AuthorFirstNameTooShort()
        {
            this.author.FirstName = "rz";
            this.AssertValidationException(this.author, "First name must have at least 3 characters!");
        }

        /// <summary>
        /// Validates that the first name is valid and the last name is too short, returning an error.
        /// </summary>
        [TestMethod]
        public void AuthorFirstNameValidLastNameTooShort()
        {
            this.author.FirstName = "razvan";
            this.author.LastName = "dr";
            this.AssertValidationException(this.author, "Last name must have at least 3 characters!");
        }

        /// <summary>
        /// Validates that the first name is valid and the last name is empty, returning an error.
        /// </summary>
        [TestMethod]
        public void AuthorFirstNameValidLastNameEmpty()
        {
            this.author.FirstName = "razvan";
            this.author.LastName = string.Empty;
            this.AssertValidationException(this.author, "Last name is required!");
        }

        /// <summary>
        /// Validates that the first name is valid and the last name is null, returning an error.
        /// </summary>
        [TestMethod]
        public void AuthorFirstNameValidLastNameNull()
        {
            this.author.FirstName = "razvan";
            this.author.LastName = null;
            this.AssertValidationException(this.author, "Last name is required!");
        }

        /// <summary>
        /// Validates that the first name is valid and the last name contains special characters, returning an error.
        /// </summary>
        [TestMethod]
        public void AuthorFirstNameValidLastNameWithSpecialCharacters()
        {
            this.author.FirstName = "razvan";
            this.author.LastName = "++33dr";
            this.AssertValidationException(this.author, "Last name must not have special characters!");
        }

        /// <summary>
        /// Validates that the first name is empty and the last name is valid, returning an error.
        /// </summary>
        [TestMethod]
        public void AuthorFirstNameEmptyLastNameValid()
        {
            this.author.FirstName = string.Empty;
            this.author.LastName = "razz";
            this.AssertValidationException(this.author, "First name is required!");
        }

        /// <summary>
        /// Validates that the first name is null and the last name is valid, returning an error.
        /// </summary>
        [TestMethod]
        public void AuthorFirstNameNullLastNameValid()
        {
            this.author.FirstName = null;
            this.author.LastName = "razz";
            this.AssertValidationException(this.author, "First name is required!");
        }

        /// <summary>
        /// Validates that the first name contains special characters and the last name is valid, returning an error.
        /// </summary>
        [TestMethod]
        public void AuthorFirstNamWithSpecialCharactersLastNameValid()
        {
            this.author.FirstName = "##+=dd";
            this.author.LastName = "razz";
            this.AssertValidationException(this.author, "First name must not have special characters!");
        }

        /// <summary>
        /// Validates that both first name and last name are valid and returns success.
        /// </summary>
        [TestMethod]
        public void AuthorFirstNameValidLastNameValid()
        {
            this.author.FirstName = "razvan";
            this.author.LastName = "dragomir";
            try
            {
                Validator.ValidateObject(this.author, this.CreateValidationContext(this.author), true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }
        }

        /// <summary>
        /// Validates that the author's first name is correct and returns success.
        /// </summary>
        [TestMethod]
        public void AuthorFirstNameCorrect()
        {
            this.author.FirstName = "Razvan";
            try
            {
                Validator.ValidateObject(this.author, this.CreateValidationContext(this.author), true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }
        }

        /// <summary>
        /// Validates that the last name is empty and returns an error.
        /// </summary>
        [TestMethod]
        public void AuthorLastNameIsEmpty()
        {
            this.author.LastName = string.Empty;
            this.AssertValidationException(this.author, "Last name is required!");
        }

        /// <summary>
        /// Validates that the last name contains special characters and returns an error.
        /// </summary>
        [TestMethod]
        public void AuthorLastNameContainsSpecialCharacters()
        {
            this.author.LastName = "12312@@@";
            this.AssertValidationException(this.author, "Last name must not have special characters!");
        }

        /// <summary>
        /// Validates that the last name is too short and returns an error.
        /// </summary>
        [TestMethod]
        public void AuthorLastNameTooShort()
        {
            this.author.LastName = "rz";
            this.AssertValidationException(this.author, "Last name must have at least 3 characters!");
        }

        /// <summary>
        /// Validates that the last name is correct and returns success.
        /// </summary>
        [TestMethod]
        public void AuthorLastNameCorrect()
        {
            this.author.LastName = "Razvan";
            try
            {
                Validator.ValidateObject(this.author, this.CreateValidationContext(this.author), true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }
        }

        /// <summary>
        /// Sets books for the author and asserts that they are equal.
        /// </summary>
        [TestMethod]
        public void SetBooks()
        {
            List<Book> list = new List<Book>
            {
                new Book
                {
                    Title = "test",
                },
                new Book
                {
                    Title = "test2",
                },
            };
            this.author.Books = list;
            Assert.AreEqual(list,this.author.Books);
        }

        /// <summary>
        /// Creates a validation context for the specified instance.
        /// </summary>
        private ValidationContext CreateValidationContext(object instance)
        {
            return new ValidationContext(instance, null, null);
        }

        /// <summary>
        /// Asserts that a validation exception is thrown for the specified instance and expected error message.
        /// </summary>
        private void AssertValidationException<T>(T instance, string expectedErrorMessage)
        {
            ModelValidationHelper.AssertValidationException(instance, expectedErrorMessage);
        }
    }
}
