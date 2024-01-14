namespace Library.Models
{
    using System.Data.Entity;
    using DomainModel;
    using Library.Models;

    /// <summary>
    /// The <see cref="LibraryContext"/> class represents the DbContext for the Library database.
    /// It includes DbSets for various entities such as books, book domains, authors, editions, readers,
    /// borrows, and extended borrows.
    /// </summary>
    public class LibraryContext : DbContext
    {
        /// <summary>
        /// Gets or sets the DbSet for the "Book" entity.
        /// </summary>
        public DbSet<Book> Books { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for the "BookDomain" entity.
        /// </summary>
        public DbSet<BookDomain> BookDomains { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for the "Author" entity.
        /// </summary>
        public DbSet<Author> Author { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for the "Edition" entity.
        /// </summary>
        public DbSet<Edition> Editions { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for the "Reader" entity.
        /// </summary>
        public DbSet<Reader> Readers { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for the "Borrow" entity.
        /// </summary>
        public DbSet<Borrow> Borrows { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for the "ExtendedBorrow" entity.
        /// </summary>
        public DbSet<ExtendedBorrow> ExtendedBorrows { get; set; }
    }
}
