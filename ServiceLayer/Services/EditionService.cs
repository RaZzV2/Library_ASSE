namespace ServiceLayer.Services
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using DataMapper;
    using Library.Models;
    using log4net;
    using ServiceLayer.IServices;

    /// <summary>
    /// Service class for performing operations related to the Edition model.
    /// </summary>
    public class EditionService : IEditionService
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(EditionService));
        private readonly IEditionIDAO iEditionIDAO;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditionService"/> class with the specified IEditionIDAO implementation.
        /// </summary>
        /// <param name="iEditionIDAO">The data access object interface for Edition.</param>
        public EditionService(IEditionIDAO iEditionIDAO)
        {
            this.iEditionIDAO = iEditionIDAO;
        }

        /// <summary>
        /// Adds a new Edition entity after validation.
        /// </summary>
        /// <param name="t">The Edition entity to be added.</param>
        public void Add(Edition t)
        {
            Validator.ValidateObject(t, this.CreateValidationContext(t), true);
            this.iEditionIDAO.Add(t);
            Log.Info("Edition has been added successfully!");
        }

        /// <summary>
        /// Gets a list of all Edition entities.
        /// </summary>
        /// <returns>The list of Edition entities.</returns>
        public List<Edition> GetAll()
        {
            Log.Info("List of editions has been returned succesfully!");
            return this.iEditionIDAO.GetAll();
        }

        /// <summary>
        /// Deletes an Edition entity.
        /// </summary>
        /// <param name="t">The Edition entity to be deleted.</param>
        public void Delete(Edition t)
        {
            Log.Info("Edition has been deleted succesfully!");
            this.iEditionIDAO.Delete(t);
        }

        /// <summary>
        /// Gets an Edition entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the Edition entity to be retrieved.</param>
        /// <returns>The Edition entity with the specified ID.</returns>
        public Edition GetById(int id)
        {
            Log.Info("Edition has been returned succesfully!");
            return this.iEditionIDAO.GetById(id);
        }

        /// <summary>
        /// Updates an Edition entity.
        /// </summary>
        /// <param name="t">The Edition entity to be updated.</param>
        public void Update(Edition t)
        {
            this.iEditionIDAO.Update(t);
            Log.Info("Edition has been updated succesfully!");
        }

        private ValidationContext CreateValidationContext(object instance)
        {
            return new ValidationContext(instance, null, null);
        }
    }
}
