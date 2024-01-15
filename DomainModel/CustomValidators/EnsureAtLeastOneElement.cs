// <copyright file="EnsureAtLeastOneElement.cs" company="Transilvania University of Brasov">
// Dragomir Razvan
// </copyright>

namespace DomainModel.CustomValidators
{
    using System;
    using System.Collections;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The <see cref="EnsureAtLeastOneElement"/> attribute is a custom validation attribute for ensuring that a collection property
    /// contains at least one element.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class EnsureAtLeastOneElement : ValidationAttribute
    {
        /// <summary>
        /// Validates whether the specified collection contains at least one element.
        /// </summary>
        /// <param name="value">The collection to validate.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>A <see cref="ValidationResult"/> indicating whether the validation was successful.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
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
