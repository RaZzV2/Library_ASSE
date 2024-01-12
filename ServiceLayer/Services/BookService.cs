﻿using DataMapper;
using DomainModel.CustomValidationHelpers;
using Library.models;
using log4net;
using ServiceLayer.IServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class BookService : IBookService
    {
        readonly IBookIDAO iBookIDAO;
        private static readonly ILog Log = LogManager.GetLogger(typeof(BookService));

        private ValidationContext CreateValidationContext(object instance)
        {
            return new ValidationContext(instance, null, null);
        }

        public BookService(IBookIDAO iBookIDAO)
        {
            this.iBookIDAO = iBookIDAO;
        }

        public void Add(Book t)
        {
                Validator.ValidateObject(t, CreateValidationContext(t),true);
                iBookIDAO.Add(t);
                Log.Info("Book has been added successfully!");
        }

        public void Delete(Book t)
        {
            iBookIDAO.Delete(t);
        }

        public Book GetById(int id)
        {
            return iBookIDAO.GetById(id);
        }

        public void Update(Book t)
        {
            try
            {
                Validator.ValidateObject(t, CreateValidationContext(t), true);
                iBookIDAO.Update(t);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
