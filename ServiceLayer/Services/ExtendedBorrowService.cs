namespace ServiceLayer.Services
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using DataMapper;
    using DomainModel;
    using log4net;
    using ServiceLayer.IServices;

    public class ExtendedBorrowService : IExtendedBorrowService
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ExtendedBorrowService));
        private readonly IExtendedBorrowIDAO iExtendedBorrowIDAO;

        private readonly int lim;

        private ValidationContext CreateValidationContext(object instance)
        {
            return new ValidationContext(instance, null, null);
        }

        public ExtendedBorrowService(IExtendedBorrowIDAO iExtendedBorrowIDAO, int lim)
        {
            this.iExtendedBorrowIDAO = iExtendedBorrowIDAO;
            this.lim = lim;
        }

        public void Add(ExtendedBorrow t)
        {
            Validator.ValidateObject(t, this.CreateValidationContext(t), true);
            this.iExtendedBorrowIDAO.Add(t);
            Log.Info("Extended borrow has been added successfully!");
        }

        public List<ExtendedBorrow> GetAll()
        {
            Log.Info("List of extended borrow has been returned successfully!");
            return this.iExtendedBorrowIDAO.GetAll();
        }

        public void Delete(ExtendedBorrow t)
        {
            this.iExtendedBorrowIDAO.Delete(t);
            Log.Info("Extended borrow has been deleted successfully!");
        }

        public ExtendedBorrow GetById(int id)
        {
            Log.Info("Extended borrow has been returned successfully!");
            return this.iExtendedBorrowIDAO.GetById(id);
        }

        public void Update(ExtendedBorrow t)
        {
            Log.Info("Extended borrow has been updated successfully!");
            this.iExtendedBorrowIDAO.Update(t);
        }

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
    }
}
