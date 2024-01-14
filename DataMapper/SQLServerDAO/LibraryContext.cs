using DomainModel;
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

        public DbSet<Edition> Editions { get; set; }
        public DbSet<Reader> Readers { get; set; }
        public DbSet<Borrow> Borrows { get; set; }
        public DbSet<ExtendedBorrow> ExtendedBorrows { get; set; } 


    }
}
