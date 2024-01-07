using DomainModel.CustomValidators;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.models
{
    public class Book
    {
        public int BookId { get; set; }

        [Required(ErrorMessage = "Title is required!")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Title must have at least 4 characters!")]
        [RegularExpression("^[a-zA-Z0-9 ]+$", ErrorMessage = "Title must not have special characters!")]
        public string Title { get; set; }


        [EnsureAtLeastOneElement(ErrorMessage = "The book must have at least one domain!")]
        public virtual ICollection<BookDomain> Domains { get; set; }

        [EnsureAtLeastOneElement(ErrorMessage = "The book must have at least one author!")]
        public virtual ICollection<Author> Author { get; set; } = new List<Author>();

        public virtual ICollection<Edition> Editions { get; set; } = new List<Edition>();
    }
}
