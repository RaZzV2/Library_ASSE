using Library.models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper
{
    public class BookDAO : IBookIDAO
    {
        public void Add(Book t)
        {
            using (var context = new LibraryContext())
            {
                context.Books.Add(t);
                context.SaveChanges();
            }
        }

        public void Delete(Book t)
        {
            using (var context = new LibraryContext())
            {
                var newBook = new Book { BookId = t.BookId };
                context.Books.Attach(newBook);
                context.Books.Remove(newBook);
                context.SaveChanges();
            }
        }

        public List<Book> GetAll()
        {
            using (var context = new LibraryContext())
            {
                return context.Books.ToList();
            }
        }

        public Book GetById(int id)
        {
            using (var context = new LibraryContext())
            {
                return context.Books.Where(book => book.BookId == id).SingleOrDefault();
            }
        }

        public void Update(Book t)
        {
            using (var context = new LibraryContext())
            {
                var existingBook = context.Books.Find(t.BookId);

                if (existingBook != null)
                {
                    context.Entry(existingBook).CurrentValues.SetValues(t);
                    context.SaveChanges();
                }
                else
                {
                    throw new InvalidOperationException($"Book with ID {t.BookId} not found");
                }
            }
        }
    }
}
