using DataMapper;
using DomainModel;
using Library.models;
using log4net;
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

        private ValidationContext CreateValidationContext(object instance)
        {
            return new ValidationContext(instance, null, null);
        }

        public BorrowService(IBorrowIDAO iBorrowIDAO)
        {
            this.iBorrowIDAO = iBorrowIDAO;
        }

        public void Add(Borrow t)
        {
            Validator.ValidateObject(t, CreateValidationContext(t), true);
            iBorrowIDAO.Add(t);
            Log.Info("Borrow has been added successfully!");
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
            try
            {
                Validator.ValidateObject(t, CreateValidationContext(t), true);
                iBorrowIDAO.Update(t);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
