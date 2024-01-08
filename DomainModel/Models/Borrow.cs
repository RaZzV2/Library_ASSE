using DomainModel.CustomValidators;
using Library.models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DomainModel
{
    [StartEndDateValid(ErrorMessage = "End date must be later than start date.")]
    public class Borrow
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Reader is required!")]
        public virtual Reader Reader { get; set; }

        [Required(ErrorMessage = "Edition is required!")]
        public virtual Edition Edition { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Enter a valid date!")]
        [DateRange(1900, 2100, ErrorMessage = "Borrow dates must be between 1900 and 2100.")]
        public DateTime BorrowStartDate { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Enter a valid date!")]
        [DateRange(1900, 2100, ErrorMessage = "Borrow dates must be between 1900 and 2100.")]
        public DateTime BorrowEndDate { get; set; }

        [Required(ErrorMessage = "IsReturned is required!")]
        public bool IsReturned { get; set; }

        public virtual ICollection<ExtendedBorrow> ExtendedBorrows { get; set; } = new List<ExtendedBorrow>();
    }
}
