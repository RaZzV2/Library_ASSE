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

    [TestClass]
    public class ReaderServiceTests
    {
        private IReaderIDAO mockReaderIDAO;
        private ReaderService readerService;
        private Reader reader;

        [TestInitialize]
        public void SetUp()
        {
            this.mockReaderIDAO = MockRepository.GenerateMock<IReaderIDAO>();
            this.readerService = new ReaderService(this.mockReaderIDAO);
            this.reader = new Reader()
            {
                ReaderId = 1,
                ReaderFirstName = "Andrei",
                ReaderLastName ="Icsulescu",
                Address = "Strada x, Numarul 33",
                Role = true,
                PhoneNumber = "0732138913"
            };
        }

        [TestMethod]
        public void AddValidReaderCallsIReaderIDAO()
        {
            this.readerService.Add(this.reader);
            this.mockReaderIDAO.AssertWasCalled(mock => mock.Add(Arg<Reader>.Is.Equal(this.reader)), options => options.Repeat.Once());
        }

        [TestMethod]
        public void AddInvalidReaderCallsIReaderIDAO()
        {
            this.reader.ReaderFirstName = "invalid_name++";
            var exception = Assert.ThrowsException<ValidationException>(() => this.readerService.Add(this.reader));
            Assert.AreEqual("First name must not have special characters!", exception.Message);
        }

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

        [TestMethod]
        public void RemoveReaderCallsIReaderIDAO()
        {
            this.readerService.Delete(this.reader);
            this.mockReaderIDAO.AssertWasCalled(mock => mock.Delete(Arg<Reader>.Is.Equal(this.reader)), options => options.Repeat.Once());
        }

        [TestMethod]
        public void GetById_ReturnsCorrectReader()
        {
            int readerId = 1;

            this.mockReaderIDAO.Stub(x => x.GetById(Arg<int>.Is.Anything)).Return(this.reader);

            var result = this.readerService.GetById(readerId);

            Assert.IsNotNull(result);
            Assert.AreEqual(this.reader, result);
        }

        [TestMethod]
        public void UpdateReaderCallsIReaderIDAO()
        {
            this.readerService.Update(this.reader);
            this.mockReaderIDAO.AssertWasCalled(mock => mock.Update(Arg<Reader>.Is.Equal(this.reader)), options => options.Repeat.Once());
        }
    }
}
