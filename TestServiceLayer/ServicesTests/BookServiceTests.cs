// <copyright file="BookServiceTests.cs" company="Transilvania University of Brasov">
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
    /// Unit tests for the <see cref="BookService"/> class.
    /// </summary>
    [TestClass]
    public class BookServiceTests
    {
        /// <summary>
        /// Represents a mock instance of the IBookIDAO interface used for testing in the BookService class.
        /// </summary>
        private IBookIDAO mockBookIDAO;

        /// <summary>
        /// Represents an instance of the BookService class, which provides business logic operations related to books.
        /// </summary>
        private BookService bookService;

        /// <summary>
        /// Represents an instance of the Book class, which encapsulates information about a book in the system.
        /// </summary>
        private Book book;

        /// <summary>
        /// Set up the test environment by creating mock objects and initializing test data.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            this.mockBookIDAO = MockRepository.GenerateMock<IBookIDAO>();
            this.bookService = new BookService(this.mockBookIDAO, 3);
            this.book = new Book()
            {
                BookId = 1,
                Title = "Amintiri din copilarie",
                Author = new List<Author>
                {
                    new Author
                    {
                        FirstName = "Ion",
                        LastName = "Creanga",
                    },
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
                    },
                },
                Editions = new List<Edition>(),
            };
        }

        /// <summary>
        /// Test the <see cref="BookService.VerifyMoreDomainsThanNecessary"/> method to ensure it throws a <see cref="ValidationException"/> when a book has more domains than necessary.
        /// </summary>
        [TestMethod]
        public void MoreThanNecessaryDomains()
        {
            var exception = Assert.ThrowsException<ValidationException>(() =>
                this.bookService.VerifyMoreDomainsThanNecessary(this.book));
            Assert.AreEqual("A book shouldn't have more than 3 domains", exception.Message);
        }

        /// <summary>
        /// Test the <see cref="BookService.VerifyMoreDomainsThanNecessary"/> method to ensure it allows a book with the necessary number of domains.
        /// </summary>
        [TestMethod]
        public void NecessaryDomains()
        {
            this.book.Domains = new List<BookDomain>()
            {
                new BookDomain { },
            };
            this.bookService.VerifyMoreDomainsThanNecessary(this.book);
        }

        /// <summary>
        /// Test the <see cref="BookService.Add"/> method with a valid Book, verifying that it calls the <see cref="IBookIDAO"/> interface.
        /// </summary>
        [TestMethod]
        public void AddValidBookCallsIBookIDAO()
        {
            this.bookService.Add(this.book);
        }

        /// <summary>
        /// Test the <see cref="BookService.Add"/> method with an invalid Book, ensuring that it throws a <see cref="ValidationException"/>.
        /// </summary>
        [TestMethod]
        public void AddInvalidBookCallsIBookIDAO()
        {
            this.book.Title = "+++33";
            var exception = Assert.ThrowsException<ValidationException>(() => this.bookService.Add(this.book));
            Assert.AreEqual("Title must not have special characters!", exception.Message);
        }

        /// <summary>
        /// Test the <see cref="BookService.Delete"/> method to ensure it calls the <see cref="IBookIDAO"/> interface when deleting a Book.
        /// </summary>
        [TestMethod]
        public void RemoveBookCallsIBookIDAO()
        {
            this.bookService.Delete(this.book);
        }

        /// <summary>
        /// Test the <see cref="BookService.GetAll"/> method to ensure it calls the <see cref="IBookIDAO"/> interface and returns the expected list of Books.
        /// </summary>
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
                            LastName = "Creanga",
                        },
                    },
                    Domains = new List<BookDomain>
                    {
                        new BookDomain
                        {
                        },
                    },
                    Editions = new List<Edition>(),
                },
            };

            this.mockBookIDAO.Stub(x => x.GetAll()).Return(expectedBooks);

            var result = this.bookService.GetAll();

            this.mockBookIDAO.AssertWasCalled(mock => mock.GetAll(), options => options.Repeat.Once());
            CollectionAssert.AreEqual(expectedBooks, result.ToList());
        }

        /// <summary>
        /// Test the <see cref="BookService.GetById"/> method to ensure it returns the correct Book based on the provided ID.
        /// </summary>
        [TestMethod]
        public void GetByIdReturnsCorrectBook()
        {
            int bookId = 1;
            this.mockBookIDAO.Stub(x => x.GetById(bookId)).Return(this.book);

            var result = this.bookService.GetById(bookId);

            Assert.AreEqual(this.book, result);
        }

        /// <summary>
        /// Test the <see cref="BookService.Update"/> method to ensure it calls the <see cref="IBookIDAO"/> interface when updating a Book.
        /// </summary>
        [TestMethod]
        public void UpdateBookCallsIBookIDAO()
        {
            this.bookService.Update(this.book);
        }
    }
}
