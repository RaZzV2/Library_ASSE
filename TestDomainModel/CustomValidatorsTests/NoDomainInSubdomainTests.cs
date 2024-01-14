namespace TestDomainModel.CustomValidatorsTests
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using DomainModel.CustomValidators;
    using Library.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test class for validating the NoDomainInSubdomains custom validator.
    /// </summary>
    [TestClass]
    public class NoDomainInSubdomainTests
    {
        /// <summary>
        /// Validates that having a unique domain name in subdomains returns success.
        /// </summary>
        [TestMethod]
        public void UniqueDomainNameInSubdomains_ReturnsSuccess()
        {
            var bookDomain = new BookDomain
            {
                DomainName = "Domain1",
                BookSubdomains = new List<BookDomain>
            {
                new BookDomain { DomainName = "Subdomain1" },
                new BookDomain { DomainName = "Subdomain2" },
            },
            };
            var validator = new NoDomainInSubdomains();

            var validationResult = validator.GetValidationResult(bookDomain, new ValidationContext(bookDomain));

            Assert.AreEqual(ValidationResult.Success, validationResult);
        }

        /// <summary>
        /// Validates that having a non-unique domain name in subdomains returns an error.
        /// </summary>
        [TestMethod]
        public void UniqueDomainNameInSubdomains_ReturnsError()
        {
            var bookDomain = new BookDomain
            {
                DomainName = "Subdomain1",
                BookSubdomains = new List<BookDomain>
            {
                new BookDomain { DomainName = "Subdomain1" },
                new BookDomain { DomainName = "Subdomain2" },
            },
            };
            var validator = new NoDomainInSubdomains();

            var validationResult = validator.GetValidationResult(bookDomain, new ValidationContext(bookDomain));

            Assert.AreEqual("Domain name must be unique within subdomains!", validationResult.ToString());
        }

        /// <summary>
        /// Validates that having a circular dependency in domains returns an error.
        /// </summary>
        [TestMethod]
        public void CircularDependencyInDomains_ReturnsError()
        {
            var bookDomain = new BookDomain
            {
                DomainName = "Domain1",
                ParentDomain = new BookDomain
                {
                    DomainName = "Domain2",
                    ParentDomain = new BookDomain { DomainName = "Domain1" },
                },
            };
            var validator = new NoDomainInSubdomains();

            var validationResult = validator.GetValidationResult(bookDomain, new ValidationContext(bookDomain));

            Assert.AreNotEqual(ValidationResult.Success, validationResult);
            Assert.AreEqual("Circular dependency detected! Domain name must be unique within parent domains!", validationResult.ErrorMessage);
        }

        /// <summary>
        /// Validates that having no circular dependency in domains returns success.
        /// </summary>
        [TestMethod]
        public void NoCircularDependencyInDomains_ReturnsSuccess()
        {
            var bookDomain = new BookDomain
            {
                DomainName = "Domain1",
                ParentDomain = new BookDomain
                {
                    DomainName = "Domain2",
                    ParentDomain = new BookDomain { DomainName = "Domain4" },
                },
            };
            var validator = new NoDomainInSubdomains();

            var validationResult = validator.GetValidationResult(bookDomain, new ValidationContext(bookDomain));

            Assert.AreEqual(ValidationResult.Success, validationResult);
        }
    }
}
