// <copyright file="AuthorServiceTests.cs" company="Transilvania University of Brasov">
// Dragomir Razvan
// </copyright>

namespace TestServiceLayer.ServicesTests
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using DataMapper;
    using Library.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Rhino.Mocks;
    using ServiceLayer.Services;

    /// <summary>
    /// Unit tests for the <see cref="AuthorService"/> class.
    /// </summary>
    [TestClass]
    public class AuthorServiceTests
    {
        /// <summary>
        /// Represents a mock instance of the IAuthorIDAO interface for testing purposes.
        /// </summary>
        private IAuthorIDAO mockAuthorIDAO;

        /// <summary>
        /// Represents an instance of the AuthorService, providing operations related to authors.
        /// </summary>
        private AuthorService authorService;

        /// <summary>
        /// Represents an instance of the Author model, containing information about an author.
        /// </summary>
        private Author author;

        /// <summary>
        /// Set up the test environment by creating mock objects and initializing test data.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            this.mockAuthorIDAO = MockRepository.GenerateMock<IAuthorIDAO>();
            this.authorService = new AuthorService(this.mockAuthorIDAO);
            this.author = new Author
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
                                    DomainName = "Matematica",
                                },
                            },
                        },
                    },
                },
            };
        }

        /// <summary>
        /// Test the <see cref="AuthorService.GetById"/> method to ensure it returns the correct Author based on the provided ID.
        /// </summary>
        [TestMethod]
        public void GetByIdReturnsCorrectAuthor()
        {
            int authorId = 1;
            this.mockAuthorIDAO.Stub(x => x.GetById(authorId)).Return(this.author);

            var result = this.authorService.GetById(authorId);

            Assert.AreEqual(this.author, result);
        }

        /// <summary>
        /// Test the <see cref="AuthorService.Add"/> method with a valid Author, verifying that it calls the <see cref="IAuthorIDAO"/> interface.
        /// </summary>
        [TestMethod]
        public void AddValidAuthorCallsIAuthorIDAO()
        {
            this.authorService.Add(this.author);
        }

        /// <summary>
        /// Test the <see cref="AuthorService.Add"/> method with an invalid Author, ensuring that it throws a <see cref="ValidationException"/>.
        /// </summary>
        [TestMethod]
        public void AddInvalidAuthorCallsIAuthorIDAO()
        {
            this.author.FirstName = "invalid_name++";
            var exception = Assert.ThrowsException<ValidationException>(() => this.authorService.Add(this.author));
            Assert.AreEqual("First name must not have special characters!", exception.Message);
        }

        /// <summary>
        /// Test the <see cref="AuthorService.Delete"/> method to ensure it calls the <see cref="IAuthorIDAO"/> interface when deleting an Author.
        /// </summary>
        [TestMethod]
        public void RemoveAuthorCallsIAuthorIDAO()
        {
            this.authorService.Delete(this.author);
        }

        /// <summary>
        /// Test the <see cref="AuthorService.GetAll"/> method to ensure it calls the <see cref="IAuthorIDAO"/> interface and returns the expected list of Authors.
        /// </summary>
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
                                        DomainName = "Matematica",
                                    },
                                },
                            },
                        },
                    },
                },
            };

            this.mockAuthorIDAO.Stub(x => x.GetAll()).Return(expectedAuthors);

            var result = this.authorService.GetAll();

            this.mockAuthorIDAO.AssertWasCalled(mock => mock.GetAll(), options => options.Repeat.Once());
            CollectionAssert.AreEqual(expectedAuthors, result.ToList());
        }

        /// <summary>
        /// Test the <see cref="AuthorService.Update"/> method to ensure it calls the <see cref="IAuthorIDAO"/> interface when updating an Author.
        /// </summary>
        [TestMethod]
        public void UpdateAuthorCallsIAuthorIDAO()
        {
            this.authorService.Update(this.author);
        }
    }
}
