using Library.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.SQLServerDAO
{
    internal class BookDomainDAO : BookDomainIDAO
    {
        public void Add(BookDomain t)
        {
            using (var context = new LibraryContext())
            {
                context.BookDomains.Add(t);
                context.SaveChanges();
            }
        }

        public void Delete(BookDomain t)
        {
            using (var context = new LibraryContext())
            {
                var newBookDomain = new BookDomain { BookDomainId = t.BookDomainId };
                context.BookDomains.Attach(newBookDomain);
                context.BookDomains.Remove(newBookDomain);
                context.SaveChanges();
            }
        }

        public BookDomain GetById(int id)
        {
            using (var context = new LibraryContext())
            {
                return context.BookDomains.Where(bookDomain => bookDomain.BookDomainId == id).SingleOrDefault();
            }
        }

        public void Update(BookDomain t)
        {
            using (var context = new LibraryContext())
            {
                var existingBookDomain = context.Books.Find(t.BookDomainId);

                if (existingBookDomain != null)
                {
                    context.Entry(existingBookDomain).CurrentValues.SetValues(t);
                    context.SaveChanges();
                }
                else
                {
                    throw new InvalidOperationException($"Book Domain with ID {t.BookDomainId} not found");
                }
            }
        }
    }
}
