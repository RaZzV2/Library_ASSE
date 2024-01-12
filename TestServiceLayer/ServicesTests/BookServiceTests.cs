using DataMapper;
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
    public class BookServiceTests
    {
        private IBookIDAO mockBookIDAO;
        private BookService bookService;
        private Book book;

        private void AssertValidationException<T>(T instance, string expectedErrorMessage)
        {
            ModelValidationHelper.AssertValidationException(instance, expectedErrorMessage);
        }

        [TestInitialize]
        public void SetUp()
        {
            mockBookIDAO = MockRepository.GenerateMock<IBookIDAO>();
            bookService = new BookService(mockBookIDAO);
            book = new Book()
            {
                Title = "Amintiri din copilarie",
                Author = new List<Author>
                {
                    new Author
                    {
                        FirstName = "Ion",
                        LastName = "Creanga"
                    }
                },
                Domains = new List<BookDomain>
                {
                    new BookDomain
                    {

                    }
                },
                Editions = new List<Edition>()
            };
        }

        [TestMethod]
        public void AddValidBookCallsIBookIDAO()
        {
            bookService.Add(this.book);
            mockBookIDAO.AssertWasCalled(mock => mock.Add(Arg<Book>.Is.Equal(this.book)), options => options.Repeat.Once());
        }

        [TestMethod]
        public void AddInvalidBookCallsIBookIDAO()
        {
            this.book.Title = "+++33";
            var exception = Assert.ThrowsException<ValidationException>(() => bookService.Add(this.book));
            Assert.AreEqual("Title must not have special characters!", exception.Message);
        }

        [TestMethod]
        public void RemoveBookCallsIBookIDAO()
        {
            bookService.Delete(this.book);
            mockBookIDAO.AssertWasCalled(mock => mock.Delete(Arg<Book>.Is.Equal(this.book)), options => options.Repeat.Once());
        }

        [TestMethod]
        public void UpdateBookCallsIBookIDAO()
        {
            bookService.Update(this.book);
            mockBookIDAO.AssertWasCalled(mock => mock.Update(Arg<Book>.Is.Equal(this.book)), options => options.Repeat.Once());

        }
    }
}
