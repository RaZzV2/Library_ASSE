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
using System.Linq;
using System.Text;

namespace TestServiceLayer.ServicesTests
{
    [TestClass]
    public class BorrowServiceTests
    {
        private IBorrowIDAO mockBorrowIDAO;
        private BorrowService borrowService;
        private Borrow borrow;

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
        }

        [TestMethod]
        public void GetByIdReturnsCorrectBorrow()
        {
            int borrowId = 1;
            mockBorrowIDAO.Stub(x => x.GetById(borrowId)).Return(borrow);

            var result = borrowService.GetById(borrowId);

            Assert.AreEqual(borrow, result);
        }


        [TestMethod]
        public void GetAllBorrowsCallsIBorrowIDAO()
        {  
            var expectedBorrows = new List<Borrow>
            {
                new Borrow
                {
                    Reader = new Reader(),
                    Edition = new Edition(),
                    BorrowStartDate = new DateTime(2003,12,1),
                    BorrowEndDate = new DateTime(2004,2,3),
                    IsReturned = false,
                    ExtendedBorrows = new List<ExtendedBorrow>()
                },
            };

            mockBorrowIDAO.Stub(x => x.GetAll()).Return(expectedBorrows);

            var result = borrowService.GetAll();

            CollectionAssert.AreEqual(expectedBorrows, result.ToList());
        }


        [TestMethod]
        public void UpdateBorrowCallsIBorrowIDAO()
        {
            borrowService.Update(this.borrow);
        }
    }
}
