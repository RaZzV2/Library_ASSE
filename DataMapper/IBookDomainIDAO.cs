// <copyright file="IBookDomainIDAO.cs" company="Transilvania University Of Brasov">
// Dragomir Razvan
// </copyright>

namespace DataMapper
{
    using Library.Models;

    /// <summary>
    /// The <see cref="IBookDomainIDAO"/> interface defines the contract for Data Access Object (DAO) operations
    /// specific to the "BookDomain" entity in the data storage layer.
    /// </summary>
    public interface IBookDomainIDAO : IDAO<BookDomain>
    {
    }
}
