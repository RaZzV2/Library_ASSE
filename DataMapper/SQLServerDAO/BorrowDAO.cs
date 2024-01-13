using DomainModel;
using Library.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.SQLServerDAO
{
    internal class BorrowDAO : IBorrowIDAO
    {
        public void Add(Borrow t)
        {
            using (var context = new LibraryContext())
            {
                context.Borrows.Add(t);
                context.SaveChanges();
            }
        }

        public void Delete(Borrow t)
        {
            using (var context = new LibraryContext())
            {
                var newBorrow = new Borrow { Id = t.Id };
                context.Borrows.Attach(newBorrow);
                context.Borrows.Remove(newBorrow);
                context.SaveChanges();
            }
        }

        public List<Borrow> GetAll()
        {
            using (var context = new LibraryContext())
            {
                return context.Borrows.ToList();
            }
        }

        public Borrow GetById(int id)
        {
            using (var context = new LibraryContext())
            {
                return context.Borrows.Where(borrow => borrow.Id == id).SingleOrDefault();
            }
        }

        public void Update(Borrow t)
        {
            using (var context = new LibraryContext())
            {
                var existingBorrow = context.Borrows.Find(t.Id);

                if (existingBorrow != null)
                {
                    context.Entry(existingBorrow).CurrentValues.SetValues(t);
                    context.SaveChanges();
                }
                else
                {
                    throw new InvalidOperationException($"Borrow with ID {t.Id} not found");
                }
            }
        }
    }
}
