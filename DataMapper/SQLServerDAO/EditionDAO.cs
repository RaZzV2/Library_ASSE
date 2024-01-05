using Library.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.SQLServerDAO
{
    internal class EditionDAO : EditionIDAO
    {
        public void Add(Edition t)
        {
            using (var context = new LibraryContext())
            {
                context.Editions.Add(t);
                context.SaveChanges();
            }
        }

        public void Delete(Edition t)
        {
            using (var context = new LibraryContext())
            {
                var newEdition = new Edition { EditionId = t.EditionId };
                context.Editions.Attach(newEdition);
                context.Editions.Remove(newEdition);
                context.SaveChanges();
            }
        }

        public Edition GetById(int id)
        {
            using (var context = new LibraryContext())
            {
                return context.Editions.Where(edition => edition.EditionId == id).SingleOrDefault();
            }
        }

        public void Update(Edition t)
        {
            using (var context = new LibraryContext())
            {
                var existingEdition = context.Editions.Find(t.EditionId);

                if (existingEdition != null)
                {
                    context.Entry(existingEdition).CurrentValues.SetValues(t);
                    context.SaveChanges();
                }
                else
                {
                    throw new InvalidOperationException($"Edition with ID {t.EditionId} not found");
                }
            }
        }
    }
}
