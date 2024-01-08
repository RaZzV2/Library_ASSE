using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace DomainModel.CustomValidators
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class EnsureAtLeastOneElement : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var testObject = value as IEnumerable;
            if(testObject == null || !testObject.GetEnumerator().MoveNext())
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
