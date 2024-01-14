namespace DomainModel
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The <see cref="PhoneOrEmailReq"/> attribute is a custom validation attribute for ensuring that either 
    /// a phone number or an email address is provided in a Reader object.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class PhoneOrEmailReq : ValidationAttribute
    {
        /// <summary>
        /// Validates whether either a phone number or an email address is provided in a Reader object.
        /// </summary>
        /// <param name="value">The value of the object to validate.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>A <see cref="ValidationResult"/> indicating whether the validation was successful.</returns>
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
