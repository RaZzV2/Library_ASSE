namespace DataMapper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Library.models;
    using Library.Models;

    /// <summary>
    /// The <see cref="BookDAO"/> class provides Data Access Object (DAO) functionality
    /// for interacting with the "Book" entity in the SQL Server database.
    /// </summary>
    public class BookDAO : IBookIDAO
    {
        /// <summary>
        /// Adds a new book to the database.
        /// </summary>
        /// <param name="t">The <see cref="Book"/> object to be added.</param>
        public void Add(Book t)
        {
            using (var context = new LibraryContext())
            {
                context.Books.Add(t);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes an existing book from the database.
        /// </summary>
        /// <param name="t">The <see cref="Book"/> object to be deleted.</param>
        public void Delete(Book t)
        {
            using (var context = new LibraryContext())
            {
                var newBook = new Book { BookId = t.BookId };
                context.Books.Attach(newBook);
                context.Books.Remove(newBook);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Retrieves all books from the database.
        /// </summary>
        /// <returns>A <see cref="List{T}"/> of <see cref="Book"/> objects.</returns>
        public List<Book> GetAll()
        {
            using (var context = new LibraryContext())
            {
                return context.Books.ToList();
            }
        }

        /// <summary>
        /// Retrieves a book from the database based on the provided ID.
        /// </summary>
        /// <param name="id">The unique identifier of the book to retrieve.</param>
        /// <returns>The <see cref="Book"/> object if found, otherwise null.</returns>
        public Book GetById(int id)
        {
            using (var context = new LibraryContext())
            {
                return context.Books.Where(book => book.BookId == id).SingleOrDefault();
            }
        }

        /// <summary>
        /// Updates an existing book in the database.
        /// </summary>
        /// <param name="t">The <see cref="Book"/> object with updated information.</param>
        public void Update(Book t)
        {
            using (var context = new LibraryContext())
            {
                var existingBook = context.Books.Find(t.BookId);

                if (existingBook != null)
                {
                    context.Entry(existingBook).CurrentValues.SetValues(t);
                    context.SaveChanges();
                }
                else
                {
                    throw new InvalidOperationException($"Book with ID {t.BookId} not found");
                }
            }
        }
    }
}
