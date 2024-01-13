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
    public class AuthorServiceTests
    {
        private IAuthorIDAO mockAuthorIDAO;
        private AuthorService authorService;
        private Author author;

        [TestInitialize]
        public void SetUp()
        {
            mockAuthorIDAO = MockRepository.GenerateMock<IAuthorIDAO>();
            authorService = new AuthorService(mockAuthorIDAO);
            author = new Author
            {
                Id = 1,
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
        public void GetByIdReturnsCorrectAuthor()
        {
            int authorId = 1;
            mockAuthorIDAO.Stub(x => x.GetById(authorId)).Return(author);

            var result = authorService.GetById(authorId);

            Assert.AreEqual(author, result);
        }

        [TestMethod]
        public void AddValidAuthorCallsIAuthorIDAO()
        {
            authorService.Add(this.author);
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
        }

        [TestMethod]
        public void GetAllAuthorsCallsIAuthorIDAO()
        {
            var expectedAuthors = new List<Author>
            {
                new Author
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
                }
            };

            mockAuthorIDAO.Stub(x => x.GetAll()).Return(expectedAuthors);

            var result = authorService.GetAll();

            mockAuthorIDAO.AssertWasCalled(mock => mock.GetAll(), options => options.Repeat.Once());
            CollectionAssert.AreEqual(expectedAuthors, result.ToList());
        }

        [TestMethod]
        public void UpdateAuthorCallsIAuthorIDAO()
        {
            authorService.Update(this.author);
        }
    }
}
