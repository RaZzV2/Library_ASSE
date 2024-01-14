﻿namespace ServiceLayer.IServices
{
    using DomainModel;

    /// <summary>
    /// Service interface for managing operations related to the ExtendedBorrow model.
    /// Inherits from the generic IDAOService for common CRUD operations.
    /// </summary>
    public interface IExtendedBorrowService : IDAOService<ExtendedBorrow>
    {
    }
}
