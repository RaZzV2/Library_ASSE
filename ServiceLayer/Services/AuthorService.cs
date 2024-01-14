using DataMapper;
using Library.Models;
using log4net;
using ServiceLayer.IServices;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.Services
{
    public class AuthorService : IAuthorService
    {
        readonly IAuthorIDAO iAuthorIDAO;
        private static readonly ILog Log = LogManager.GetLogger(typeof(AuthorService));

        private ValidationContext CreateValidationContext(object instance)
        {
            return new ValidationContext(instance, null, null);
        }

        public AuthorService(IAuthorIDAO iAuthorIDAO)
        {
            this.iAuthorIDAO = iAuthorIDAO;
        }

        public void Add(Author t)
        {
            Validator.ValidateObject(t, CreateValidationContext(t), true);
            iAuthorIDAO.Add(t);
            Log.Info("Author has been added successfully!");
        }

        public List<Author> GetAll()
        {
            Log.Info("List of authors has been returned successfully!");
            return iAuthorIDAO.GetAll();
        }

        public void Delete(Author t)
        {
            iAuthorIDAO.Delete(t);
            Log.Info("An author has been deleted!");
        }

        public Author GetById(int id)
        {
            Log.Info("Author has been returned!");
            return iAuthorIDAO.GetById(id);
        }

        public void Update(Author t)
        {
            Validator.ValidateObject(t, CreateValidationContext(t), true);
            iAuthorIDAO.Update(t);
            Log.Info("Author has been updated!");
        }
    }
}
