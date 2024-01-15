// <copyright file="Author.cs" company="Transilvania University of Brasov">
// Dragomir Razvan
// </copyright>

namespace Library.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The <see cref="Author"/> class represents an author in the library system.
    /// </summary>
    public class Author
    {
        /// <summary>
        /// Gets or sets the unique identifier for the author.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the first name of the author.
        /// </summary>
        [Required(ErrorMessage = "First name is required!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "First name must have at least 3 characters!")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "First name must not have special characters!")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the author.
        /// </summary>
        [Required(ErrorMessage = "Last name is required!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Last name must have at least 3 characters!")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Last name must not have special characters!")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the collection of books authored by this author.
        /// </summary>
        public virtual ICollection<Book> Books { get; set; }
    }
}
