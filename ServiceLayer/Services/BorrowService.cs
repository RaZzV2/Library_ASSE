namespace ServiceLayer.Services
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using DataMapper;
    using DomainModel;
    using log4net;

    /// <summary>
    /// Service class for performing operations related to the Borrow model.
    /// </summary>
    public class BorrowService : IBorrowIDAO
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(BorrowService));
        private readonly IBorrowIDAO iBorrowIDAO;
        private readonly int nmc;
        private readonly int c;
        private readonly int ncz;
        private readonly int d;
        private TimeSpan per;
        private TimeSpan l;
        private TimeSpan delta;

        /// <summary>
        /// Initializes a new instance of the <see cref="BorrowService"/> class with the specified IBorrowIDAO implementation and parameters.
        /// </summary>
        /// <param name="iBorrowIDAO">The data access object interface for Borrow.</param>
        /// <param name="per">The time span representing the specific period for borrowing.</param>
        /// <param name="nmc">The maximum number of books that can be borrowed in a specific period for regular readers.</param>
        /// <param name="c">The limit of books that can be borrowed at once for regular readers.</param>
        /// <param name="d">The maximum number of books with the same domain that can be borrowed in a specific period for regular readers.</param>
        /// <param name="l">The time span representing the duration for checking the maximum books in the same domain.</param>
        /// <param name="ncz">The maximum number of books that can be borrowed in a day for regular readers.</param>
        /// <param name="delta">The time span representing the delta for checking the same book borrow in a period.</param>
        public BorrowService(IBorrowIDAO iBorrowIDAO, TimeSpan per, int nmc, int c, int d, TimeSpan l, int ncz, TimeSpan delta)
        {
            this.iBorrowIDAO = iBorrowIDAO;
            this.per = per;
            this.nmc = nmc;
            this.c = c;
            this.d = d;
            this.l = l;
            this.ncz = ncz;
            this.delta = delta;
        }

        /// <summary>
        /// Validates and throws a ValidationException if the same book is borrowed in a specific period.
        /// </summary>
        /// <param name="entity">The Borrow entity to be validated.</param>
        public void CannotBorrowSameBookInAPeriod(Borrow entity)
        {
            if (entity.Reader.Role == false)
            {
                var startDate = entity.BorrowStartDate - this.delta;
                var borrowCount = entity.Reader.Borrows.Where(x => startDate < x.BorrowStartDate && x.BorrowStartDate < entity.BorrowStartDate).Where(x => x.Edition.EditionId == entity.Edition.EditionId).Count();
                if (borrowCount > 0)
                {
                    Log.Error($"An error occurred in CannotBorrowSameBookInAPeriod for book with Id {entity.Edition.Book.BookId}");
                    throw new ValidationException("You cannot borrow same edition in this period!");
                }
            }
            else
            {
                var deltaPersonal = new TimeSpan(this.delta.Ticks / 2);
                var startDate = entity.BorrowStartDate - deltaPersonal;
                var borrowCount = entity.Reader.Borrows.Where(x => startDate < x.BorrowStartDate && x.BorrowStartDate < entity.BorrowStartDate).Where(x => x.Edition.EditionId == entity.Edition.EditionId).Count();
                if (borrowCount > 0)
                {
                    Log.Error($"An error occurred in CannotBorrowSameBookInAPeriod for book with Id {entity.Edition.Book.BookId}");
                    throw new ValidationException("You cannot borrow same edition in this period!");
                }
            }

            Log.Debug($"CannotBorrowSameBookInAPeriod completed successfully for book with Id {entity.Edition.Book.BookId}.");
        }

        /// <summary>
        /// Validates and throws a ValidationException if the maximum books in the same domain are exceeded.
        /// </summary>
        /// <param name="entity">The Borrow entity to be validated.</param>
        public void MaximumBooksInTheSameDomain(Borrow entity)
        {
            Dictionary<int, int> numbersOfParentDomains = new Dictionary<int, int>();
            var startDate = entity.BorrowStartDate - this.l;
            var borrowsOfLastMonths = entity.Reader.Borrows.Where(x => x.BorrowStartDate >= startDate && x.BorrowStartDate <= entity.BorrowStartDate);
            foreach (var currentBorrow in borrowsOfLastMonths)
            {
                foreach (var currentBorrowDomain in currentBorrow.Edition.Book.Domains)
                {
                    var aux = currentBorrowDomain;
                    while (aux.ParentDomain != null)
                    {
                        aux = aux.ParentDomain;
                    }

                    if (numbersOfParentDomains.ContainsKey(aux.BookDomainId))
                    {
                        numbersOfParentDomains[aux.BookDomainId]++;
                    }
                    else
                    {
                        numbersOfParentDomains[aux.BookDomainId] = 1;
                    }
                }
            }

            var maximum = numbersOfParentDomains.Max(x => x.Value);
            if (entity.Reader.Role == false)
            {
                if (maximum > this.d)
                {
                    Log.Warn($"Validation failed: You borrowed a book with the same domain too much for book with Id {entity.Edition.Book.BookId}.");
                    throw new ValidationException("You borrowed a book with same domain too much!");
                }
            }
            else
            {
                if (maximum > this.d * 2)
                {
                    Log.Warn($"Validation failed: You borrowed a book with the same domain too much for book with Id {entity.Edition.Book.BookId}.");
                    throw new ValidationException("You borrowed a book with same domain too much!");
                }
            }

            Log.Debug($"MaximumBooksInTheSameDomain completed successfully for book with Id {entity.Edition.Book.BookId}.");
        }

        /// <summary>
        /// Validates a list of Borrow entities and throws a ValidationException if validation fails.
        /// </summary>
        /// <param name="borrowList">The list of Borrow entities to be validated.</param>
        public void ValidateBorrowList(List<Borrow> borrowList)
        {
            if (borrowList.First().Reader.Role == false)
            {
                if (borrowList.Count > this.c)
                {
                    Log.Warn($"Validation failed: Limit exceeded for borrower with Id {borrowList.First().Reader.ReaderId}.");
                    throw new ValidationException("Your limit to borrow books has exceeded!");
                }
            }
            else
            {
                if (borrowList.Count > this.c * 2)
                {
                    Log.Warn($"Validation failed: Limit exceeded for borrower with Id {borrowList.First().Reader.ReaderId}.");
                    throw new ValidationException("Your limit to borrow books has exceeded!");
                }
            }

            if (borrowList.Count > 2)
            {

                HashSet<int> bookDomains = new HashSet<int>();
                foreach (var entity in borrowList)
                {
                    foreach (var domains in entity.Edition.Book.Domains)
                    {
                        bookDomains.Add(domains.BookDomainId);
                    }
                }

                if (bookDomains.Count < 2)
                {
                    throw new ValidationException("You should borrow at least two different books in two different domains!");
                }
            }

            Log.Debug($"ValidateBorrowList completed successfully for borrower with Id {borrowList.First().Reader.ReaderId}.");
        }

        /// <summary>
        /// Creates a new list of Borrow entities after validation and adds them to the data store.
        /// </summary>
        /// <param name="borrowList">The list of Borrow entities to be added.</param>
        public void CreateBorrowList(List<Borrow> borrowList)
        {
            try
            {
                this.ValidateBorrowList(borrowList);
                foreach (var entity in borrowList)
                {
                    this.Validate(entity);
                    this.iBorrowIDAO.Add(entity);
                }
            }
            catch (Exception ex)
            {
                foreach (var entity in borrowList)
                {
                    this.iBorrowIDAO.Delete(entity);
                }

                throw ex;
            }
        }

        /// <summary>
        /// Validates and throws a ValidationException if the borrowable book conditions are not met.
        /// </summary>
        /// <param name="t">The Borrow entity to be validated.</param>
        public void CheckBorrowBook(Borrow t)
        {
            if (t.Edition.BorrowableBooks == 0)
            {
                throw new ValidationException("There are no borrowable books!");
            }

            if ((t.Edition.BorrowableBooks + t.Edition.UnBorrowableBooks) * 0.1 > t.Edition.BorrowableBooks)
            {
                throw new ValidationException("Borrowable books should be at least 10% of total books!");
            }
        }

        /// <summary>
        /// Validates and throws a <see cref="ValidationException"/> if the maximum number of borrows per day is exceeded.
        /// </summary>
        /// <param name="t">The <see cref="Borrow"/> entity to be validated.</param>
        public void MaximumBorrowsPerDay(Borrow t)
        {
            var borrowCountInADay = t.Reader.Borrows.Where(x => t.BorrowStartDate.Date == x.BorrowStartDate).Count();
            if (borrowCountInADay > this.ncz && t.Reader.Role == false)
            {
                throw new ValidationException("You cannot borrow more than " + this.ncz + " books per day!");
            }
        }

        /// <summary>
        /// Validates and throws a <see cref="ValidationException"/> if the maximum books in a specific period are exceeded.
        /// </summary>
        /// <param name="t">The <see cref="Borrow"/> entity to be validated.</param>
        public void MaximumBooksInSpecificPeriod(Borrow t)
        {
            var startDate = t.BorrowStartDate - this.per;
            var numberOfBorrows = t.Reader.Borrows.Where(x => x.BorrowStartDate >= startDate && x.BorrowStartDate <= t.BorrowStartDate).Count();
            if (t.Reader.Role == false)
            {
                if (numberOfBorrows > this.nmc)
                {
                    Log.Warn($"Validation failed: Borrowing more than {this.nmc} books in this period for borrower with Id {t.Reader.ReaderId}.");
                    throw new ValidationException("You cannot borrow more than " + this.nmc + " books in this period!");
                }
            }
            else
            {
                if (numberOfBorrows > this.nmc * 2)
                {
                    Log.Warn($"Validation failed: Borrowing more than {this.nmc} books in this period for borrower with Id {t.Reader.ReaderId}.");
                    throw new ValidationException("You cannot borrow more than " + (this.nmc * 2) + " books in this period!");
                }
            }

            Log.Debug($"MaximumBooksInSpecificPeriod completed successfully for borrower with Id {t.Reader.ReaderId}.");
        }

        /// <summary>
        /// Adds a new Borrow entity after validation.
        /// </summary>
        /// <param name="t">The Borrow entity to be added.</param>
        public void Add(Borrow t)
        {
            Validator.ValidateObject(t, this.CreateValidationContext(t), true);
            this.iBorrowIDAO.Add(t);
            Log.Info("Borrow has been added successfully!");
        }

        /// <summary>
        /// Gets a list of all Borrow entities.
        /// </summary>
        /// <returns>The list of Borrow entities.</returns>
        public List<Borrow> GetAll()
        {
            Log.Info("List of borrows has been returned succesfully!");
            return this.iBorrowIDAO.GetAll();
        }

        /// <summary>
        /// Deletes a Borrow entity.
        /// </summary>
        /// <param name="t">The Borrow entity to be deleted.</param>
        public void Delete(Borrow t)
        {
            Log.Info("Borrow has been deleted successfully!");
            this.iBorrowIDAO.Delete(t);
        }

        /// <summary>
        /// Gets a Borrow entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the Borrow entity to be retrieved.</param>
        /// <returns>The Borrow entity with the specified ID.</returns>
        public Borrow GetById(int id)
        {
            Log.Info("Borrow has been returned successfully!");
            return this.iBorrowIDAO.GetById(id);
        }

        /// <summary>
        /// Updates a Borrow entity.
        /// </summary>
        /// <param name="t">The Borrow entity to be updated.</param>
        public void Update(Borrow t)
        {
            Log.Info("Borrow has been updated successfully!");
            this.iBorrowIDAO.Update(t);
        }

        private ValidationContext CreateValidationContext(object instance)
        {
            return new ValidationContext(instance, null, null);
        }

        private void Validate(Borrow entity)
        {
            Validator.ValidateObject(entity, this.CreateValidationContext(entity), true);
            this.CheckBorrowBook(entity);
        }
    }
}
