using Library.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper
{
    internal interface IBookDataServices
    {
        Book GetBookById(int id);

        void AddBook(Book book);

        void UpdateBook(Book book);

        void DeleteBook(Book book);
    }
}
