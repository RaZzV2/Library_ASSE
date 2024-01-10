using DataMapper;
using Library.models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestServiceLayer.ServicesTests
{
    [TestClass]
    public class BookServiceTests
    {
        [TestMethod]
        public void Add_ValidBook_CallsIBookIDAOMethod()
        {
            var mockBookIDAO = MockRepository.GenerateMock<IBookIDAO>();
            var bookService = new BookService(mockBookIDAO);

            var validBook = new Book
            {
                Title = "Amintiri din copilarie",
                Author = new List<Author>
                {
                    new Author
                    {
                        FirstName = "Ion",
                        LastName = "Creanga"
                    }
                },
                Domains = new List<BookDomain>
                {
                    new BookDomain
                    {

                    }
                },
                Editions = new List<Edition>()
            };

            try
            {
                bookService.Add(validBook);
                mockBookIDAO.AssertWasCalled(mock => mock.Add(Arg<Book>.Is.Equal(validBook)), options => options.Repeat.Once());
            }
            catch (Exception ex)
            {
                Assert.Fail($"Unexpected exception: {ex.Message}");
            }
        }
    }
}
