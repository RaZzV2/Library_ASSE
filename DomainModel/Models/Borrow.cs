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
        public DateTime BorrowStartDate { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Enter a valid date!")]
        public DateTime BorrowEndDate { get; set; }

        public bool IsReturned { get; set; }

        public virtual ICollection<ExtendedBorrow> ExtendedBorrows { get; set; } = new List<ExtendedBorrow>();
    }
}
