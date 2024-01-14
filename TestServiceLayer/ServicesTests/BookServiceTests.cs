using DataMapper;
using DomainModel.CustomValidationHelpers;
using Library.Models;
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
    public class BookServiceTests
    {
        private IBookIDAO mockBookIDAO;
        private BookService bookService;
        private Book book;

        [TestInitialize]
        public void SetUp()
        {
            mockBookIDAO = MockRepository.GenerateMock<IBookIDAO>();
            bookService = new BookService(mockBookIDAO,3);
            book = new Book()
            {
                BookId = 1,
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

                    },
                    new BookDomain
                    {

                    },
                    new BookDomain
                    {

                    }
                },
                Editions = new List<Edition>()
            };
        }

        [TestMethod]
        public void MoreThanNecessaryDomains()
        {
            var exception = Assert.ThrowsException<ValidationException>(() =>
                bookService.VerifyMoreDomainsThanNecessary(this.book));
            Assert.AreEqual("A book shouldn't have more than 3 domains", exception.Message);
        }

        [TestMethod]
        public void NecessaryDomains()
        {
            this.book.Domains = new List<BookDomain>()
            {
                new BookDomain {}
            };
            bookService.VerifyMoreDomainsThanNecessary(this.book);
        }

        [TestMethod]
        public void AddValidBookCallsIBookIDAO()
        {
            bookService.Add(this.book);
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
        }

        [TestMethod]
        public void GetAllBooksCallsIBookIDAO()
        {
            var expectedBooks = new List<Book>
            {
                new Book
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
                },
            };

            mockBookIDAO.Stub(x => x.GetAll()).Return(expectedBooks);

            var result = bookService.GetAll();

            mockBookIDAO.AssertWasCalled(mock => mock.GetAll(), options => options.Repeat.Once());
            CollectionAssert.AreEqual(expectedBooks, result.ToList());
        }

        [TestMethod]
        public void GetByIdReturnsCorrectBook()
        {
            int bookId = 1;
            mockBookIDAO.Stub(x => x.GetById(bookId)).Return(book);

            var result = bookService.GetById(bookId);

            Assert.AreEqual(book, result);
        }


        [TestMethod]
        public void UpdateBookCallsIBookIDAO()
        {
            bookService.Update(this.book);
        }
    }
}
