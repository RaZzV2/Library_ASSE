// <copyright file="AuthorService.cs" company="Transilvania University of Brasov">
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
    /// Service class for performing operations related to the Author model.
    /// </summary>
    public class AuthorService : IAuthorService
    {
        /// <summary>
        /// Represents the logger for the AuthorService class.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(AuthorService));

        /// <summary>
        /// Represents the data access object for handling Author entities.
        /// </summary>
        private readonly IAuthorIDAO authorIDAO;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorService"/> class with the specified IAuthorIDAO implementation.
        /// </summary>
        /// <param name="authorIDAO">The data access object interface for Author.</param>
        public AuthorService(IAuthorIDAO authorIDAO)
        {
            this.authorIDAO = authorIDAO;
        }

        /// <summary>
        /// Adds a new Author to the data store.
        /// </summary>
        /// <param name="t">The Author object to be added.</param>
        public void Add(Author t)
        {
            Validator.ValidateObject(t, this.CreateValidationContext(t), true);
            this.authorIDAO.Add(t);
            Log.Info("Author has been added successfully!");
        }

        /// <summary>
        /// Retrieves a list of all authors from the data store.
        /// </summary>
        /// <returns>List of authors.</returns>
        public List<Author> GetAll()
        {
            Log.Info("List of authors has been returned successfully!");
            return this.authorIDAO.GetAll();
        }

        /// <summary>
        /// Deletes an existing author from the data store.
        /// </summary>
        /// <param name="t">The Author object to be deleted.</param>
        public void Delete(Author t)
        {
            this.authorIDAO.Delete(t);
            Log.Info("An author has been deleted!");
        }

        /// <summary>
        /// Retrieves an author by its unique identifier from the data store.
        /// </summary>
        /// <param name="id">The unique identifier of the author.</param>
        /// <returns>The retrieved Author object.</returns>
        public Author GetById(int id)
        {
            Log.Info("Author has been returned!");
            return this.authorIDAO.GetById(id);
        }

        /// <summary>
        /// Updates an existing Author in the data store.
        /// </summary>
        /// <param name="t">The Author object to be updated.</param>
        public void Update(Author t)
        {
            Validator.ValidateObject(t, this.CreateValidationContext(t), true);
            this.authorIDAO.Update(t);
            Log.Info("Author has been updated!");
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
