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
    }
}
