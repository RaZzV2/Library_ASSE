namespace DataMapper.SQLServerDAO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DomainModel;
    using Library.Models;

    /// <summary>
    /// The <see cref="BorrowDAO"/> class provides Data Access Object (DAO) functionality
    /// for interacting with the "Borrow" entity in the SQL Server database.
    /// </summary>
    internal class BorrowDAO : IBorrowIDAO
    {
        /// <summary>
        /// Adds a new borrow record to the database.
        /// </summary>
        /// <param name="t">The <see cref="Borrow"/> object to be added.</param>
        public void Add(Borrow t)
        {
            using (var context = new LibraryContext())
            {
                context.Borrows.Add(t);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes an existing borrow record from the database.
        /// </summary>
        /// <param name="t">The <see cref="Borrow"/> object to be deleted.</param>
        public void Delete(Borrow t)
        {
            using (var context = new LibraryContext())
            {
                var newBorrow = new Borrow { Id = t.Id };
                context.Borrows.Attach(newBorrow);
                context.Borrows.Remove(newBorrow);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Retrieves all borrow records from the database.
        /// </summary>
        /// <returns>A <see cref="List{T}"/> of <see cref="Borrow"/> objects.</returns>
        public List<Borrow> GetAll()
        {
            using (var context = new LibraryContext())
            {
                return context.Borrows.ToList();
            }
        }

        /// <summary>
        /// Retrieves a borrow record from the database based on the provided ID.
        /// </summary>
        /// <param name="id">The unique identifier of the borrow record to retrieve.</param>
        /// <returns>The <see cref="Borrow"/> object if found, otherwise null.</returns>
        public Borrow GetById(int id)
        {
            using (var context = new LibraryContext())
            {
                return context.Borrows.Where(borrow => borrow.Id == id).SingleOrDefault();
            }
        }

        /// <summary>
        /// Updates an existing borrow record in the database.
        /// </summary>
        /// <param name="t">The <see cref="Borrow"/> object with updated information.</param>
        public void Update(Borrow t)
        {
            using (var context = new LibraryContext())
            {
                var existingBorrow = context.Borrows.Find(t.Id);

                if (existingBorrow != null)
                {
                    context.Entry(existingBorrow).CurrentValues.SetValues(t);
                    context.SaveChanges();
                }
                else
                {
                    throw new InvalidOperationException($"Borrow with ID {t.Id} not found");
                }
            }
        }
    }
}
