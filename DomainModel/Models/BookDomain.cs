// <copyright file="BookDomain.cs" company="Transilvania University of Brasov">
// Dragomir Razvan
// </copyright>

namespace Library.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using DomainModel.CustomValidators;

    /// <summary>
    /// The <see cref="BookDomain"/> class represents a domain associated with books in the library system.
    /// </summary>
    public class BookDomain
    {
        /// <summary>
        /// Gets or sets the unique identifier for the book domain.
        /// </summary>
        public int BookDomainId { get; set; }

        /// <summary>
        /// Gets or sets the name of the book domain.
        /// </summary>
        [Required(ErrorMessage = "Domain name is required!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Domain name must have at least 3 characters!")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Domain name must not have special characters!")]
        [NoDomainInSubdomains(ErrorMessage = "Domain name must be unique within subdomains and parent domains!")]
        public string DomainName { get; set; }

        /// <summary>
        /// Gets or sets the collection of books associated with the book domain.
        /// </summary>
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();

        /// <summary>
        /// Gets or sets the collection of subdomains under the current book domain.
        /// </summary>
        public virtual ICollection<BookDomain> BookSubdomains { get; set; } = new List<BookDomain>();

        /// <summary>
        /// Gets or sets the parent domain of the current book domain.
        /// </summary>
        [NoDomainInSubdomains(ErrorMessage = "Domain name must be unique within parent domains!")]
        public virtual BookDomain ParentDomain { get; set; }
    }
}
