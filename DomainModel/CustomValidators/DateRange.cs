using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.CustomValidators
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DateRange : ValidationAttribute
    {
        private readonly int _minYear;
        private readonly int _maxYear;

        public DateRange(int minYear, int maxYear)
        {
            _minYear = minYear;
            _maxYear = maxYear;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateValue)
            {
                if (dateValue.Year < _minYear || dateValue.Year > _maxYear)
                {
                    return new ValidationResult($"Date must be between {_minYear} and {_maxYear}.");
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
