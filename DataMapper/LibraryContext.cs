using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Reflection.Emit;

namespace Library.models
{
    public class LibraryContext : DbContext
    {
        public DbSet<Book> Books {get; set;}
        public DbSet<BookDomain> BookDomains { get; set; }
        public DbSet<Author> Author { get; set; }

    }
}
