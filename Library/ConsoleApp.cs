using Library.models;
using DataMapper;
using System;
using System.Configuration;
using System.Data.Entity;

namespace Library
{
    internal class ConsoleApp
    {
        static void Main(string[] args)
        {
            var context = new LibraryContext();
            Book book = new Book();
            BookDAO bookDAO = new DataMapper.BookDAO();
            book.BookId = 2;
            book.Title= "Clementina";
            bookDAO.Update(book);
            
        }
    }
}
