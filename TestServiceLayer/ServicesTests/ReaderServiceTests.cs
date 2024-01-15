// <copyright file="ReaderServiceTests.cs" company="Transilvania University of Brasov">
// Dragomir Razvan
// </copyright>

namespace TestServiceLayer.ServicesTests
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using DataMapper;
    using DomainModel;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Rhino.Mocks;
    using ServiceLayer.Services;

    /// <summary>
    /// Unit tests for the ReaderService class.
    /// </summary>
    [TestClass]
    public class ReaderServiceTests
    {
        /// <summary>
        /// Represents the mock object for the IReaderIDAO interface.
        /// </summary>
        private IReaderIDAO mockReaderIDAO;

        /// <summary>
        /// Represents the service responsible for handling Reader entities.
        /// </summary>
        private ReaderService readerService;

        /// <summary>
        /// Represents an instance of the Reader entity.
        /// </summary>
        private Reader reader;

        /// <summary>
        /// Initializes necessary objects before each test.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            this.mockReaderIDAO = MockRepository.GenerateMock<IReaderIDAO>();
            this.readerService = new ReaderService(this.mockReaderIDAO);
            this.reader = new Reader()
            {
                ReaderId = 1,
                ReaderFirstName = "Andrei",
                ReaderLastName = "Icsulescu",
                Address = "Strada x, Numarul 33",
                Role = true,
                PhoneNumber = "0732138913",
            };
        }

        /// <summary>
        /// Unit test for the ReaderService's Add method, ensuring it calls IReaderIDAO with a valid reader.
        /// </summary>
        [TestMethod]
        public void AddValidReaderCallsIReaderIDAO()
        {
            this.readerService.Add(this.reader);
            this.mockReaderIDAO.AssertWasCalled(mock => mock.Add(Arg<Reader>.Is.Equal(this.reader)), options => options.Repeat.Once());
        }

        /// <summary>
        /// Unit test for the ReaderService's Add method, verifying that it throws a ValidationException for an invalid reader.
        /// </summary>
        [TestMethod]
        public void AddInvalidReaderCallsIReaderIDAO()
        {
            this.reader.ReaderFirstName = "invalid_name++";
            var exception = Assert.ThrowsException<ValidationException>(() => this.readerService.Add(this.reader));
            Assert.AreEqual("First name must not have special characters!", exception.Message);
        }

        /// <summary>
        /// Unit test for the ReaderService's GetAll method, validating that it calls IReaderIDAO and returns the expected readers.
        /// </summary>
        [TestMethod]
        public void GetAllReadersCallsIReaderIDAO()
        {
            var expectedReaders = new List<Reader>
            {
                new Reader
                {
                    ReaderFirstName = "Andrei",
                    ReaderLastName = "Icsulescu",
                    Address = "Strada x, Numarul 33",
                    Role = true,
                    PhoneNumber = "0732138913",
                },
            };
            this.mockReaderIDAO.Stub(x => x.GetAll()).Return(expectedReaders);

            var result = this.readerService.GetAll();

            this.mockReaderIDAO.AssertWasCalled(mock => mock.GetAll(), options => options.Repeat.Once());
            CollectionAssert.AreEqual(expectedReaders, result.ToList());
        }

        /// <summary>
        /// Unit test for the ReaderService's Delete method, confirming it calls IReaderIDAO to remove a reader.
        /// </summary>
        [TestMethod]
        public void RemoveReaderCallsIReaderIDAO()
        {
            this.readerService.Delete(this.reader);
            this.mockReaderIDAO.AssertWasCalled(mock => mock.Delete(Arg<Reader>.Is.Equal(this.reader)), options => options.Repeat.Once());
        }

        /// <summary>
        /// Unit test for the ReaderService's GetById method, ensuring it returns the correct reader based on the provided ID.
        /// </summary>
        [TestMethod]
        public void GetById_ReturnsCorrectReader()
        {
            int readerId = 1;

            this.mockReaderIDAO.Stub(x => x.GetById(Arg<int>.Is.Anything)).Return(this.reader);

            var result = this.readerService.GetById(readerId);

            Assert.IsNotNull(result);
            Assert.AreEqual(this.reader, result);
        }

        /// <summary>
        /// Unit test for the ReaderService's Update method, verifying that it calls IReaderIDAO to update a reader.
        /// </summary>
        [TestMethod]
        public void UpdateReaderCallsIReaderIDAO()
        {
            this.readerService.Update(this.reader);
            this.mockReaderIDAO.AssertWasCalled(mock => mock.Update(Arg<Reader>.Is.Equal(this.reader)), options => options.Repeat.Once());
        }
    }
}
