namespace DomainModel.CustomValidationHelpers
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Helper class for performing model validation assertions in unit tests.
    /// </summary>
    public static class ModelValidationHelper
    {
        /// <summary>
        /// Asserts that a validation exception is thrown with the expected error message.
        /// </summary>
        /// <typeparam name="T">The type of the model instance to validate.</typeparam>
        /// <param name="instance">The instance of the model to validate.</param>
        /// <param name="expectedErrorMessage">The expected error message in case of validation failure.</param>
        public static void AssertValidationException<T>(T instance, string expectedErrorMessage)
        {
            var validationContext = new ValidationContext(instance, null, null);
            var exception = Assert.ThrowsException<ValidationException>(() =>
                Validator.ValidateObject(instance, validationContext, true));
            Assert.AreEqual(expectedErrorMessage, exception.Message);
        }
    }
}
