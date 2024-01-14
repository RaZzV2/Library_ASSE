using DataMapper;
using Library.models;
using log4net;
using ServiceLayer.IServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class BookDomainService : IBookDomainService
    {
        readonly IBookDomainIDAO iBookDomainIDAO;
        private static readonly ILog Log = LogManager.GetLogger(typeof(BookDomainService));

        private ValidationContext CreateValidationContext(object instance)
        {
            return new ValidationContext(instance, null, null);
        }

        public BookDomainService(IBookDomainIDAO iBookDomainIDAO)
        {
            this.iBookDomainIDAO = iBookDomainIDAO;
        }

        public void Add(BookDomain t)
        {
            Validator.ValidateObject(t, CreateValidationContext(t), true);
            iBookDomainIDAO.Add(t);
            Log.Info("Book domain has been added successfully!");
        }

        public void Delete(BookDomain t)
        {
            iBookDomainIDAO.Delete(t);
            Log.Info("Book domain has been deleted successfully!");
        }

        public BookDomain GetById(int id)
        {
            Log.Info("Book domain has been returned successfully!");
            return iBookDomainIDAO.GetById(id);
        }

        public List<BookDomain> GetAll()
        {
            Log.Info("List of book domains has been returned successfully!");
            return iBookDomainIDAO.GetAll();
        }



        public void Update(BookDomain t)
        {
            Validator.ValidateObject(t, CreateValidationContext(t), true);
            iBookDomainIDAO.Update(t);
            Log.Info("Book domain has been updated successfully!");
        }
    }
}
