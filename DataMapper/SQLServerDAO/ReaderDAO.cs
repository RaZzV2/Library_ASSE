using DomainModel;
using Library.models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.SQLServerDAO
{
    public class ReaderDAO : IReaderIDAO
    {
        public void Add(Reader t)
        {
            using (var context = new LibraryContext())
            {
                context.Readers.Add(t);
                context.SaveChanges();
            }
        }

        public void Delete(Reader t)
        {
            using (var context = new LibraryContext())
            {
                var newReader = new Reader { ReaderId = t.ReaderId };
                context.Readers.Attach(newReader);
                context.Readers.Remove(newReader);
                context.SaveChanges();
            }
        }

        public Reader GetById(int id)
        {
            using (var context = new LibraryContext())
            {
                return context.Readers.Where(reader => reader.ReaderId == id).SingleOrDefault();
            }
        }

        public void Update(Reader t)
        {
            using (var context = new LibraryContext())
            {
                var existingReader = context.Books.Find(t.ReaderId);

                if (existingReader != null)
                {
                    context.Entry(existingReader).CurrentValues.SetValues(t);
                    context.SaveChanges();
                }
                else
                {
                    throw new InvalidOperationException($"Reader with ID {t.ReaderId} not found");
                }
            }
        }
    }
   }
