namespace ServiceLayer.IServices
{
    using Library.Models;

    /// <summary>
    /// Service interface for managing operations related to the Book model.
    /// Inherits from the generic IDAOService for common CRUD operations.
    /// </summary>
    public interface IBookService : IDAOService<Book>
    {
    }
}
