namespace TestDomainModel.ModelsTests
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using DomainModel.CustomValidationHelpers;
    using Library.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test class for validating the BookDomain model.
    /// </summary>
    [TestClass]
    public class BookDomainTests
    {
        private BookDomain bookDomain;

        /// <summary>
        /// Set up method to initialize common objects for tests.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            this.bookDomain = new BookDomain
            {
                DomainName = "Matematica",
            };
        }

        /// <summary>
        /// Validates that the domain name is empty and returns an error.
        /// </summary>
        [TestMethod]
        public void DomainNameEmpty()
        {
            this.bookDomain.DomainName = string.Empty;
            this.AssertValidationException(this.bookDomain, "Domain name is required!");
        }

        /// <summary>
        /// Validates that the domain name is null and returns an error.
        /// </summary>
        [TestMethod]
        public void DomainNameNull()
        {
            this.bookDomain.DomainName = null;
            this.AssertValidationException(this.bookDomain, "Domain name is required!");
        }

        /// <summary>
        /// Validates that the domain name contains special characters and returns an error.
        /// </summary>
        [TestMethod]
        public void DomainNameWithSpecialCharacters()
        {
            this.bookDomain.DomainName = "M@T3M++";
            this.AssertValidationException(this.bookDomain, "Domain name must not have special characters!");
        }

        /// <summary>
        /// Validates that the domain name is too short and returns an error.
        /// </summary>
        [TestMethod]
        public void DomainNameTooShort()
        {
            this.bookDomain.DomainName = "sa";
            this.AssertValidationException(this.bookDomain, "Domain name must have at least 3 characters!");
        }

        /// <summary>
        /// Validates that parents are not equal to domains and returns success.
        /// </summary>
        [TestMethod]
        public void ParentsNotEqualsDomains()
        {
            var domain = new BookDomain
            {
                DomainName = "ExampleDomain",
                ParentDomain = new BookDomain { DomainName = "Genul 1", ParentDomain = new BookDomain {DomainName = "Dom 2" } },
            };
            try
            {
                Validator.ValidateObject(domain, this.CreateValidationContext(domain), true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }
        }

        /// <summary>
        /// Validates that child domains are not equal and returns success.
        /// </summary>
        [TestMethod]
        public void ChildsNotEqualsDomains()
        {
            this.bookDomain = new BookDomain
            {
                DomainName = "Example",
                BookSubdomains = new List<BookDomain>
                {
                    new BookDomain
                    {
                        DomainName = "Exampl2e",
                    },
                },
            };
            try
            {
                Validator.ValidateObject(this.bookDomain, this.CreateValidationContext(this.bookDomain), true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }
        }

        /// <summary>
        /// Validates that child domains are equal and returns an error.
        /// </summary>
        [TestMethod]
        public void ChildsEqualsDomains()
        {
            this.bookDomain = new BookDomain
            {
                DomainName = "Example",
                BookSubdomains = new List<BookDomain>
                {
                    new BookDomain
                    {
                        DomainName = "Example",
                    },
                },
            };
            this.AssertValidationException(this.bookDomain, "Domain name must be unique within subdomains!");
        }

        /// <summary>
        /// Validates that parent domains are equal and returns an error.
        /// </summary>
        [TestMethod]
        public void ParentsEqualsDomains()
        {
            this.bookDomain = new BookDomain
            {
                DomainName = "ExampleDomain",
                ParentDomain = new BookDomain { DomainName = "Genul 1", ParentDomain = new BookDomain { DomainName = "ExampleDomain" } },
            };
            this.AssertValidationException(this.bookDomain, "Circular dependency detected! Domain name must be unique within parent domains!");
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
