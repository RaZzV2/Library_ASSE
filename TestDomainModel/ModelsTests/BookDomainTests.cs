using DomainModel.CustomValidationHelpers;
using Library.models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks.Constraints;
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TestDomainModel.ModelsTests
{
   
    [TestClass]
    public class BookDomainTests
    {
       
        private BookDomain bookDomain;

        private ValidationContext CreateValidationContext(object instance)
        {
            return new ValidationContext(instance, null, null);
        }
        private void AssertValidationException<T>(T instance, string expectedErrorMessage)
        {
            ModelValidationHelper.AssertValidationException(instance, expectedErrorMessage);
        }

        [TestInitialize]
        public void setUp()
        {
            bookDomain = new BookDomain
            {
                DomainName = "Matematica"
            };
        }

        [TestMethod]
        public void DomainNameEmpty()
        {
            this.bookDomain.DomainName = string.Empty;
            AssertValidationException(this.bookDomain, "Domain name is required!");
        }

        [TestMethod]
        public void DomainNameNull()
        {
            this.bookDomain.DomainName = null;
            AssertValidationException(this.bookDomain, "Domain name is required!");
        }

        [TestMethod]
        public void DomainNameWithSpecialCharacters()
        {
            this.bookDomain.DomainName = "M@T3M++";
            AssertValidationException(this.bookDomain, "Domain name must not have special characters!");
        }

        [TestMethod]
        public void DomainNameTooShort()
        {
            this.bookDomain.DomainName = "sa";
            AssertValidationException(this.bookDomain, "Domain name must have at least 3 characters!");
        }

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
                Validator.ValidateObject(domain, CreateValidationContext(domain), true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }
        }

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
                        DomainName = "Exampl2e"
                    }
                }
            };
            try
            {
                Validator.ValidateObject(this.bookDomain, CreateValidationContext(this.bookDomain), true);
            }
            catch (ValidationException ex)
            {
                Assert.Fail($"An exception has been thrown!: {ex.Message}");
            }
        }

        [TestMethod]
        public void ChildsEqualsDomains()
        {
            bookDomain = new BookDomain
            {
                DomainName = "Example",
                BookSubdomains = new List<BookDomain>
                {
                    new BookDomain
                    {
                        DomainName = "Example"
                    }
                }
            };
            AssertValidationException(this.bookDomain, "Domain name must be unique within subdomains!");
        }

        [TestMethod]
        public void ParentsEqualsDomains()
        {
            this.bookDomain = new BookDomain
            {
                DomainName = "ExampleDomain",
                ParentDomain = new BookDomain { DomainName = "Genul 1", ParentDomain = new BookDomain { DomainName = "ExampleDomain" } },
            };
            AssertValidationException(this.bookDomain, "Circular dependency detected! Domain name must be unique within parent domains!");
        }

    }
}
