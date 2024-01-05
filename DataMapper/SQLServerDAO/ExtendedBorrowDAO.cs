using DomainModel;
using Library.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.SQLServerDAO
{
    internal class ExtendedBorrowDAO : ExtendedBorrowIDAO
    {
        public void Add(ExtendedBorrow t)
        {
            using (var context = new LibraryContext())
            {
                context.ExtendedBorrows.Add(t);
                context.SaveChanges();
            }
        }

        public void Delete(ExtendedBorrow t)
        {
            using (var context = new LibraryContext())
            {
                var newExtendedBorrow = new ExtendedBorrow { Id = t.Id };
                context.ExtendedBorrows.Attach(newExtendedBorrow);
                context.ExtendedBorrows.Remove(newExtendedBorrow);
                context.SaveChanges();
            }
        }

        public ExtendedBorrow GetById(int id)
        {
            using (var context = new LibraryContext())
            {
                return context.ExtendedBorrows.Where(extendedBorrow => extendedBorrow.Id == id).SingleOrDefault();
            }
        }

        public void Update(ExtendedBorrow t)
        {
            using (var context = new LibraryContext())
            {
                var existingExtendedBorrow = context.ExtendedBorrows.Find(t.Id);

                if (existingExtendedBorrow != null)
                {
                    context.Entry(existingExtendedBorrow).CurrentValues.SetValues(t);
                    context.SaveChanges();
                }
                else
                {
                    throw new InvalidOperationException($"Extended Borrow with ID {t.Id} not found");
                }
            }
        }
    }
}
