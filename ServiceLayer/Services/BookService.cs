// <copyright file="BookService.cs" company="Transilvania University of Brasov">
// Dragomir Razvan
// </copyright>

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
        /// <summary>
        /// Represents a static instance of the log for the BookService class.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(BookService));

        /// <summary>
        /// Represents an instance of the BookService, providing operations related to books.
        /// </summary>
        private readonly IBookIDAO bookIDAO;

        /// <summary>
        /// Represents the maximum number of domains associated with a book.
        /// </summary>
        private readonly int maxDomains;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookService"/> class with the specified IBookIDAO implementation and maximum domains.
        /// </summary>
        /// <param name="bookIDAO">The data access object interface for Book.</param>
        /// <param name="maxDomains">The maximum number of domains allowed for a book.</param>
        public BookService(IBookIDAO bookIDAO, int maxDomains)
        {
            this.bookIDAO = bookIDAO;
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
                Validator.ValidateObject(t, this.CreateValidationContext(t), true);
                this.bookIDAO.Add(t);
                Log.Info("Book has been added successfully!");
        }

        /// <summary>
        /// Retrieves a list of all Books from the data store.
        /// </summary>
        /// <returns>List of Books.</returns>
        public List<Book> GetAll()
        {
            Log.Info("List of books has been returned succesfully!");
            return this.bookIDAO.GetAll();
        }

        /// <summary>
        /// Deletes an existing Book from the data store.
        /// </summary>
        /// <param name="t">The Book object to be deleted.</param>
        public void Delete(Book t)
        {
            Log.Info("Book has been deleted succesfully!");
            this.bookIDAO.Delete(t);
        }

        /// <summary>
        /// Retrieves a Book by its unique identifier from the data store.
        /// </summary>
        /// <param name="id">The unique identifier of the Book.</param>
        /// <returns>The retrieved Book object.</returns>
        public Book GetById(int id)
        {
            Log.Info("Book has been returned succesfully!");
            return this.bookIDAO.GetById(id);
        }

        /// <summary>
        /// Updates an existing Book in the data store.
        /// </summary>
        /// <param name="t">The Book object to be updated.</param>
        public void Update(Book t)
        {
            this.bookIDAO.Update(t);
            Log.Info("Book has been updated succesfuly!");
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
