namespace TestDomainModel.CustomValidatorsTests
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using DomainModel.CustomValidators;
    using Library.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test class for validating the EnsureAtLeastOneElement custom validator.
    /// </summary>
    [TestClass]
    public class EnsureAtLeastOneElementTests
    {
        /// <summary>
        /// Validates that a null collection returns an error.
        /// </summary>
        [TestMethod]
        public void NullCollection_ReturnsError()
        {
            var validator = new EnsureAtLeastOneElement();
            List<BookDomain> nullCollection = null;

            var validationResult = validator.GetValidationResult(nullCollection, new ValidationContext(new List<BookDomain>()));

            Assert.AreNotEqual(ValidationResult.Success, validationResult);
            Assert.AreEqual("The collection must contain at least one element.", validationResult.ErrorMessage);
        }

        /// <summary>
        /// Validates that an empty collection returns an error.
        /// </summary>
        [TestMethod]
        public void EmptyCollection_ReturnsError()
        {
            var validator = new EnsureAtLeastOneElement();
            var emptyCollection = new List<BookDomain>();

            var validationResult = validator.GetValidationResult(emptyCollection, new ValidationContext(new List<BookDomain>()));

            Assert.AreNotEqual(ValidationResult.Success, validationResult);
            Assert.AreEqual("The collection must contain at least one element.", validationResult.ErrorMessage);
        }

        /// <summary>
        /// Validates that a non-empty collection returns success.
        /// </summary>
        [TestMethod]
        public void NonEmptyCollection_ReturnsSuccess()
        {
            var validator = new EnsureAtLeastOneElement();
            var nonEmptyCollection = new List<int> { 42 };

            var validationResult = validator.GetValidationResult(nonEmptyCollection, new ValidationContext(nonEmptyCollection));

            Assert.AreEqual(ValidationResult.Success, validationResult);
        }

        /// <summary>
        /// Validates that a non-collection object returns an error.
        /// </summary>
        [TestMethod]
        public void NonCollectionObject_ReturnsError()
        {
            var validator = new EnsureAtLeastOneElement();
            var nonCollectionObject = "NotACollection";

            var validationResult = validator.GetValidationResult(nonCollectionObject, new ValidationContext(nonCollectionObject));

            Assert.AreNotEqual(ValidationResult.Success, validationResult);
            Assert.AreEqual("The property must be a collection.", validationResult.ErrorMessage);
        }

        /// <summary>
        /// Validates that a single-element collection returns success.
        /// </summary>
        [TestMethod]
        public void SingleElementCollection_ReturnsSuccess()
        {
            var validator = new EnsureAtLeastOneElement();
            var singleElementCollection = new List<string> { "e1" };

            var validationResult = validator.GetValidationResult(singleElementCollection, new ValidationContext(singleElementCollection));

            Assert.AreEqual(ValidationResult.Success, validationResult);
        }

        /// <summary>
        /// Validates that a multiple-elements collection returns success.
        /// </summary>
        [TestMethod]
        public void MultipleElementsCollection_ReturnsSuccess()
        {
            var validator = new EnsureAtLeastOneElement();
            var multipleElementsCollection = new List<string> { "e1", "e2", "e3" };

            var validationResult = validator.GetValidationResult(multipleElementsCollection, new ValidationContext(multipleElementsCollection));

            Assert.AreEqual(ValidationResult.Success, validationResult);
        }
    }
}
