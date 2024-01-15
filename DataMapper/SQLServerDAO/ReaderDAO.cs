// <copyright file="ReaderDAO.cs" company="Transilvania University Of Brasov">
// Dragomir Razvan
// </copyright>

namespace DataMapper.SQLServerDAO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DomainModel;
    using Library.Models;

    /// <summary>
    /// The <see cref="ReaderDAO"/> class provides Data Access Object (DAO) functionality
    /// for interacting with the "Reader" entity in the SQL Server database.
    /// </summary>
    public class ReaderDAO : IReaderIDAO
    {
        /// <summary>
        /// Adds a new reader to the database.
        /// </summary>
        /// <param name="t">The <see cref="Reader"/> object to be added.</param>
        public void Add(Reader t)
        {
            using (var context = new LibraryContext())
            {
                context.Readers.Add(t);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes an existing reader from the database.
        /// </summary>
        /// <param name="t">The <see cref="Reader"/> object to be deleted.</param>
        public void Delete(Reader t)
        {
            using (var context = new LibraryContext())
            {
                var newReader = new Reader { ReaderId = t.ReaderId };
                context.Readers.Attach(newReader);
                context.Readers.Remove(newReader);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Retrieves all readers from the database.
        /// </summary>
        /// <returns>A <see cref="List{T}"/> of <see cref="Reader"/> objects.</returns>
        public List<Reader> GetAll()
        {
            using (var context = new LibraryContext())
            {
                return context.Readers.ToList();
            }
        }

        /// <summary>
        /// Retrieves a reader from the database based on the provided ID.
        /// </summary>
        /// <param name="id">The unique identifier of the reader to retrieve.</param>
        /// <returns>The <see cref="Reader"/> object if found, otherwise null.</returns>
        public Reader GetById(int id)
        {
            using (var context = new LibraryContext())
            {
                return context.Readers.Where(reader => reader.ReaderId == id).SingleOrDefault();
            }
        }

        /// <summary>
        /// Updates an existing reader in the database.
        /// </summary>
        /// <param name="t">The <see cref="Reader"/> object with updated information.</param>
        public void Update(Reader t)
        {
            using (var context = new LibraryContext())
            {
                var existingReader = context.Books.Find(t.ReaderId);

                if (existingReader != null)
                {
                    context.Entry(existingReader).CurrentValues.SetValues(t);
                    context.SaveChanges();
                }
                else
                {
                    throw new InvalidOperationException($"Reader with ID {t.ReaderId} not found");
                }
            }
        }
    }
   }
