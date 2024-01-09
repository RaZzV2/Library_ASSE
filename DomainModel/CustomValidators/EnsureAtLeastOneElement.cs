using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace DomainModel.CustomValidators
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class EnsureAtLeastOneElement : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value == null)
            {
                return new ValidationResult("The collection must contain at least one element.");
            }

            if (value is ICollection)
            {
                IEnumerable testObject = value as IEnumerable;
                if (testObject == null || !testObject.GetEnumerator().MoveNext())
                {
                    return new ValidationResult("The collection must contain at least one element.");
                }
            }
            else
            {
                return new ValidationResult("The property must be a collection.");
            }

            return ValidationResult.Success;
        }
    }
}
