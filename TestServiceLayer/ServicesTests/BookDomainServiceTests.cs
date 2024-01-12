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

namespace TestServiceLayer.ServicesTests
{
    [TestClass]
    public class BookDomainServiceTests
    {
        private IBookDomainIDAO mockBookDomainIDAO;
        private BookDomainService bookDomainService;
        private BookDomain bookDomain;

        private void AssertValidationException<T>(T instance, string expectedErrorMessage)
        {
            ModelValidationHelper.AssertValidationException(instance, expectedErrorMessage);
        }

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
            mockBookDomainIDAO.AssertWasCalled(mock => mock.Add(Arg<BookDomain>.Is.Equal(this.bookDomain)), options => options.Repeat.Once());
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
            mockBookDomainIDAO.AssertWasCalled(mock => mock.Delete(Arg<BookDomain>.Is.Equal(this.bookDomain)), options => options.Repeat.Once());
        }

        [TestMethod]
        public void UpdateBookDomainCallsBookDomainIDAO()
        {
            mockBookDomainIDAO.Update(this.bookDomain);
            mockBookDomainIDAO.AssertWasCalled(mock => mock.Update(Arg<BookDomain>.Is.Equal(this.bookDomain)), options => options.Repeat.Once());
        }
    }
}
