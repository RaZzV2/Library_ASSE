using DataMapper;
using DomainModel;
using DomainModel.CustomValidationHelpers;
using Library.models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using ServiceLayer.IServices;
using ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static log4net.Appender.RollingFileAppender;

namespace TestServiceLayer.ServicesTests
{
    [TestClass]
    public class ExtendedBorrowServiceTests
    {
        private IExtendedBorrowIDAO mockExtendedBorrowIDAO;
        private ExtendedBorrowService extendedBorrowService;
        private ExtendedBorrow extendedBorrow;

        [TestInitialize]
        public void SetUp()
        {
            var dateTime = new DateTime(2022, 1, 1, 12, 0, 0);
            mockExtendedBorrowIDAO = MockRepository.GenerateMock<IExtendedBorrowIDAO>();
            extendedBorrowService = new ExtendedBorrowService(mockExtendedBorrowIDAO);
            this.extendedBorrow = new ExtendedBorrow()
            {

                Borrow = new Borrow
                {
                    Reader = new Reader { },
                    Edition = new Edition { },
                    BorrowStartDate = dateTime,
                    BorrowEndDate = dateTime.AddDays(7),
                    IsReturned = false
                },
                Date = new DateTime(2023, 1, 1, 12, 0, 0)
            };
        }

        [TestMethod]
        public void AddValidExtendedBorrowCallsIExtendedBorrowIDAO()
        {
            extendedBorrowService.Add(this.extendedBorrow);
            mockExtendedBorrowIDAO.AssertWasCalled(mock => mock.Add(Arg<ExtendedBorrow>.Is.Equal(this.extendedBorrow)), options => options.Repeat.Once());
        }

        [TestMethod]
        public void AddInvalidExtendedBorrowCallsIExtendedBorrowIDAO()
        {
            this.extendedBorrow = new ExtendedBorrow
            {
                Borrow = null,
                Date = new DateTime(2022, 1, 5)
            };
            var exception = Assert.ThrowsException<ValidationException>(() => extendedBorrowService.Add(this.extendedBorrow));
            Assert.AreEqual("A borrow is required!", exception.Message);
        }

        [TestMethod]
        public void RemoveExtendedBorrowCallsIExtendedBorrowIDAO()
        {
            extendedBorrowService.Delete(this.extendedBorrow);
            mockExtendedBorrowIDAO.AssertWasCalled(mock => mock.Delete(Arg<ExtendedBorrow>.Is.Equal(this.extendedBorrow)), options => options.Repeat.Once());
        }

        [TestMethod]
        public void UpdateExtendedBorrowCallsIExtendedBorrowIDAO()
        {
            extendedBorrowService.Update(this.extendedBorrow);
            mockExtendedBorrowIDAO.AssertWasCalled(mock => mock.Update(Arg<ExtendedBorrow>.Is.Equal(this.extendedBorrow)), options => options.Repeat.Once());
        }
    }
}
