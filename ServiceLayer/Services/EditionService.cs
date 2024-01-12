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
    public class EditionService : IEditionService
    {
        readonly IEditionIDAO iEditionIDAO;
        private static readonly ILog Log = LogManager.GetLogger(typeof(EditionService));

        private ValidationContext CreateValidationContext(object instance)
        {
            return new ValidationContext(instance, null, null);
        }

        public EditionService(IEditionIDAO iEditionIDAO)
        {
            this.iEditionIDAO = iEditionIDAO;
        }
        public void Add(Edition t)
        {
            Validator.ValidateObject(t, CreateValidationContext(t), true);
            iEditionIDAO.Add(t);
            Log.Info("Edition has been added successfully!");
        }

        public void Delete(Edition t)
        {
            iEditionIDAO.Delete(t);
        }

        public Edition GetById(int id)
        {
            return iEditionIDAO.GetById(id);
        }

        public void Update(Edition t)
        {
            try
            {
                Validator.ValidateObject(t, CreateValidationContext(t), true);
                iEditionIDAO.Update(t);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
