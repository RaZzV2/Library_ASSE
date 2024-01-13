using DataMapper;
using DomainModel.CustomValidationHelpers;
using DomainModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Library.models;
using System.Linq;

namespace TestServiceLayer.ServicesTests
{
    [TestClass]
    public class BookDomainServiceTests
    {
        private IBookDomainIDAO mockBookDomainIDAO;
        private BookDomainService bookDomainService;
        private BookDomain bookDomain;

        [TestInitialize]
        public void SetUp()
        {
            mockBookDomainIDAO = MockRepository.GenerateMock<IBookDomainIDAO>();
            bookDomainService = new BookDomainService(mockBookDomainIDAO);
            bookDomain = new BookDomain()
            {
                DomainName = "Matematica",
                Books = new List<Book>()
                {
                    new Book()
                    {

                    }
                },
                BookSubdomains = new List<BookDomain>()
                {
                    new BookDomain()
                    {
                        DomainName = "Informatica"
                    }
                },
                ParentDomain = new BookDomain()
                {
                    DomainName = "Stiinta"
                }
            };
        }

        [TestMethod]
        public void AddValidBookDomainCallsIBookDomainIDAO()
        {
            bookDomainService.Add(this.bookDomain);
        }



        [TestMethod]
        public void AddInvalidBookDomainCallsIBookDomainIDAO()
        {
            this.bookDomain.ParentDomain = new BookDomain()
            {
                DomainName = "Matematica"
            };
            var exception = Assert.ThrowsException<ValidationException>(() => bookDomainService.Add(this.bookDomain));
            Assert.AreEqual("Circular dependency detected! Domain name must be unique within parent domains!", exception.Message);
        }

        [TestMethod]
        public void RemoveBookDomainCallsIBookDomainIDAO()
        {
            bookDomainService.Delete(this.bookDomain);
        }

        [TestMethod]
        public void GetAllBookDomainsCallsIBookDomainIDAO()
        {
            var expectedBookDomains = new List<BookDomain>
            {
                new BookDomain
                {
                    DomainName = "Matematica",
                    Books = new List<Book>()
                    {
                        new Book()
                        {

                        }
                    },
                    BookSubdomains = new List<BookDomain>()
                    {
                        new BookDomain()
                        {
                            DomainName = "Informatica"
                        }
                    },
                    ParentDomain = new BookDomain()
                    {
                        DomainName = "Stiinta"
                    }
                },
            };
            mockBookDomainIDAO.Stub(x => x.GetAll()).Return(expectedBookDomains);
            var result = bookDomainService.GetAll();

            CollectionAssert.AreEqual(expectedBookDomains, result.ToList());
        }

        [TestMethod]
        public void GetByIdReturnsCorrectBookDomain()
        {
            int bookDomainId = 1;
            var expectedBookDomain = new BookDomain
            {
                DomainName = "Matematica",
                Books = new List<Book>
                {
                    new Book()
                    {

                    }
                },
                BookSubdomains = new List<BookDomain>
                {
                    new BookDomain()
                    {
                        DomainName = "Informatica"
                    }
                },
                ParentDomain = new BookDomain()
                {
                    DomainName = "Stiinta"
                }
            };

            mockBookDomainIDAO.Stub(x => x.GetById(Arg<int>.Is.Anything)).Return(expectedBookDomain);
            var result = bookDomainService.GetById(bookDomainId);

            Assert.AreEqual(expectedBookDomain, result);
        }


        [TestMethod]
        public void UpdateBookDomainCallsBookDomainIDAO()
        {
            bookDomainService.Update(this.bookDomain);
        }
    }
}
