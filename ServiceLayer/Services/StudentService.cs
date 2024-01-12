using DataMapper;
using DomainModel;
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
    public class StudentService : IStudentService
    {
        readonly IStudentIDAO iStudentIDAO;
        private static readonly ILog Log = LogManager.GetLogger(typeof(StudentService));

        private ValidationContext CreateValidationContext(object instance)
        {
            return new ValidationContext(instance, null, null);
        }

        public StudentService(IStudentIDAO iStudentIDAO)
        {
            this.iStudentIDAO = iStudentIDAO;
        }

        public void Add(Student t)
        {
            try
            {
                Validator.ValidateObject(t, CreateValidationContext(t), true);
                iStudentIDAO.Add(t);
                Log.Info("Student has been added successfully!");
            }
            catch (ValidationException validationException)
            {
                throw validationException;
            }
        }

        public void Delete(Student t)
        {
            iStudentIDAO.Delete(t);
        }

        public void Update(Student t)
        {
            try
            {
                Validator.ValidateObject(t, CreateValidationContext(t), true);
                iStudentIDAO.Update(t);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public Student GetById(int id)
        {
            return iStudentIDAO.GetById(id);
        }
    }
}
