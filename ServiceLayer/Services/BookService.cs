using DataMapper;
using Library.models;
using log4net;
using ServiceLayer.IServices;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.Services
{
    public class BookService : IBookService
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(BookService));
        private readonly IBookIDAO iBookIDAO;
        private readonly int maxDomains;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookService"/> class.
        /// </summary>
        /// <param name="iBookIDAO"></param>
        /// <param name="maxDomains"></param>
        public BookService(IBookIDAO iBookIDAO, int maxDomains)
        {
            this.iBookIDAO = iBookIDAO;
            this.maxDomains = maxDomains;
        }
        private ValidationContext CreateValidationContext(object instance)
        {
            return new ValidationContext(instance, null, null);
        }

        public void VerifyMoreDomainsThanNecessary(Book book)
        {
            if(book.Domains.Count >= maxDomains)
            {
                throw new ValidationException("A book shouldn't have more than " + maxDomains + " domains");
            }
            Log.Debug("The function of verifying domains is called succesfully!");
        }

        public void Add(Book t)
        {
                Validator.ValidateObject(t, CreateValidationContext(t),true);
                iBookIDAO.Add(t);
                Log.Info("Book has been added successfully!");
        }

        public List<Book> GetAll()
        {
            Log.Info("List of books has been returned succesfully!");
            return iBookIDAO.GetAll();
        }

        public void Delete(Book t)
        {
            Log.Info("Book has been deleted succesfully!");
            iBookIDAO.Delete(t);
        }

        public Book GetById(int id)
        {
            Log.Info("Book has been returned succesfully!");
            return iBookIDAO.GetById(id);
        }

        public void Update(Book t)
        {
            iBookIDAO.Update(t);
            Log.Info("Book has been updated succesfuly!");
        }
    }
}
