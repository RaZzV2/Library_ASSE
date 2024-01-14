namespace Library.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using DomainModel.CustomValidators;

    /// <summary>
    /// The <see cref="Book"/> class represents a book in the library system.
    /// </summary>
    public class Book
    {
        /// <summary>
        /// Gets or sets the unique identifier for the book.
        /// </summary>
        public int BookId { get; set; }

        /// <summary>
        /// Gets or sets the title of the book.
        /// </summary>
        [Required(ErrorMessage = "Title is required!")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Title must have at least 4 characters!")]
        [RegularExpression("^[a-zA-Z0-9 ]+$", ErrorMessage = "Title must not have special characters!")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the collection of domains associated with the book.
        /// </summary>
        [EnsureAtLeastOneElement(ErrorMessage = "The book must have at least one domain!")]
        public virtual ICollection<BookDomain> Domains { get; set; }

        /// <summary>
        /// Gets or sets the collection of authors who contributed to the book.
        /// </summary>
        [EnsureAtLeastOneElement(ErrorMessage = "The book must have at least one author!")]
        public virtual ICollection<Author> Author { get; set; } = new List<Author>();

        /// <summary>
        /// Gets or sets the collection of editions of the book.
        /// </summary>
        public virtual ICollection<Edition> Editions { get; set; } = new List<Edition>();
    }
}
