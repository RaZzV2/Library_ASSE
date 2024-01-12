using DataMapper;
using DomainModel.CustomValidationHelpers;
using DomainModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Library.models;
using ServiceLayer.IServices;
using System.ComponentModel.DataAnnotations;

namespace TestServiceLayer.ServicesTests
{
    [TestClass]
    public class EditionServiceTests
    {
        private IEditionIDAO mockEditionIDAO;
        private EditionService editionService;
        private Edition edition;

        private void AssertValidationException<T>(T instance, string expectedErrorMessage)
        {
            ModelValidationHelper.AssertValidationException(instance, expectedErrorMessage);
        }

        [TestInitialize]
        public void SetUp()
        {
            mockEditionIDAO = MockRepository.GenerateMock<IEditionIDAO>();
            editionService = new EditionService(mockEditionIDAO);
            this.edition = new Edition
            {
                EditionName = "Editia Calugarului",
                EditionYear = 2003,
                PagesNumber = 99,
                BorrowableBooks = 23,
                UnBorrowableBooks = 32,
                BookType = Edition.Type.Board,
                Book = new Book()
                {

                }
            };
        }

        [TestMethod]
        public void AddValidEditionCallsIEditionIDAO()
        {
            editionService.Add(this.edition);
            mockEditionIDAO.AssertWasCalled(mock => mock.Add(Arg<Edition>.Is.Equal(this.edition)), options => options.Repeat.Once());
        }

        [TestMethod]
        public void AddInvalidEditionCallsIEditionIDAO()
        {
            this.edition.EditionName = string.Empty;
            var exception = Assert.ThrowsException<ValidationException>(() => editionService.Add(this.edition));
            Assert.AreEqual("Edition name is required!", exception.Message);
        }

        [TestMethod]
        public void RemoveEditionCallsIEditionIDAO()
        {
            editionService.Delete(this.edition);
            mockEditionIDAO.AssertWasCalled(mock => mock.Delete(Arg<Edition>.Is.Equal(this.edition)), options => options.Repeat.Once());
        }

        [TestMethod]
        public void UpdateEditionCallsIEditionIDAO()
        {
            editionService.Update(this.edition);
            mockEditionIDAO.AssertWasCalled(mock => mock.Update(Arg<Edition>.Is.Equal(this.edition)), options => options.Repeat.Once());
        }
    }
}
