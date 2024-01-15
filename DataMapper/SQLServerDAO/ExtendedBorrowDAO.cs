// <copyright file="ExtendedBorrowDAO.cs" company="Transilvania University Of Brasov">
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
    /// The <see cref="ExtendedBorrowDAO"/> class provides Data Access Object (DAO) functionality
    /// for interacting with the "ExtendedBorrow" entity in the SQL Server database.
    /// </summary>
    internal class ExtendedBorrowDAO : IExtendedBorrowIDAO
    {
        /// <summary>
        /// Adds a new extended borrow record to the database.
        /// </summary>
        /// <param name="t">The <see cref="ExtendedBorrow"/> object to be added.</param>
        public void Add(ExtendedBorrow t)
        {
            using (var context = new LibraryContext())
            {
                context.ExtendedBorrows.Add(t);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes an existing extended borrow record from the database.
        /// </summary>
        /// <param name="t">The <see cref="ExtendedBorrow"/> object to be deleted.</param>
        public void Delete(ExtendedBorrow t)
        {
            using (var context = new LibraryContext())
            {
                var newExtendedBorrow = new ExtendedBorrow { Id = t.Id };
                context.ExtendedBorrows.Attach(newExtendedBorrow);
                context.ExtendedBorrows.Remove(newExtendedBorrow);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Retrieves all extended borrow records from the database.
        /// </summary>
        /// <returns>A <see cref="List{T}"/> of <see cref="ExtendedBorrow"/> objects.</returns>
        public List<ExtendedBorrow> GetAll()
        {
            using (var context = new LibraryContext())
            {
                return context.ExtendedBorrows.ToList();
            }
        }

        /// <summary>
        /// Retrieves an extended borrow record from the database based on the provided ID.
        /// </summary>
        /// <param name="id">The unique identifier of the extended borrow record to retrieve.</param>
        /// <returns>The <see cref="ExtendedBorrow"/> object if found, otherwise null.</returns>
        public ExtendedBorrow GetById(int id)
        {
            using (var context = new LibraryContext())
            {
                return context.ExtendedBorrows.Where(extendedBorrow => extendedBorrow.Id == id).SingleOrDefault();
            }
        }

        /// <summary>
        /// Updates an existing extended borrow record in the database.
        /// </summary>
        /// <param name="t">The <see cref="ExtendedBorrow"/> object with updated information.</param>
        public void Update(ExtendedBorrow t)
        {
            using (var context = new LibraryContext())
            {
                var existingExtendedBorrow = context.ExtendedBorrows.Find(t.Id);

                if (existingExtendedBorrow != null)
                {
                    context.Entry(existingExtendedBorrow).CurrentValues.SetValues(t);
                    context.SaveChanges();
                }
                else
                {
                    throw new InvalidOperationException($"Extended Borrow with ID {t.Id} not found");
                }
            }
        }
    }
}
