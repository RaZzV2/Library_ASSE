namespace Library.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The <see cref="Edition"/> class represents an edition of a book in the library system.
    /// </summary>
    public class Edition
    {
        /// <summary>
        /// Enumeration representing the types of book editions.
        /// </summary>
        public enum Type
        {
            /// <summary>
            /// Hardcover edition type.
            /// </summary>
            Hardcover,

            /// <summary>
            /// Paperback edition type.
            /// </summary>
            Paperback,

            /// <summary>
            /// Board edition type.
            /// </summary>
            Board,

            /// <summary>
            /// Spiral-bound edition type.
            /// </summary>
            SpiralBound,

            /// <summary>
            /// Ring-bound edition type.
            /// </summary>
            RingBound,
        }

        /// <summary>
        /// Gets or sets the unique identifier for the edition.
        /// </summary>
        public int EditionId { get; set; }

        /// <summary>
        /// Gets or sets the name of the edition.
        /// </summary>
        [Required(ErrorMessage = "Edition name is required!")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Edition name must have at least 4 characters!")]
        [RegularExpression("^[a-zA-Z0-9 ]+$", ErrorMessage = "Edition name must not have special characters!")]
        public string EditionName { get; set; }

        /// <summary>
        /// Gets or sets the year of the edition.
        /// </summary>
        [Range(1900, 2100, ErrorMessage = "Invalid edition year. Must be between 1900 and 2024.")]
        public int EditionYear { get; set; }

        /// <summary>
        /// Gets or sets the number of pages in the edition.
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "Number of pages must be at least 1.")]
        public int PagesNumber { get; set; }

        /// <summary>
        /// Gets or sets the number of borrowable books in the edition.
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "Number of borrowable books cannot be negative.")]
        public int BorrowableBooks { get; set; }

        /// <summary>
        /// Gets or sets the number of unborrowable books in the edition.
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "Number of unborrowable books cannot be negative.")]
        public int UnBorrowableBooks { get; set; }

        /// <summary>
        /// Gets or sets the type of the book edition.
        /// </summary>
        [EnumDataType(typeof(Type), ErrorMessage = "Invalid book type.")]
        public Type BookType { get; set; }

        /// <summary>
        /// Gets or sets the book associated with the edition.
        /// </summary>
        [Required(ErrorMessage = "A edition must have at least one book!")]
        public virtual Book Book { get; set; }
    }
}
