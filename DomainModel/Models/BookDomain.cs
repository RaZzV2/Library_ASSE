using System.Collections.Generic;

namespace Library.models
{
    public class BookDomain
    {
        public int BookDomainId { get; set; }
        public string DomainName { get; set; }
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
        public virtual ICollection<BookDomain> BookSubdomains { get; set; }  = new List<BookDomain>();
    }
}
