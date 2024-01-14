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

        public List<Reader> GetAll()
        {
            Log.Info("List of readers has been returned successfully!");
            return iReaderIDAO.GetAll();
        }

        public void Delete(Reader t)
        {
            iReaderIDAO.Delete(t);
            Log.Info("Reader has been deleted successfully!");
        }

        public void Update(Reader t)
        {
            iReaderIDAO.Update(t);
            Log.Info("Reader has been updated successfully!");
        }

        public Reader GetById(int id)
        {
            Log.Info("Reader has been returned successfully!");
            return iReaderIDAO.GetById(id);
        }
    }
}
