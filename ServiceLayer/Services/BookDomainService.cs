﻿using DataMapper;
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
    public class BookDomainService : IBookDomainService
    {
        readonly IBookDomainIDAO iBookDomainIDAO;
        private static readonly ILog Log = LogManager.GetLogger(typeof(BookDomainService));

        private ValidationContext CreateValidationContext(object instance)
        {
            return new ValidationContext(instance, null, null);
        }

        public BookDomainService(IBookDomainIDAO iBookDomainIDAO)
        {
            this.iBookDomainIDAO = iBookDomainIDAO;
        }

        public void Add(BookDomain t)
        {
            Validator.ValidateObject(t, CreateValidationContext(t), true);
            iBookDomainIDAO.Add(t);
            Log.Info("Book domain has been added successfully!");
        }

        public void Delete(BookDomain t)
        {
            iBookDomainIDAO.Delete(t);
        }

        public BookDomain GetById(int id)
        {
            return iBookDomainIDAO.GetById(id);
        }

        public void Update(BookDomain t)
        {
            try
            {
                Validator.ValidateObject(t, CreateValidationContext(t), true);
                iBookDomainIDAO.Update(t);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
