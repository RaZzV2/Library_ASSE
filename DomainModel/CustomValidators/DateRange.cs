// <copyright file="DateRange.cs" company="Transilvania University of Brasov">
// Dragomir Razvan
// </copyright>

namespace DomainModel.CustomValidators
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The <see cref="DateRange"/> attribute is a custom validation attribute for validating that a DateTime property
    /// falls within a specified range of years.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DateRange : ValidationAttribute
    {
        /// <summary>
        /// Represents the minimum allowable year value.
        /// </summary>
        private readonly int minYear;

        /// <summary>
        /// Represents the maximum allowable year value.
        /// </summary>
        private readonly int maxYear;

        /// <summary>
        /// Initializes a new instance of the <see cref="DateRange"/> class.
        /// </summary>
        /// <param name="minYear">The minimum allowed year.</param>
        /// <param name="maxYear">The maximum allowed year.</param>
        public DateRange(int minYear, int maxYear)
        {
            this.minYear = minYear;
            this.maxYear = maxYear;
        }

        /// <summary>
        /// Validates whether the specified date falls within the allowed date range.
        /// </summary>
        /// <param name="value">The DateTime value to validate.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>A <see cref="ValidationResult"/> indicating whether the validation was successful.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateValue)
            {
                if (dateValue.Year < this.minYear || dateValue.Year > this.maxYear)
                {
                    return new ValidationResult($"Date must be between {this.minYear} and {this.maxYear}.");
                }

                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("The value must be a valid DateTime.");
            }
        }
    }
}
