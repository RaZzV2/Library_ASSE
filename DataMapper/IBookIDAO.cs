// <copyright file="IBookIDAO.cs" company="Transilvania University Of Brasov">
// Dragomir Razvan
// </copyright>

namespace DataMapper
{
    using Library.Models;

    /// <summary>
    /// The <see cref="IBookIDAO"/> interface defines the contract for Data Access Object (DAO) operations
    /// specific to the "Book" entity in the data storage layer.
    /// </summary>
    public interface IBookIDAO : IDAO<Book>
    {
    }
}
