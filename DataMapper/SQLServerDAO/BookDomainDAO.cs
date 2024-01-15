// <copyright file="BookDomainDAO.cs" company="Transilvania University Of Brasov">
// Dragomir Razvan
// </copyright>

namespace DataMapper.SQLServerDAO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Library.Models;

    /// <summary>
    /// The <see cref="BookDomainDAO"/> class provides Data Access Object (DAO) functionality
    /// for interacting with the "BookDomain" entity in the SQL Server database.
    /// </summary>
    internal class BookDomainDAO : IBookDomainIDAO
    {
        /// <summary>
        /// Adds a new book domain to the database.
        /// </summary>
        /// <param name="t">The <see cref="BookDomain"/> object to be added.</param>
        public void Add(BookDomain t)
        {
            using (var context = new LibraryContext())
            {
                context.BookDomains.Add(t);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes an existing book domain from the database.
        /// </summary>
        /// <param name="t">The <see cref="BookDomain"/> object to be deleted.</param>
        public void Delete(BookDomain t)
        {
            using (var context = new LibraryContext())
            {
                var newBookDomain = new BookDomain { BookDomainId = t.BookDomainId };
                context.BookDomains.Attach(newBookDomain);
                context.BookDomains.Remove(newBookDomain);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Retrieves all book domains from the database.
        /// </summary>
        /// <returns>A <see cref="List{T}"/> of <see cref="BookDomain"/> objects.</returns>
        public List<BookDomain> GetAll()
        {
            using (var context = new LibraryContext())
            {
                return context.BookDomains.ToList();
            }
        }

        /// <summary>
        /// Retrieves a book domain from the database based on the provided ID.
        /// </summary>
        /// <param name="id">The unique identifier of the book domain to retrieve.</param>
        /// <returns>The <see cref="BookDomain"/> object if found, otherwise null.</returns>
        public BookDomain GetById(int id)
        {
            using (var context = new LibraryContext())
            {
                return context.BookDomains.Where(bookDomain => bookDomain.BookDomainId == id).SingleOrDefault();
            }
        }

        /// <summary>
        /// Updates an existing book domain in the database.
        /// </summary>
        /// <param name="t">The <see cref="BookDomain"/> object with updated information.</param>
        public void Update(BookDomain t)
        {
            using (var context = new LibraryContext())
            {
                var existingBookDomain = context.Books.Find(t.BookDomainId);

                if (existingBookDomain != null)
                {
                    context.Entry(existingBookDomain).CurrentValues.SetValues(t);
                    context.SaveChanges();
                }
                else
                {
                    throw new InvalidOperationException($"Book Domain with ID {t.BookDomainId} not found");
                }
            }
        }
    }
}
