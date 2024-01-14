﻿namespace ServiceLayer.IServices
{
    using Library.Models;

    /// <summary>
    /// Service interface for managing operations related to the Edition model.
    /// Inherits from the generic IDAOService for common CRUD operations.
    /// </summary>
    public interface IEditionService : IDAOService<Edition>
    {
    }
}
