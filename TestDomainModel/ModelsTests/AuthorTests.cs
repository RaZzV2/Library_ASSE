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
    public class AuthorTests
    {
        private Author author;
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
            author = new Author()
            {
                FirstName = "Ionel",
                LastName = "Dorel"
            };
        }

        [TestMethod]
        public void AuthorFirstNameIsEmpty()
        {
            this.author.FirstName = string.Empty;
            AssertValidationException(this.author, "First name is required!");
        }

        [TestMethod]
        public void AuthorFirstNameContainsSpecialCharacters()
        {
            this.author.FirstName = "12312@@@";
            AssertValidationException(this.author, "First name must not have special characters!");
        }

        [TestMethod]
        public void AuthorFirstNameTooShort()
        {
            this.author.FirstName = "rz";
            AssertValidationException(this.author, "First name must have at least 3 characters!");
        }

        [TestMethod]
        public void AuthorFirstNameValidLastNameTooShort()
        {
            this.author.FirstName = "razvan";
            this.author.LastName = "dr";
            AssertValidationException(this.author, "Last name must have at least 3 characters!");
        }

        [TestMethod]
        public void AuthorFirstNameValidLastNameEmpty()
        {
            this.author.FirstName = "razvan";
            this.author.LastName = string.Empty;
            AssertValidationException(this.author, "Last name is required!");
        }

        [TestMethod]
        public void AuthorFirstNameValidLastNameNull()
        {
            this.author.FirstName = "razvan";
            this.author.LastName = null;
            AssertValidationException(this.author, "Last name is required!");
        }

        [TestMethod]
        public void AuthorFirstNameValidLastNameWithSpecialCharacters()
        {
            this.author.FirstName = "razvan";
            this.author.LastName = "++33dr";
            AssertValidationException(this.author, "Last name must not have special characters!");
        }

        [TestMethod]
        public void AuthorFirstNameEmptyLastNameValid()
        {
            this.author.FirstName = string.Empty;
            this.author.LastName = "razz";
            AssertValidationException(this.author, "First name is required!");
        }

        [TestMethod]
        public void AuthorFirstNameNullLastNameValid()
        {
            this.author.FirstName = null;
            this.author.LastName = "razz";
            AssertValidationException(this.author, "First name is required!");
        }

        [TestMethod]
        public void AuthorFirstNamWithSpecialCharactersLastNameValid()
        {
            this.author.FirstName = "##+=dd";
            this.author.LastName = "razz";
            AssertValidationException(this.author, "First name must not have special characters!");
        }

        [TestMethod]
        public void AuthorFirstNameValidLastNameValid()
        {
            this.author.FirstName = "razvan";
            this.author.LastName = "dragomir";
            try
            {
                Validator.ValidateObject(this.author, CreateValidationContext(this.author), true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }
        }


        [TestMethod]
        public void AuthorFirstNameCorrect()
        {
            this.author.FirstName = "Razvan";
            try
            {
                Validator.ValidateObject(this.author, CreateValidationContext(this.author), true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }
        }

        [TestMethod]
        public void AuthorLastNameIsEmpty()
        {
            this.author.LastName = string.Empty;
            AssertValidationException(this.author, "Last name is required!");
        }

        [TestMethod]
        public void AuthorLastNameContainsSpecialCharacters()
        {
            this.author.LastName = "12312@@@";
            AssertValidationException(this.author, "Last name must not have special characters!");
        }

        [TestMethod]
        public void AuthorLastNameTooShort()
        {
            this.author.LastName = "rz";
            AssertValidationException(this.author, "Last name must have at least 3 characters!");
        }

        [TestMethod]
        public void AuthorLastNameCorrect()
        {
            this.author.LastName = "Razvan";
            try
            {
                Validator.ValidateObject(this.author, CreateValidationContext(this.author), true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }
        }

        [TestMethod]
        public void SetBooks()
        {
            List<Book> list = new List<Book>
            {
                new Book
                {
                    Title = "test"
                },
                new Book
                {
                    Title = "test2"
                }
            };
            this.author.Books = list;
            Assert.AreEqual(list,this.author.Books);
        }
    }
}
