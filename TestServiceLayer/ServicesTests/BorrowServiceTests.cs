using DataMapper;
using DomainModel;
using DomainModel.CustomValidationHelpers;
using Library.models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TestServiceLayer.ServicesTests
{
    [TestClass]
    public class BorrowServiceTests
    {
        private IBorrowIDAO mockBorrowIDAO;
        private BorrowService borrowService;
        private Borrow borrow;

        private void AssertValidationException<T>(T instance, string expectedErrorMessage)
        {
            ModelValidationHelper.AssertValidationException(instance, expectedErrorMessage);
        }

        [TestInitialize]
        public void SetUp()
        {
            mockBorrowIDAO = MockRepository.GenerateMock<IBorrowIDAO>();
            borrowService = new BorrowService(mockBorrowIDAO);
            borrow = new Borrow()
            {
                Reader = new Reader(),
                Edition = new Edition(),
                BorrowStartDate = new DateTime(2003,12,1),
                BorrowEndDate = new DateTime(2004,2,3),
                IsReturned = false,
                ExtendedBorrows = new List<ExtendedBorrow>()
            };
        }

        [TestMethod]
        public void AddValidBorrowCallsIBorrowIDAO()
        {
            borrowService.Add(this.borrow);
            mockBorrowIDAO.AssertWasCalled(mock => mock.Add(Arg<Borrow>.Is.Equal(this.borrow)), options => options.Repeat.Once());
        }

        [TestMethod]
        public void AddInvalidBorrowCallsIBorrowIDAO()
        {
            this.borrow.Reader = null;
            var exception = Assert.ThrowsException<ValidationException>(() => borrowService.Add(this.borrow));
            Assert.AreEqual("Reader is required!", exception.Message);
        }

        [TestMethod]
        public void RemoveBorrowCallsIBorrowIDAO()
        {
            borrowService.Delete(this.borrow);
            mockBorrowIDAO.AssertWasCalled(mock => mock.Delete(Arg<Borrow>.Is.Equal(this.borrow)), options => options.Repeat.Once());
        }

        [TestMethod]
        public void UpdateBorrowCallsIBorrowIDAO()
        {
            borrowService.Update(this.borrow);
            mockBorrowIDAO.AssertWasCalled(mock => mock.Update(Arg<Borrow>.Is.Equal(this.borrow)), options => options.Repeat.Once());
        }
    }
}
