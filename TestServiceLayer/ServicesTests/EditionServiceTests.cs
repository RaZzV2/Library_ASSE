using DataMapper;
using DomainModel.CustomValidationHelpers;
using DomainModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Library.Models;
using ServiceLayer.IServices;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TestServiceLayer.ServicesTests
{
    [TestClass]
    public class EditionServiceTests
    {
        private IEditionIDAO mockEditionIDAO;
        private EditionService editionService;
        private Edition edition;

        [TestInitialize]
        public void SetUp()
        {
            mockEditionIDAO = MockRepository.GenerateMock<IEditionIDAO>();
            editionService = new EditionService(mockEditionIDAO);
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

                }
            };
        }

        [TestMethod]
        public void AddValidEditionCallsIEditionIDAO()
        {
            this.editionService.Add(this.edition);
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
                    }
                },
            };

            mockEditionIDAO.Stub(x => x.GetAll()).Return(expectedEditions);

            var result = this.editionService.GetAll();

            mockEditionIDAO.AssertWasCalled(mock => mock.GetAll(), options => options.Repeat.Once());
            CollectionAssert.AreEqual(expectedEditions, result.ToList());
        }

        [TestMethod]
        public void GetById_ReturnsCorrectEdition()
        {
            
            mockEditionIDAO.Stub(x => x.GetById(Arg<int>.Is.Anything)).Return(this.edition);

            var result = editionService.GetById(1); 

            Assert.IsNotNull(result);
            Assert.AreEqual(this.edition, result);
        }

        [TestMethod]
        public void UpdateEditionCallsIEditionIDAO()
        {
            editionService.Update(this.edition);
            mockEditionIDAO.AssertWasCalled(mock => mock.Update(Arg<Edition>.Is.Equal(this.edition)), options => options.Repeat.Once());
        }
    }
}
