using Library.models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.SQLServerDAO
{
    internal class AuthorDAO : AuthorIDAO
    {
        public void Add(Author t)
        {
            using (var context = new LibraryContext())
            {
                context.Author.Add(t);
                context.SaveChanges();
            }
        }

        public void Delete(Author t)
        {
            using (var context = new LibraryContext())
            {
                var newAuthor = new Author { Id = t.Id };
                context.Author.Attach(newAuthor);
                context.Author.Remove(newAuthor);
                context.SaveChanges();
            }
        }

        public Author GetById(int id)
        {
            using (var context = new LibraryContext())
            {
                return context.Author.Where(author => author.Id == id).SingleOrDefault();
            }
        }

        public void Update(Author t)
        {
            using (var context = new LibraryContext())
            {
                var existingAuthor = context.Author.Find(t.Id);

                if (existingAuthor != null)
                {
                    context.Entry(existingAuthor).CurrentValues.SetValues(t);
                    context.SaveChanges();
                }
                else
                {
                    throw new InvalidOperationException($"Author with ID {t.Id} not found");
                }
            }
        }
    }
}
