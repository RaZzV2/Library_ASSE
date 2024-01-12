using DataMapper;
using DomainModel;
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
    public class ReaderService : IReaderService
    {
        readonly IReaderIDAO iReaderIDAO;
        private static readonly ILog Log = LogManager.GetLogger(typeof(ReaderService));


        private ValidationContext CreateValidationContext(object instance)
        {
            return new ValidationContext(instance, null, null);
        }

        public ReaderService(IReaderIDAO iReaderIDAO)
        {
            this.iReaderIDAO = iReaderIDAO;
        }

        public void Add(Reader t)
        {
            try
            {
                Validator.ValidateObject(t, CreateValidationContext(t), true);
                iReaderIDAO.Add(t);
                Log.Info("Reader has been added successfully!");
            }
            catch (ValidationException validationException)
            {
                throw validationException;
            }
        }

        public void Delete(Reader t)
        {
            iReaderIDAO.Delete(t);
        }

        public void Update(Reader t)
        {
            try
            {
                Validator.ValidateObject(t, CreateValidationContext(t), true);
                iReaderIDAO.Update(t);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public Reader GetById(int id)
        {
            return iReaderIDAO.GetById(id);
        }
    }
}
