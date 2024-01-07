using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.CustomValidationHelpers
{
    public static class ModelValidationHelper
    {
        public static void AssertValidationException<T>(T instance, string expectedErrorMessage)
        {
            var validationContext = new ValidationContext(instance, null, null);
            var exception = Assert.ThrowsException<ValidationException>(() =>
                Validator.ValidateObject(instance, validationContext, true));
            Assert.AreEqual(expectedErrorMessage, exception.Message);
        }
    }
}
