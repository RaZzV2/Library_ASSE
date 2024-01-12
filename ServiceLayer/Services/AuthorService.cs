using DataMapper;
using Library.models;
using log4net;
using ServiceLayer.IServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void Delete(Author t)
        {
            iAuthorIDAO.Delete(t);
        }

        public Author GetById(int id)
        {
            return iAuthorIDAO.GetById(id);
        }

        public void Update(Author t)
        {
            try
            {
                Validator.ValidateObject(t, CreateValidationContext(t), true);
                iAuthorIDAO.Update(t);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
