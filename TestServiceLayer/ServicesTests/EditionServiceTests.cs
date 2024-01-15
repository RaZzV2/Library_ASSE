// <copyright file="EditionServiceTests.cs" company="Transilvania University of Brasov">
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
    /// Unit tests for the EditionService class.
    /// </summary>
    [TestClass]
    public class EditionServiceTests
    {
        /// <summary>
        /// Represents a mock instance of the IEditionIDAO interface used for testing in the EditionService class.
        /// </summary>
        private IEditionIDAO mockEditionIDAO;

        /// <summary>
        /// Represents an instance of the EditionService class, which provides business logic operations related to editions.
        /// </summary>
        private EditionService editionService;

        /// <summary>
        /// Represents an instance of the Edition class, which encapsulates information about an edition in the system.
        /// </summary>
        private Edition edition;

        /// <summary>
        /// Initializes necessary objects before each test.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            this.mockEditionIDAO = MockRepository.GenerateMock<IEditionIDAO>();
            this.editionService = new EditionService(this.mockEditionIDAO);
            this.edition = new Edition
            {
                EditionId = 1,
                EditionName = "Editia Calugarului",
                EditionYear = 2003,
                PagesNumber = 99,
                BorrowableBooks = 23,
                UnBorrowableBooks = 32,
                BookType = Edition.Type.Board,
                Book = new Book()
                {
                },
            };
        }

        /// <summary>
        /// Unit test for the EditionService's Add method, ensuring it calls IEditionIDAO with a valid edition.
        /// </summary>
        [TestMethod]
        public void AddValidEditionCallsIEditionIDAO()
        {
            this.editionService.Add(this.edition);
            this.mockEditionIDAO.AssertWasCalled(mock => mock.Add(Arg<Edition>.Is.Equal(this.edition)), options => options.Repeat.Once());
        }

        /// <summary>
        /// Unit test for the EditionService's Add method, verifying that it throws a ValidationException for an invalid edition.
        /// </summary>
        [TestMethod]
        public void AddInvalidEditionCallsIEditionIDAO()
        {
            this.edition.EditionName = string.Empty;
            var exception = Assert.ThrowsException<ValidationException>(() => this.editionService.Add(this.edition));
            Assert.AreEqual("Edition name is required!", exception.Message);
        }

        /// <summary>
        /// Unit test for the EditionService's Delete method, confirming it calls IEditionIDAO to remove an edition.
        /// </summary>
        [TestMethod]
        public void RemoveEditionCallsIEditionIDAO()
        {
            this.editionService.Delete(this.edition);
            this.mockEditionIDAO.AssertWasCalled(mock => mock.Delete(Arg<Edition>.Is.Equal(this.edition)), options => options.Repeat.Once());
        }

        /// <summary>
        /// Unit test for the EditionService's GetAll method, validating that it calls IEditionIDAO and returns the expected editions.
        /// </summary>
        [TestMethod]
        public void GetAllEditionsCallsIEditionIDAO()
        {
            var expectedEditions = new List<Edition>
            {
                new Edition
                {
                    EditionName = "Editia Calugarului",
                    EditionYear = 2003,
                    PagesNumber = 99,
                    BorrowableBooks = 23,
                    UnBorrowableBooks = 32,
                    BookType = Edition.Type.Board,
                    Book = new Book()
                    {
                    },
                },
            };

            this.mockEditionIDAO.Stub(x => x.GetAll()).Return(expectedEditions);

            var result = this.editionService.GetAll();

            this.mockEditionIDAO.AssertWasCalled(mock => mock.GetAll(), options => options.Repeat.Once());
            CollectionAssert.AreEqual(expectedEditions, result.ToList());
        }

        /// <summary>
        /// Unit test for the EditionService's GetById method, ensuring it returns the correct edition based on the provided ID.
        /// </summary>
        [TestMethod]
        public void GetById_ReturnsCorrectEdition()
        {
            this.mockEditionIDAO.Stub(x => x.GetById(Arg<int>.Is.Anything)).Return(this.edition);

            var result = this.editionService.GetById(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(this.edition, result);
        }

        /// <summary>
        /// Unit test for the EditionService's Update method, verifying that it calls IEditionIDAO to update an edition.
        /// </summary>
        [TestMethod]
        public void UpdateEditionCallsIEditionIDAO()
        {
            this.editionService.Update(this.edition);
            this.mockEditionIDAO.AssertWasCalled(mock => mock.Update(Arg<Edition>.Is.Equal(this.edition)), options => options.Repeat.Once());
        }
    }
}
