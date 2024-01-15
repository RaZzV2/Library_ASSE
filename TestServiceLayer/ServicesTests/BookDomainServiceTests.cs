// <copyright file="BookDomainServiceTests.cs" company="Transilvania University of Brasov">
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
    /// Unit tests for the <see cref="BookDomainService"/> class.
    /// </summary>
    [TestClass]
    public class BookDomainServiceTests
    {
        /// <summary>
        /// Represents a mock implementation of the data access object for managing BookDomain entities.
        /// </summary>
        private IBookDomainIDAO mockBookDomainIDAO;

        /// <summary>
        /// Represents a service for managing BookDomain entities.
        /// </summary>
        private BookDomainService bookDomainService;

        /// <summary>
        /// Represents an instance of the BookDomain entity associated with this object.
        /// </summary>
        private BookDomain bookDomain;

        /// <summary>
        /// Set up the test environment by creating mock objects and initializing test data.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            this.mockBookDomainIDAO = MockRepository.GenerateMock<IBookDomainIDAO>();
            this.bookDomainService = new BookDomainService(this.mockBookDomainIDAO);
            this.bookDomain = new BookDomain()
            {
                DomainName = "Matematica",
                Books = new List<Book>()
                {
                    new Book()
                    {
                    },
                },
                BookSubdomains = new List<BookDomain>()
                {
                    new BookDomain()
                    {
                        DomainName = "Informatica",
                    },
                },
                ParentDomain = new BookDomain()
                {
                    DomainName = "Stiinta",
                },
            };
        }

        /// <summary>
        /// Test the <see cref="BookDomainService.Add"/> method with a valid BookDomain, verifying that it calls the <see cref="IBookDomainIDAO"/> interface.
        /// </summary>
        [TestMethod]
        public void AddValidBookDomainCallsIBookDomainIDAO()
        {
            this.bookDomainService.Add(this.bookDomain);
        }

        /// <summary>
        /// Test the <see cref="BookDomainService.Add"/> method with an invalid BookDomain, ensuring that it throws a <see cref="ValidationException"/>.
        /// </summary>
        [TestMethod]
        public void AddInvalidBookDomainCallsIBookDomainIDAO()
        {
            this.bookDomain.ParentDomain = new BookDomain()
            {
                DomainName = "Matematica",
            };
            var exception = Assert.ThrowsException<ValidationException>(() => this.bookDomainService.Add(this.bookDomain));
            Assert.AreEqual("Circular dependency detected! Domain name must be unique within parent domains!", exception.Message);
        }

        /// <summary>
        /// Test the <see cref="BookDomainService.Delete"/> method to ensure it calls the <see cref="IBookDomainIDAO"/> interface when deleting a BookDomain.
        /// </summary>
        [TestMethod]
        public void RemoveBookDomainCallsIBookDomainIDAO()
        {
            this.bookDomainService.Delete(this.bookDomain);
        }

        /// <summary>
        /// Test the <see cref="BookDomainService.GetAll"/> method to ensure it calls the <see cref="IBookDomainIDAO"/> interface and returns the expected list of BookDomains.
        /// </summary>
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
                        },
                    },
                    BookSubdomains = new List<BookDomain>()
                    {
                        new BookDomain()
                        {
                            DomainName = "Informatica",
                        },
                    },
                    ParentDomain = new BookDomain()
                    {
                        DomainName = "Stiinta",
                    },
                },
            };
            this.mockBookDomainIDAO.Stub(x => x.GetAll()).Return(expectedBookDomains);
            var result = this.bookDomainService.GetAll();

            CollectionAssert.AreEqual(expectedBookDomains, result.ToList());
        }

        /// <summary>
        /// Test the <see cref="BookDomainService.GetById"/> method to ensure it returns the correct BookDomain based on the provided ID.
        /// </summary>
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
                    },
                },
                BookSubdomains = new List<BookDomain>
                {
                    new BookDomain()
                    {
                        DomainName = "Informatica",
                    },
                },
                ParentDomain = new BookDomain()
                {
                    DomainName = "Stiinta",
                },
            };

            this.mockBookDomainIDAO.Stub(x => x.GetById(Arg<int>.Is.Anything)).Return(expectedBookDomain);
            var result = this.bookDomainService.GetById(bookDomainId);

            Assert.AreEqual(expectedBookDomain, result);
        }

        /// <summary>
        /// Test the <see cref="BookDomainService.Update"/> method to ensure it calls the <see cref="IBookDomainIDAO"/> interface when updating a BookDomain.
        /// </summary>
        [TestMethod]
        public void UpdateBookDomainCallsBookDomainIDAO()
        {
            this.bookDomainService.Update(this.bookDomain);
        }
    }
}
