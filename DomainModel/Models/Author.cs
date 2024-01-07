using DomainModel.CustomValidators;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.models
{
    public class Author
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "First name must have at least 3 characters!")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "First name must not have special characters!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Last name must have at least 3 characters!")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Last name must not have special characters!")]
        public string LastName { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}
