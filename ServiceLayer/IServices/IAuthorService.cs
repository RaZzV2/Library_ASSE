// <copyright file="IAuthorService.cs" company="Transilvania University of Brasov">
// Dragomir Razvan
// </copyright>

namespace ServiceLayer.IServices
{
    using Library.Models;

    /// <summary>
    /// Service interface for managing operations related to the Author model.
    /// Inherits from the generic IDAOService for common CRUD operations.
    /// </summary>
    public interface IAuthorService : IDAOService<Author>
    {
    }
}
