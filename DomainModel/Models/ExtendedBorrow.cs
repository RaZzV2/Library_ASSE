namespace DomainModel
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using DomainModel.CustomValidators;

    /// <summary>
    /// The <see cref="ExtendedBorrow"/> class represents an extended borrow record in the library system.
    /// </summary>
    public class ExtendedBorrow
    {
        /// <summary>
        /// Gets or sets the unique identifier for the extended borrow record.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the associated borrow record for the extended borrow.
        /// </summary>
        [Required(ErrorMessage = "A borrow is required!")]
        public virtual Borrow Borrow { get; set; }

        /// <summary>
        /// Gets or sets the date of the extended borrow.
        /// </summary>
        [DataType(DataType.Date, ErrorMessage = "Enter a valid date!")]
        [DateRange(1900, 2100, ErrorMessage = "Borrow dates must be between 1900 and 2100.")]
        public DateTime Date { get; set; }
    }
}