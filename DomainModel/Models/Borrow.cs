// <copyright file="Borrow.cs" company="Transilvania University of Brasov">
// Dragomir Razvan
// </copyright>

namespace DomainModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using DomainModel.CustomValidators;
    using Library.Models;

    /// <summary>
    /// The <see cref="Borrow"/> class represents a book borrowing record in the library system.
    /// </summary>
    [StartEndDateValid(ErrorMessage = "End date must be later than start date.")]
    public class Borrow
    {
        /// <summary>
        /// Gets or sets the unique identifier for the borrow record.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the reader associated with the borrow record.
        /// </summary>
        [Required(ErrorMessage = "Reader is required!")]
        public virtual Reader Reader { get; set; }

        /// <summary>
        /// Gets or sets the edition associated with the borrow record.
        /// </summary>
        [Required(ErrorMessage = "Edition is required!")]
        public virtual Edition Edition { get; set; }

        /// <summary>
        /// Gets or sets the start date of the borrow period.
        /// </summary>
        [DataType(DataType.Date, ErrorMessage = "Enter a valid date!")]
        [DateRange(1900, 2100, ErrorMessage = "Borrow dates must be between 1900 and 2100.")]
        public DateTime BorrowStartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the borrow period.
        /// </summary>
        [DataType(DataType.Date, ErrorMessage = "Enter a valid date!")]
        [DateRange(1900, 2100, ErrorMessage = "Borrow dates must be between 1900 and 2100.")]
        public DateTime BorrowEndDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the book has been returned.
        /// </summary>
        [Required(ErrorMessage = "IsReturned is required!")]
        public bool IsReturned { get; set; }

        /// <summary>
        /// Gets or sets the collection of extended borrow records associated with the borrow record.
        /// </summary>
        public virtual ICollection<ExtendedBorrow> ExtendedBorrows { get; set; } = new List<ExtendedBorrow>();
    }
}
