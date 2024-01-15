// <copyright file="EditionDAO.cs" company="Transilvania University Of Brasov">
// Dragomir Razvan
// </copyright>

namespace DataMapper.SQLServerDAO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Library.Models;

    /// <summary>
    /// The <see cref="EditionDAO"/> class provides Data Access Object (DAO) functionality
    /// for interacting with the "Edition" entity in the SQL Server database.
    /// </summary>
    internal class EditionDAO : IEditionIDAO
    {
        /// <summary>
        /// Adds a new edition to the database.
        /// </summary>
        /// <param name="t">The <see cref="Edition"/> object to be added.</param>
        public void Add(Edition t)
        {
            using (var context = new LibraryContext())
            {
                context.Editions.Add(t);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes an existing edition from the database.
        /// </summary>
        /// <param name="t">The <see cref="Edition"/> object to be deleted.</param>
        public void Delete(Edition t)
        {
            using (var context = new LibraryContext())
            {
                var newEdition = new Edition { EditionId = t.EditionId };
                context.Editions.Attach(newEdition);
                context.Editions.Remove(newEdition);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Retrieves all editions from the database.
        /// </summary>
        /// <returns>A <see cref="List{T}"/> of <see cref="Edition"/> objects.</returns>
        public List<Edition> GetAll()
        {
            using (var context = new LibraryContext())
            {
                return context.Editions.ToList();
            }
        }

        /// <summary>
        /// Retrieves an edition from the database based on the provided ID.
        /// </summary>
        /// <param name="id">The unique identifier of the edition to retrieve.</param>
        /// <returns>The <see cref="Edition"/> object if found, otherwise null.</returns>
        public Edition GetById(int id)
        {
            using (var context = new LibraryContext())
            {
                return context.Editions.Where(edition => edition.EditionId == id).SingleOrDefault();
            }
        }

        /// <summary>
        /// Updates an existing edition in the database.
        /// </summary>
        /// <param name="t">The <see cref="Edition"/> object with updated information.</param>
        public void Update(Edition t)
        {
            using (var context = new LibraryContext())
            {
                var existingEdition = context.Editions.Find(t.EditionId);

                if (existingEdition != null)
                {
                    context.Entry(existingEdition).CurrentValues.SetValues(t);
                    context.SaveChanges();
                }
                else
                {
                    throw new InvalidOperationException($"Edition with ID {t.EditionId} not found");
                }
            }
        }
    }
}
