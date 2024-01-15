// <copyright file="ExtendedBorrowService.cs" company="Transilvania University of Brasov">
// Dragomir Razvan
// </copyright>

namespace ServiceLayer.Services
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using DataMapper;
    using DomainModel;
    using log4net;
    using ServiceLayer.IServices;

    /// <summary>
    /// Service class for performing operations related to the ExtendedBorrow model.
    /// </summary>
    public class ExtendedBorrowService : IExtendedBorrowService
    {
        /// <summary>
        /// Provides logging functionality for the ExtendedBorrowService class.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(ExtendedBorrowService));

        /// <summary>
        /// Represents the data access object for managing ExtendedBorrow entities.
        /// </summary>
        private readonly IExtendedBorrowIDAO extendedBorrowIDAO;

        /// <summary>
        /// Represents the maximum limit for a specific condition.
        /// </summary>
        private readonly int lim;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedBorrowService"/> class with the specified IExtendedBorrowIDAO implementation and limit for borrow extension.
        /// </summary>
        /// <param name="extendedBorrowIDAO">The data access object interface for ExtendedBorrow.</param>
        /// <param name="lim">The limit for extending a borrow in a specific period.</param>
        public ExtendedBorrowService(IExtendedBorrowIDAO extendedBorrowIDAO, int lim)
        {
            this.extendedBorrowIDAO = extendedBorrowIDAO;
            this.lim = lim;
        }

        /// <summary>
        /// Adds a new ExtendedBorrow entity after validation.
        /// </summary>
        /// <param name="t">The ExtendedBorrow entity to be added.</param>
        public void Add(ExtendedBorrow t)
        {
            Validator.ValidateObject(t, this.CreateValidationContext(t), true);
            this.extendedBorrowIDAO.Add(t);
            Log.Info("Extended borrow has been added successfully!");
        }

        /// <summary>
        /// Gets a list of all ExtendedBorrow entities.
        /// </summary>
        /// <returns>The list of ExtendedBorrow entities.</returns>
        public List<ExtendedBorrow> GetAll()
        {
            Log.Info("List of extended borrow has been returned successfully!");
            return this.extendedBorrowIDAO.GetAll();
        }

        /// <summary>
        /// Deletes an ExtendedBorrow entity.
        /// </summary>
        /// <param name="t">The ExtendedBorrow entity to be deleted.</param>
        public void Delete(ExtendedBorrow t)
        {
            this.extendedBorrowIDAO.Delete(t);
            Log.Info("Extended borrow has been deleted successfully!");
        }

        /// <summary>
        /// Gets an ExtendedBorrow entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the ExtendedBorrow entity to be retrieved.</param>
        /// <returns>The ExtendedBorrow entity with the specified ID.</returns>
        public ExtendedBorrow GetById(int id)
        {
            Log.Info("Extended borrow has been returned successfully!");
            return this.extendedBorrowIDAO.GetById(id);
        }

        /// <summary>
        /// Updates an ExtendedBorrow entity.
        /// </summary>
        /// <param name="t">The ExtendedBorrow entity to be updated.</param>
        public void Update(ExtendedBorrow t)
        {
            Log.Info("Extended borrow has been updated successfully!");
            this.extendedBorrowIDAO.Update(t);
        }

        /// <summary>
        /// Updates an ExtendedBorrow entity.
        /// </summary>
        /// <param name="t">The ExtendedBorrow entity to be updated.</param>
        public void MaximumExtension(ExtendedBorrow t)
        {
            var extendedBorrowCount = t.Borrow.Reader.Borrows.Select(x => x.ExtendedBorrows.Where(y => t.Date.Date.AddMonths(-3) < y.Date && y.Date < t.Date.Date)).SelectMany(i => i).Count();
            if (extendedBorrowCount > this.lim)
            {
                Log.Warn($"Validation failed: Attempting to extend borrow beyond the limit for borrow with Id {t.Borrow.Id}.");
                throw new ValidationException("You can extend your borrow with maximum " + this.lim + " in this period!");
            }

            Log.Warn($"Validation failed: Attempting to extend borrow beyond the limit for borrow with Id {t.Borrow.Id}.");
        }

        /// <summary>
        /// Creates a new <see cref="ValidationContext"/> for the specified instance with optional service provider and items.
        /// </summary>
        /// <param name="instance">The object to be validated.</param>
        /// <returns>A <see cref="ValidationContext"/> for the specified instance.</returns>
        private ValidationContext CreateValidationContext(object instance)
        {
            return new ValidationContext(instance, null, null);
        }
    }
}
