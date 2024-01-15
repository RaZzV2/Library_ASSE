// <copyright file="EditionService.cs" company="Transilvania University of Brasov">
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
    /// Service class for performing operations related to the Edition model.
    /// </summary>
    public class EditionService : IEditionService
    {
        /// <summary>
        /// Represents a static readonly instance of the ILog interface used for logging in the EditionService class.
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(EditionService));

        /// <summary>
        /// Represents an instance of the IEditionIDAO interface, providing data access operations related to editions.
        /// </summary>
        private readonly IEditionIDAO editionIDAO;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditionService"/> class with the specified IEditionIDAO implementation.
        /// </summary>
        /// <param name="editionIDAO">The data access object interface for Edition.</param>
        public EditionService(IEditionIDAO editionIDAO)
        {
            this.editionIDAO = editionIDAO;
        }

        /// <summary>
        /// Adds a new Edition entity after validation.
        /// </summary>
        /// <param name="t">The Edition entity to be added.</param>
        public void Add(Edition t)
        {
            Validator.ValidateObject(t, this.CreateValidationContext(t), true);
            this.editionIDAO.Add(t);
            Log.Info("Edition has been added successfully!");
        }

        /// <summary>
        /// Gets a list of all Edition entities.
        /// </summary>
        /// <returns>The list of Edition entities.</returns>
        public List<Edition> GetAll()
        {
            Log.Info("List of editions has been returned succesfully!");
            return this.editionIDAO.GetAll();
        }

        /// <summary>
        /// Deletes an Edition entity.
        /// </summary>
        /// <param name="t">The Edition entity to be deleted.</param>
        public void Delete(Edition t)
        {
            Log.Info("Edition has been deleted succesfully!");
            this.editionIDAO.Delete(t);
        }

        /// <summary>
        /// Gets an Edition entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the Edition entity to be retrieved.</param>
        /// <returns>The Edition entity with the specified ID.</returns>
        public Edition GetById(int id)
        {
            Log.Info("Edition has been returned succesfully!");
            return this.editionIDAO.GetById(id);
        }

        /// <summary>
        /// Updates an Edition entity.
        /// </summary>
        /// <param name="t">The Edition entity to be updated.</param>
        public void Update(Edition t)
        {
            this.editionIDAO.Update(t);
            Log.Info("Edition has been updated succesfully!");
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
