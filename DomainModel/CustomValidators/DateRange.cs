namespace DomainModel.CustomValidators
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [AttributeUsage(AttributeTargets.Property)]
    public class DateRange : ValidationAttribute
    {
        private readonly int minYear;
        private readonly int maxYear;

        /// <summary>
        /// Initializes a new instance of the <see cref="DateRange"/> class.
        /// </summary>
        /// <param name="minYear"></param>
        /// <param name="maxYear"></param>
        public DateRange(int minYear, int maxYear)
        {
            this.minYear = minYear;
            this.maxYear = maxYear;
        }

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
