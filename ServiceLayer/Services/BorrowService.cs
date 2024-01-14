using DataMapper;
using DataMapper.Migrations;
using DomainModel;
using Library.models;
using log4net;
using Rhino.Mocks.Impl;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class BorrowService : IBorrowIDAO
    {
        readonly IBorrowIDAO iBorrowIDAO;
        private static readonly ILog Log = LogManager.GetLogger(typeof(BorrowService));
        private TimeSpan per;
        private readonly int nmc;
        private readonly int c;
        private readonly int d;
        private TimeSpan l;
        private TimeSpan delta;
        private readonly int ncz;

        private ValidationContext CreateValidationContext(object instance)
        {
            return new ValidationContext(instance, null, null);
        }

        public BorrowService(IBorrowIDAO iBorrowIDAO, TimeSpan per, int nmc, int c, int d, TimeSpan l, int ncz, TimeSpan delta)
        {
            this.iBorrowIDAO = iBorrowIDAO;
            this.per = per;
            this.nmc = nmc;
            this.c= c;
            this.d=d;
            this.l=l;
            this.ncz = ncz;
            this.delta = delta;
        }

        public void CannotBorrowSameBookInAPeriod(Borrow entity)
        {
            var startDate = entity.BorrowStartDate - delta;
            var borrowCount = entity.Reader.Borrows.Where(x => startDate < x.BorrowStartDate && x.BorrowStartDate < entity.BorrowStartDate).Where(x => x.Edition.EditionId == entity.Edition.EditionId).Count();
            if(borrowCount > 0)
            {
                throw new ValidationException("You cannot borrow same edition in this period!");
            }
        }

        public void MaximumBooksInTheSameDomain(Borrow entity)
        {
            Dictionary<int, int> numbersOfParentDomains = new Dictionary<int, int>();
            var startDate = entity.BorrowStartDate - l;
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
                if (maximum > d)
                {
                    throw new ValidationException("You borrowed a book with same domain too much!");
                }
            }
            else
            {
                if (maximum > d*2)
                {
                    throw new ValidationException("You borrowed a book with same domain too much!");
                }
            }
        }

        private void Validate(Borrow entity)
        {
            Validator.ValidateObject(entity, CreateValidationContext(entity), true);
            CheckBorrowBook(entity);
        }

        public void ValidateBorrowList(List<Borrow> borrowList)
        {
            if (borrowList.First().Reader.Role == false)
            {
                if (borrowList.Count > c)
                {
                    throw new ValidationException("Your limit to borrow books has exceeded!");
                }
            }
            else
            {
                if (borrowList.Count > c*2)
                {
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
        }

        public void CreateBorrowList(List<Borrow> borrowList)
        {
            try
            {
                ValidateBorrowList(borrowList);
                foreach (var entity in borrowList)
                {
                    Validate(entity);
                    iBorrowIDAO.Add(entity);
                }
            }
            catch (Exception ex)
            {
                foreach (var entity in borrowList)
                {
                    iBorrowIDAO.Delete(entity);
                }
                throw ex;
            }
        }
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

        public void MaximumBorrowsPerDay(Borrow t)
        {
            var borrowCountInADay = t.Reader.Borrows.Where(x=>t.BorrowStartDate.Date == x.BorrowStartDate).Count();
            if(borrowCountInADay > ncz && t.Reader.Role == false)
            {
                throw new ValidationException("You cannot borrow more than " + ncz + " books per day!");
            }
        }

        public void MaximumBooksInSpecificPeriod(Borrow t)
        {
            var startDate = t.BorrowStartDate - per;
            var numberOfBorrows = t.Reader.Borrows.Where(x => x.BorrowStartDate >= startDate && x.BorrowStartDate <= t.BorrowStartDate).Count();
            if (t.Reader.Role == false)
            {
                if (numberOfBorrows > nmc)
                {
                    throw new ValidationException("You cannot borrow more than " + nmc + " books in this period!");
                }
            }
            else
            {
                if (numberOfBorrows > nmc*2)
                {
                    throw new ValidationException("You cannot borrow more than " + nmc*2 + " books in this period!");
                }
            }
        }


        public void Add(Borrow t)
        {
            Validator.ValidateObject(t, CreateValidationContext(t), true);
            iBorrowIDAO.Add(t);
            Log.Info("Borrow has been added successfully!");
        }

        public List<Borrow> GetAll()
        {
            return iBorrowIDAO.GetAll();
        }
        public void Delete(Borrow t)
        {
            iBorrowIDAO.Delete(t);
        }

        public Borrow GetById(int id)
        {
            return iBorrowIDAO.GetById(id);
        }

        public void Update(Borrow t)
        {
            iBorrowIDAO.Update(t);
        }
    }
}
