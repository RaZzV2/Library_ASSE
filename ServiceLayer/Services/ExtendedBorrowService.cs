﻿using DataMapper;
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
    public class ExtendedBorrowService : IExtendedBorrowService
    {
        readonly IExtendedBorrowIDAO iExtendedBorrowIDAO;
        private static readonly ILog Log = LogManager.GetLogger(typeof(ExtendedBorrowService));

        private ValidationContext CreateValidationContext(object instance)
        {
            return new ValidationContext(instance, null, null);
        }

        public ExtendedBorrowService(IExtendedBorrowIDAO iExtendedBorrowIDAO)
        {
            this.iExtendedBorrowIDAO = iExtendedBorrowIDAO;
        }
        public void Add(ExtendedBorrow t)
        {
            Validator.ValidateObject(t, CreateValidationContext(t), true);
            iExtendedBorrowIDAO.Add(t);
            Log.Info("Extended borrow has been added successfully!");
        }

        public List<ExtendedBorrow> GetAll()
        {
            return iExtendedBorrowIDAO.GetAll();
        }

        public void Delete(ExtendedBorrow t)
        {
            iExtendedBorrowIDAO.Delete(t);
        }

        public ExtendedBorrow GetById(int id)
        {
            return iExtendedBorrowIDAO.GetById(id);
        }

        public void Update(ExtendedBorrow t)
        {
            iExtendedBorrowIDAO.Update(t);
        }
    }
}
