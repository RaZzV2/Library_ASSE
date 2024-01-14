namespace ServiceLayer.IServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DomainModel;

    /// <summary>
    /// Service interface for managing operations related to the Borrow model.
    /// Inherits from the generic IDAOService for common CRUD operations.
    /// </summary>
    public interface IBorrowService : IDAOService<Borrow>
    {
    }
}
