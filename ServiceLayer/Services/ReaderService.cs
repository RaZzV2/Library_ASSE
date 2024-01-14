namespace ServiceLayer.Services
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using DataMapper;
    using DomainModel;
    using log4net;
    using ServiceLayer.IServices;

    /// <summary>
    /// Service class for performing operations related to the Reader model.
    /// </summary>
    public class ReaderService : IReaderService
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(ReaderService));
        private readonly IReaderIDAO iReaderIDAO;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReaderService"/> class with the specified IReaderIDAO implementation.
        /// </summary>
        /// <param name="iReaderIDAO">The data access object interface for Reader.</param>
        public ReaderService(IReaderIDAO iReaderIDAO)
        {
            this.iReaderIDAO = iReaderIDAO;
        }

        /// <summary>
        /// Adds a new Reader entity after validation.
        /// </summary>
        /// <param name="t">The Reader entity to be added.</param>
        /// <exception cref="ValidationException">Thrown if the validation fails.</exception>
        public void Add(Reader t)
        {
            try
            {
                Validator.ValidateObject(t, this.CreateValidationContext(t), true);
                this.iReaderIDAO.Add(t);
                Log.Info("Reader has been added successfully!");
            }
            catch (ValidationException validationException)
            {
                throw validationException;
            }
        }

        /// <summary>
        /// Gets a list of all Reader entities.
        /// </summary>
        /// <returns>The list of Reader entities.</returns>
        public List<Reader> GetAll()
        {
            Log.Info("List of readers has been returned successfully!");
            return this.iReaderIDAO.GetAll();
        }

        /// <summary>
        /// Deletes a Reader entity.
        /// </summary>
        /// <param name="t">The Reader entity to be deleted.</param>
        public void Delete(Reader t)
        {
            this.iReaderIDAO.Delete(t);
            Log.Info("Reader has been deleted successfully!");
        }

        /// <summary>
        /// Updates a Reader entity.
        /// </summary>
        /// <param name="t">The Reader entity to be updated.</param>
        public void Update(Reader t)
        {
            this.iReaderIDAO.Update(t);
            Log.Info("Reader has been updated successfully!");
        }

        /// <summary>
        /// Gets a Reader entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the Reader entity to be retrieved.</param>
        /// <returns>The Reader entity with the specified ID.</returns>
        public Reader GetById(int id)
        {
            Log.Info("Reader has been returned successfully!");
            return this.iReaderIDAO.GetById(id);
        }

        private ValidationContext CreateValidationContext(object instance)
        {
            return new ValidationContext(instance, null, null);
        }
    }
}
