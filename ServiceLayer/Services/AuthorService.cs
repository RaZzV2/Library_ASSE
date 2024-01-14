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
        private static readonly ILog Log = LogManager.GetLogger(typeof(AuthorService));
        private readonly IAuthorIDAO iAuthorIDAO;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorService"/> class with the specified IAuthorIDAO implementation.
        /// </summary>
        /// <param name="iAuthorIDAO">The data access object interface for Author.</param>
        public AuthorService(IAuthorIDAO iAuthorIDAO)
        {
            this.iAuthorIDAO = iAuthorIDAO;
        }

        /// <summary>
        /// Adds a new Author to the data store.
        /// </summary>
        /// <param name="t">The Author object to be added.</param>
        public void Add(Author t)
        {
            Validator.ValidateObject(t, this.CreateValidationContext(t), true);
            this.iAuthorIDAO.Add(t);
            Log.Info("Author has been added successfully!");
        }

        /// <summary>
        /// Retrieves a list of all authors from the data store.
        /// </summary>
        /// <returns>List of authors.</returns>
        public List<Author> GetAll()
        {
            Log.Info("List of authors has been returned successfully!");
            return this.iAuthorIDAO.GetAll();
        }

        /// <summary>
        /// Deletes an existing author from the data store.
        /// </summary>
        /// <param name="t">The Author object to be deleted.</param>
        public void Delete(Author t)
        {
            this.iAuthorIDAO.Delete(t);
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
            return this.iAuthorIDAO.GetById(id);
        }

        /// <summary>
        /// Updates an existing Author in the data store.
        /// </summary>
        /// <param name="t">The Author object to be updated.</param>
        public void Update(Author t)
        {
            Validator.ValidateObject(t, this.CreateValidationContext(t), true);
            this.iAuthorIDAO.Update(t);
            Log.Info("Author has been updated!");
        }

        /// <summary>
        /// Creates a validation context for the specified instance.
        /// </summary>
        private ValidationContext CreateValidationContext(object instance)
        {
            return new ValidationContext(instance, null, null);
        }
    }
}
