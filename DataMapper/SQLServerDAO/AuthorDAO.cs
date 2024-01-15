// <copyright file="AuthorDAO.cs" company="Transilvania University Of Brasov">
// Dragomir Razvan
// </copyright>

namespace DataMapper.SQLServerDAO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Library.Models;

    /// <summary>
    /// The <see cref="AuthorDAO"/> class provides Data Access Object (DAO) functionality
    /// for interacting with the "Author" entity in the SQL Server database.
    /// </summary>
    internal class AuthorDAO : IAuthorIDAO
    {
        /// <summary>
        /// Adds a new author to the database.
        /// </summary>
        /// <param name="t">The <see cref="Author"/> object to be added.</param>
        public void Add(Author t)
        {
            using (var context = new LibraryContext())
            {
                context.Author.Add(t);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes an existing author from the database.
        /// </summary>
        /// <param name="t">The <see cref="Author"/> object to be deleted.</param>
        public void Delete(Author t)
        {
            using (var context = new LibraryContext())
            {
                var newAuthor = new Author { Id = t.Id };
                context.Author.Attach(newAuthor);
                context.Author.Remove(newAuthor);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Retrieves an author from the database based on the provided ID.
        /// </summary>
        /// <param name="id">The unique identifier of the author to retrieve.</param>
        /// <returns>The <see cref="Author"/> object if found, otherwise null.</returns>
        public Author GetById(int id)
        {
            using (var context = new LibraryContext())
            {
                return context.Author.Where(author => author.Id == id).SingleOrDefault();
            }
        }

        /// <summary>
        /// Retrieves all authors from the database.
        /// </summary>
        /// <returns>A <see cref="List{T}"/> of <see cref="Author"/> objects.</returns>
        public List<Author> GetAll()
        {
            using (var context = new LibraryContext())
            {
                return context.Author.ToList();
            }
        }

        /// <summary>
        /// Updates an existing author in the database.
        /// </summary>
        /// <param name="t">The <see cref="Author"/> object with updated information.</param>
        public void Update(Author t)
        {
            using (var context = new LibraryContext())
            {
                var existingAuthor = context.Author.Find(t.Id);

                if (existingAuthor != null)
                {
                    context.Entry(existingAuthor).CurrentValues.SetValues(t);
                    context.SaveChanges();
                }
                else
                {
                    throw new InvalidOperationException($"Author with ID {t.Id} not found");
                }
            }
        }
    }
}
