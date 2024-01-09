using DomainModel.CustomValidators;
using System;
using System.ComponentModel.DataAnnotations;

namespace DomainModel
{
    public class ExtendedBorrow
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A borrow is required!")]
        public virtual Borrow Borrow { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Enter a valid date!")]
        [DateRange(1900, 2100, ErrorMessage = "Borrow dates must be between 1900 and 2100.")]
        public DateTime Date { get; set; } 
    }
}
