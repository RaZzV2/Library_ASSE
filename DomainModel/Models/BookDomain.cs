using DomainModel.CustomValidators;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.models
{
    public class BookDomain
    {
        public int BookDomainId { get; set; }

        [Required(ErrorMessage = "Domain name is required!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Domain name must have at least 3 characters!")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Domain name must not have special characters!")]
        [NoDomainInSubdomains(ErrorMessage = "Domain name must be unique within subdomains and parent domains!")]
        public string DomainName { get; set; }
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();

        public virtual ICollection<BookDomain> BookSubdomains { get; set; }  = new List<BookDomain>();

        [NoDomainInSubdomains(ErrorMessage = "Domain name must be unique within parent domains!")]
        public virtual BookDomain ParentDomain { get; set; }
    }
}
