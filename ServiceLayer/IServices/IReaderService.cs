// <copyright file="IReaderService.cs" company="Transilvania University of Brasov">
// Dragomir Razvan
// </copyright>

namespace ServiceLayer.IServices
{
    using DomainModel;

    /// <summary>
    /// Service interface for managing operations related to the Reader model.
    /// Inherits from the generic IDAOService for common CRUD operations.
    /// </summary>
    public interface IReaderService : IDAOService<Reader>
    {
    }
}
