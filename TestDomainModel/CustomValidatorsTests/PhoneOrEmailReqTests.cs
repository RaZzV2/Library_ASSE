using DomainModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TestDomainModel.CustomValidatorsTests
{
    [TestClass]
    public class PhoneOrEmailReqTests
    {
        [TestMethod]
        public void EitherPhoneNumberOrEmailAddressIsProvided_ReturnsSuccess()
        {
            var reader = new Reader { PhoneNumber = "123456789", EmailAddress = "" };
            var validator = new PhoneOrEmailReq();

            var validationResult = validator.GetValidationResult(reader, new ValidationContext(reader));

            Assert.AreEqual(ValidationResult.Success, validationResult);
        }

        [TestMethod]
        public void NeitherPhoneNumberNorEmailAddressIsProvided_ReturnsError()
        {
            var reader = new Reader { PhoneNumber = "", EmailAddress = "" };
            var validator = new PhoneOrEmailReq();

            var validationResult = validator.GetValidationResult(reader, new ValidationContext(reader));

            Assert.AreNotEqual(ValidationResult.Success, validationResult);
            Assert.AreEqual("Either phone number or email address is required.", validationResult.ErrorMessage);
        }
    }
}
