// <copyright file="BookDomainService.cs" company="Transilvania University of Brasov">
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
    /// Service class for performing operations related to the BookDomain model.
    /// </summary>
    public class BookDomainService : IBookDomainService
    {
        /// <summary>
        /// Represents the static readonly log instance for logging in the BookDomainService class.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(BookDomainService));

        /// <summary>
        /// Represents the Data Access Object (DAO) for handling BookDomain entities.
        /// </summary>
        private readonly IBookDomainIDAO bookDomainIDAO;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookDomainService"/> class with the specified IBookDomainIDAO implementation.
        /// </summary>
        /// <param name="bookDomainIDAO">The data access object interface for BookDomain.</param>
        public BookDomainService(IBookDomainIDAO bookDomainIDAO)
        {
            this.bookDomainIDAO = bookDomainIDAO;
        }

        /// <summary>
        /// Adds a new BookDomain to the data store.
        /// </summary>
        /// <param name="t">The BookDomain object to be added.</param>
        public void Add(BookDomain t)
        {
            Validator.ValidateObject(t, this.CreateValidationContext(t), true);
            this.bookDomainIDAO.Add(t);
            Log.Info("Book domain has been added successfully!");
        }

        /// <summary>
        /// Deletes an existing BookDomain from the data store.
        /// </summary>
        /// <param name="t">The BookDomain object to be deleted.</param>
        public void Delete(BookDomain t)
        {
            this.bookDomainIDAO.Delete(t);
            Log.Info("Book domain has been deleted successfully!");
        }

        /// <summary>
        /// Retrieves a BookDomain by its unique identifier from the data store.
        /// </summary>
        /// <param name="id">The unique identifier of the BookDomain.</param>
        /// <returns>The retrieved BookDomain object.</returns>
        public BookDomain GetById(int id)
        {
            Log.Info("Book domain has been returned successfully!");
            return this.bookDomainIDAO.GetById(id);
        }

        /// <summary>
        /// Retrieves a list of all BookDomains from the data store.
        /// </summary>
        /// <returns>List of BookDomains.</returns>
        public List<BookDomain> GetAll()
        {
            Log.Info("List of book domains has been returned successfully!");
            return this.bookDomainIDAO.GetAll();
        }

        /// <summary>
        /// Updates an existing BookDomain in the data store.
        /// </summary>
        /// <param name="t">The BookDomain object to be updated.</param>
        public void Update(BookDomain t)
        {
            Validator.ValidateObject(t, this.CreateValidationContext(t), true);
            this.bookDomainIDAO.Update(t);
            Log.Info("Book domain has been updated successfully!");
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
