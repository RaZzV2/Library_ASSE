using DataMapper;
using DomainModel;
using DomainModel.CustomValidationHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using ServiceLayer.IServices;
using ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TestServiceLayer.ServicesTests
{
    [TestClass]
    public class ReaderServiceTests
    {
        private IReaderIDAO mockReaderIDAO;
        private ReaderService readerService;
        private Reader reader;

        private void AssertValidationException<T>(T instance, string expectedErrorMessage)
        {
            ModelValidationHelper.AssertValidationException(instance, expectedErrorMessage);
        }

        [TestInitialize]
        public void SetUp()
        {
            mockReaderIDAO = MockRepository.GenerateMock<IReaderIDAO>();
            readerService = new ReaderService(mockReaderIDAO);
            reader = new Reader()
            {
                ReaderFirstName = "Andrei",
                ReaderLastName ="Icsulescu",
                Address = "Strada x, Numarul 33",
                Role = true,
                PhoneNumber ="0732138913"
            };
        }

        [TestMethod]
        public void AddValidReaderCallsIReaderIDAO()
        {
            readerService.Add(this.reader);
            mockReaderIDAO.AssertWasCalled(mock => mock.Add(Arg<Reader>.Is.Equal(this.reader)), options => options.Repeat.Once());
        }

        [TestMethod]
        public void AddInvalidReaderCallsIReaderIDAO()
        {
            this.reader.ReaderFirstName = "invalid_name++";
            var exception = Assert.ThrowsException<ValidationException>(() => readerService.Add(this.reader));
            Assert.AreEqual("First name must not have special characters!", exception.Message);
        }

        [TestMethod]
        public void RemoveReaderCallsIReaderIDAO()
        {
            readerService.Delete(this.reader);
            mockReaderIDAO.AssertWasCalled(mock => mock.Delete(Arg<Reader>.Is.Equal(this.reader)), options => options.Repeat.Once());
        }

        [TestMethod]
        public void UpdateReaderCallsIReaderIDAO()
        {
            readerService.Update(this.reader);
            mockReaderIDAO.AssertWasCalled(mock => mock.Update(Arg<Reader>.Is.Equal(this.reader)), options => options.Repeat.Once());
        }
    }
}
