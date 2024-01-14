namespace DataMapper
{
    using System.Collections.Generic;

    /// <summary>
    /// The <see cref="IDAO{T}"/> interface defines the generic contract for Data Access Object (DAO) operations
    /// for any entity type in the data storage layer.
    /// </summary>
    /// <typeparam name="T">The type of entity for which DAO operations are defined.</typeparam>
    public interface IDAO<T>
    {
        /// <summary>
        /// Retrieves an entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <returns>The entity if found, otherwise null.</returns>
        T GetById(int id);

        /// <summary>
        /// Adds a new entity to the data storage layer.
        /// </summary>
        /// <param name="t">The entity to be added.</param>
        void Add(T t);

        /// <summary>
        /// Updates an existing entity in the data storage layer.
        /// </summary>
        /// <param name="t">The updated entity information.</param>
        void Update(T t);

        /// <summary>
        /// Deletes an entity from the data storage layer.
        /// </summary>
        /// <param name="t">The entity to be deleted.</param>
        void Delete(T t);

        /// <summary>
        /// Retrieves all entities of the specified type from the data storage layer.
        /// </summary>
        /// <returns>A list of entities.</returns>
        List<T> GetAll();
    }
}
