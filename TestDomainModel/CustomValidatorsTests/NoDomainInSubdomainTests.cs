using DomainModel.CustomValidators;
using Library.models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TestDomainModel.CustomValidatorsTests
{

    [TestClass]
    public class NoDomainInSubdomainTests
    {
        [TestMethod]
        public void UniqueDomainNameInSubdomains_ReturnsSuccess()
        {
            var bookDomain = new BookDomain
            {
                DomainName = "Domain1",
                BookSubdomains = new List<BookDomain>
            {
                new BookDomain { DomainName = "Subdomain1" },
                new BookDomain { DomainName = "Subdomain2" }
            }
            };
            var validator = new NoDomainInSubdomains();

            var validationResult = validator.GetValidationResult(bookDomain, new ValidationContext(bookDomain));

            Assert.AreEqual(ValidationResult.Success, validationResult);
        }

        [TestMethod]
        public void UniqueDomainNameInSubdomains_ReturnsError()
        {
            var bookDomain = new BookDomain
            {
                DomainName = "Subdomain1",
                BookSubdomains = new List<BookDomain>
            {
                new BookDomain { DomainName = "Subdomain1" },
                new BookDomain { DomainName = "Subdomain2" }
            }
            };
            var validator = new NoDomainInSubdomains();

            var validationResult = validator.GetValidationResult(bookDomain, new ValidationContext(bookDomain));

            Assert.AreEqual("Domain name must be unique within subdomains!", validationResult.ToString());
        }

        [TestMethod]
        public void CircularDependencyInDomains_ReturnsError()
        {
            var bookDomain = new BookDomain
            {
                DomainName = "Domain1",
                ParentDomain = new BookDomain
                {
                    DomainName = "Domain2",
                    ParentDomain = new BookDomain { DomainName = "Domain1" }
                }
            };
            var validator = new NoDomainInSubdomains();

            var validationResult = validator.GetValidationResult(bookDomain, new ValidationContext(bookDomain));

            Assert.AreNotEqual(ValidationResult.Success, validationResult);
            Assert.AreEqual("Circular dependency detected! Domain name must be unique within parent domains!", validationResult.ErrorMessage);
        }

        [TestMethod]
        public void NoCircularDependencyInDomains_ReturnsSuccess()
        {
            var bookDomain = new BookDomain
            {
                DomainName = "Domain1",
                ParentDomain = new BookDomain
                {
                    DomainName = "Domain2",
                    ParentDomain = new BookDomain { DomainName = "Domain4" }
                }
            };
            var validator = new NoDomainInSubdomains();

            var validationResult = validator.GetValidationResult(bookDomain, new ValidationContext(bookDomain));

            Assert.AreEqual(ValidationResult.Success, validationResult);
        }
    }
}
