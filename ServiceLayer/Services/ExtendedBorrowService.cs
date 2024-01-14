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
        private static readonly ILog Log = LogManager.GetLogger(typeof(ExtendedBorrowService));
        private readonly IExtendedBorrowIDAO iExtendedBorrowIDAO;

        private readonly int lim;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedBorrowService"/> class with the specified IExtendedBorrowIDAO implementation and limit for borrow extension.
        /// </summary>
        /// <param name="iExtendedBorrowIDAO">The data access object interface for ExtendedBorrow.</param>
        /// <param name="lim">The limit for extending a borrow in a specific period.</param>
        public ExtendedBorrowService(IExtendedBorrowIDAO iExtendedBorrowIDAO, int lim)
        {
            this.iExtendedBorrowIDAO = iExtendedBorrowIDAO;
            this.lim = lim;
        }

        /// <summary>
        /// Adds a new ExtendedBorrow entity after validation.
        /// </summary>
        /// <param name="t">The ExtendedBorrow entity to be added.</param>
        public void Add(ExtendedBorrow t)
        {
            Validator.ValidateObject(t, this.CreateValidationContext(t), true);
            this.iExtendedBorrowIDAO.Add(t);
            Log.Info("Extended borrow has been added successfully!");
        }

        /// <summary>
        /// Gets a list of all ExtendedBorrow entities.
        /// </summary>
        /// <returns>The list of ExtendedBorrow entities.</returns>
        public List<ExtendedBorrow> GetAll()
        {
            Log.Info("List of extended borrow has been returned successfully!");
            return this.iExtendedBorrowIDAO.GetAll();
        }

        /// <summary>
        /// Deletes an ExtendedBorrow entity.
        /// </summary>
        /// <param name="t">The ExtendedBorrow entity to be deleted.</param>
        public void Delete(ExtendedBorrow t)
        {
            this.iExtendedBorrowIDAO.Delete(t);
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
            return this.iExtendedBorrowIDAO.GetById(id);
        }

        /// <summary>
        /// Updates an ExtendedBorrow entity.
        /// </summary>
        /// <param name="t">The ExtendedBorrow entity to be updated.</param>
        public void Update(ExtendedBorrow t)
        {
            Log.Info("Extended borrow has been updated successfully!");
            this.iExtendedBorrowIDAO.Update(t);
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

        private ValidationContext CreateValidationContext(object instance)
        {
            return new ValidationContext(instance, null, null);
        }
    }
}
