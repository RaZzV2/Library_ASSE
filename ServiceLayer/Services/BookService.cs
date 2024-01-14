namespace ServiceLayer.Services
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using DataMapper;
    using Library.Models;
    using log4net;
    using ServiceLayer.IServices;

    /// <summary>
    /// Service class for performing operations related to the Book model.
    /// </summary>
    public class BookService : IBookService
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(BookService));
        private readonly IBookIDAO iBookIDAO;
        private readonly int maxDomains;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookService"/> class with the specified IBookIDAO implementation and maximum domains.
        /// </summary>
        /// <param name="iBookIDAO">The data access object interface for Book.</param>
        /// <param name="maxDomains">The maximum number of domains allowed for a book.</param>
        public BookService(IBookIDAO iBookIDAO, int maxDomains)
        {
            this.iBookIDAO = iBookIDAO;
            this.maxDomains = maxDomains;
        }

        /// <summary>
        /// Verifies that a book doesn't have more domains than necessary.
        /// </summary>
        /// <param name="book">The Book object to be verified.</param>
        public void VerifyMoreDomainsThanNecessary(Book book)
        {
            if (book.Domains.Count >= this.maxDomains)
            {
                throw new ValidationException("A book shouldn't have more than " + this.maxDomains + " domains");
            }

            Log.Debug("The function of verifying domains is called succesfully!");
        }

        /// <summary>
        /// Adds a new Book to the data store.
        /// </summary>
        /// <param name="t">The Book object to be added.</param>
        public void Add(Book t)
        {
                Validator.ValidateObject(t, this.CreateValidationContext(t),true);
                this.iBookIDAO.Add(t);
                Log.Info("Book has been added successfully!");
        }

        /// <summary>
        /// Retrieves a list of all Books from the data store.
        /// </summary>
        /// <returns>List of Books.</returns>
        public List<Book> GetAll()
        {
            Log.Info("List of books has been returned succesfully!");
            return this.iBookIDAO.GetAll();
        }

        /// <summary>
        /// Deletes an existing Book from the data store.
        /// </summary>
        /// <param name="t">The Book object to be deleted.</param>
        public void Delete(Book t)
        {
            Log.Info("Book has been deleted succesfully!");
            this.iBookIDAO.Delete(t);
        }

        /// <summary>
        /// Retrieves a Book by its unique identifier from the data store.
        /// </summary>
        /// <param name="id">The unique identifier of the Book.</param>
        /// <returns>The retrieved Book object.</returns>
        public Book GetById(int id)
        {
            Log.Info("Book has been returned succesfully!");
            return this.iBookIDAO.GetById(id);
        }

        /// <summary>
        /// Updates an existing Book in the data store.
        /// </summary>
        /// <param name="t">The Book object to be updated.</param>
        public void Update(Book t)
        {
            this.iBookIDAO.Update(t);
            Log.Info("Book has been updated succesfuly!");
        }

        private ValidationContext CreateValidationContext(object instance)
        {
            return new ValidationContext(instance, null, null);
        }
    }
}
