// <copyright file="IDAOService.cs" company="Transilvania University of Brasov">
// Dragomir Razvan
// </copyright>

namespace ServiceLayer.IServices
{
    using System.Collections.Generic;

    /// <summary>
    /// Generic Data Access Object (DAO) service interface providing common CRUD operations.
    /// </summary>
    /// <typeparam name="T">The type of entity the service operates on.</typeparam>
    public interface IDAOService<T>
    {
        /// <summary>
        /// Retrieves an entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <returns>The entity with the specified identifier.</returns>
        T GetById(int id);

        /// <summary>
        /// Adds a new entity to the data store.
        /// </summary>
        /// <param name="t">The entity to be added.</param>
        void Add(T t);

        /// <summary>
        /// Updates an existing entity in the data store.
        /// </summary>
        /// <param name="t">The entity to be updated.</param>
        void Update(T t);

        /// <summary>
        /// Deletes an entity from the data store.
        /// </summary>
        /// <param name="t">The entity to be deleted.</param>
        void Delete(T t);

        /// <summary>
        /// Retrieves a list of all entities of type T from the data store.
        /// </summary>
        /// <returns>A list of all entities of type T.</returns>
        List<T> GetAll();
    }
}
