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
    public class AuthorServiceTests
    {
        private IAuthorIDAO mockAuthorIDAO;
        private AuthorService authorService;
        private Author author;

        private void AssertValidationException<T>(T instance, string expectedErrorMessage)
        {
            ModelValidationHelper.AssertValidationException(instance, expectedErrorMessage);
        }

        [TestInitialize]
        public void SetUp()
        {
            mockAuthorIDAO = MockRepository.GenerateMock<IAuthorIDAO>();
            authorService = new AuthorService(mockAuthorIDAO);
            author = new Author
            {
                FirstName = "Ionel",
                LastName = "Dorel",
                Books = new List<Book>()
                {
                    new Book
                    {
                        Title = "Test",
                        Domains = new List<BookDomain>()
                        {
                            new BookDomain
                            {
                                DomainName = "Stiinta",
                                ParentDomain = new BookDomain
                                {
                                    DomainName = "Matematica"
                                }
                            }
                        }
                    }
                }
            };
        }

        [TestMethod]
        public void AddValidAuthorCallsIAuthorIDAO()
        {
            authorService.Add(this.author);
            mockAuthorIDAO.AssertWasCalled(mock => mock.Add(Arg<Author>.Is.Equal(this.author)), options => options.Repeat.Once());
        }

        [TestMethod]
        public void AddInvalidAuthorCallsIAuthorIDAO()
        {
            this.author.FirstName = "invalid_name++";
            var exception = Assert.ThrowsException<ValidationException>(() => authorService.Add(this.author));
            Assert.AreEqual("First name must not have special characters!", exception.Message);
        }

        [TestMethod]
        public void RemoveAuthorCallsIAuthorIDAO()
        {
            authorService.Delete(this.author);
            mockAuthorIDAO.AssertWasCalled(mock => mock.Delete(Arg<Author>.Is.Equal(this.author)), options => options.Repeat.Once());
        }

        [TestMethod]
        public void UpdateAuthorCallsIAuthorIDAO()
        {
            authorService.Update(this.author);
            mockAuthorIDAO.AssertWasCalled(mock => mock.Update(Arg<Author>.Is.Equal(this.author)), options => options.Repeat.Once());
        }
    }
}
