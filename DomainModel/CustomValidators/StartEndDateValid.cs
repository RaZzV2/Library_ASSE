// <copyright file="StartEndDateValid.cs" company="Transilvania University of Brasov">
// Dragomir Razvan
// </copyright>

namespace DomainModel.CustomValidators
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The <see cref="StartEndDateValid"/> attribute is a custom validation attribute for ensuring that
    /// the end date of a Borrow object is later than its start date.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class StartEndDateValid : ValidationAttribute
    {
        /// <summary>
        /// Validates whether the end date of a Borrow object is later than its start date.
        /// </summary>
        /// <param name="value">The value of the object to validate.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>A <see cref="ValidationResult"/> indicating whether the validation was successful.</returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is Borrow borrow)
            {
                var startDate = borrow.BorrowStartDate;
                var endDate = borrow.BorrowEndDate;

                if (endDate < startDate)
                {
                    return new ValidationResult("End date must be later than start date.");
                }

                return ValidationResult.Success;
            }

            return new ValidationResult("Wrong object type!");
        }
    }
}
