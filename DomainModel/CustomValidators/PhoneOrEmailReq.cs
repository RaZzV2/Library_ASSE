using System;
using System.ComponentModel.DataAnnotations;

namespace DomainModel
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PhoneOrEmailReq : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var reader = (Reader)validationContext.ObjectInstance;

            if (string.IsNullOrWhiteSpace(reader.PhoneNumber) && string.IsNullOrWhiteSpace(reader.EmailAddress))
            {
                return new ValidationResult("Either phone number or email address is required.");
            }
            
            return ValidationResult.Success;
        }
    }
}
