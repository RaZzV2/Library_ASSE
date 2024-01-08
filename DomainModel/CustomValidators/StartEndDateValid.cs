using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.CustomValidators
{
    [AttributeUsage(AttributeTargets.Class)]
    public class StartEndDateValid : ValidationAttribute
    {
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
