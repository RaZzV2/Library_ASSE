using System.Collections.Generic;

namespace Library.models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }

        public virtual ICollection<BookDomain> Domains { get; set; }
        public virtual ICollection<Author> Author { get; set; } = new List<Author>();

        public virtual ICollection<Edition> Editions { get; set; } = new List<Edition>();
    }
}
