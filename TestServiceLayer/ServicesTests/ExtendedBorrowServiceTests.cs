// <copyright file="ExtendedBorrowServiceTests.cs" company="Transilvania University of Brasov">
// Dragomir Razvan
// </copyright>

namespace TestServiceLayer.ServicesTests
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using DataMapper;
    using DomainModel;
    using Library.Models;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Rhino.Mocks;
    using ServiceLayer.Services;

    /// <summary>
    /// Unit tests for the ExtendedBorrowService class.
    /// </summary>
    [TestClass]
    public class ExtendedBorrowServiceTests
    {
        /// <summary>
        /// Represents the mock object for the IExtendedBorrowIDAO interface.
        /// </summary>
        private IExtendedBorrowIDAO mockExtendedBorrowIDAO;

        /// <summary>
        /// Represents the service responsible for handling ExtendedBorrow entities.
        /// </summary>
        private ExtendedBorrowService extendedBorrowService;

        /// <summary>
        /// Represents an instance of the ExtendedBorrow entity.
        /// </summary>
        private ExtendedBorrow extendedBorrow;

        /// <summary>
        /// Initializes necessary objects before each test.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            var dateTime = new DateTime(2022, 1, 1, 12, 0, 0);
            this.mockExtendedBorrowIDAO = MockRepository.GenerateMock<IExtendedBorrowIDAO>();
            this.extendedBorrowService = new ExtendedBorrowService(this.mockExtendedBorrowIDAO, 2);
            this.extendedBorrow = new ExtendedBorrow()
            {
                Id = 1,

                Borrow = new Borrow
                {
                    Reader = new Reader { },
                    Edition = new Edition { },
                    BorrowStartDate = dateTime,
                    BorrowEndDate = dateTime.AddDays(7),
                    IsReturned = false,
                },
                Date = new DateTime(2023, 1, 1, 12, 0, 0),
            };
        }

        /// <summary>
        /// Unit test for the ExtendedBorrowService's Add method, ensuring it calls IExtendedBorrowIDAO with a valid extended borrow.
        /// </summary>
        [TestMethod]
        public void AddValidExtendedBorrowCallsIExtendedBorrowIDAO()
        {
            this.extendedBorrowService.Add(this.extendedBorrow);
            this.mockExtendedBorrowIDAO.AssertWasCalled(mock => mock.Add(Arg<ExtendedBorrow>.Is.Equal(this.extendedBorrow)), options => options.Repeat.Once());
        }

        /// <summary>
        /// Unit test for the ExtendedBorrowService's GetAll method, validating that it calls IExtendedBorrowIDAO and returns the expected extended borrows.
        /// </summary>
        [TestMethod]
        public void GetAllExtendedBorrowsCallsIExtendedBorrowIDAO()
        {
            var expectedExtendedBorrows = new List<ExtendedBorrow>
            {
                new ExtendedBorrow
                {
                    Borrow = new Borrow
                    {
                        Reader = new Reader { },
                        Edition = new Edition { },
                        BorrowStartDate = new DateTime(2022, 1, 1, 12, 0, 0),
                        BorrowEndDate = new DateTime(2022, 1, 8, 12, 0, 0),
                        IsReturned = false,
                    },
                    Date = new DateTime(2023, 1, 1, 12, 0, 0),
                },
            };

            this.mockExtendedBorrowIDAO.Stub(x => x.GetAll()).Return(expectedExtendedBorrows);

            var result = this.extendedBorrowService.GetAll();

            this.mockExtendedBorrowIDAO.AssertWasCalled(mock => mock.GetAll(), options => options.Repeat.Once());
            CollectionAssert.AreEqual(expectedExtendedBorrows, result.ToList());
        }

        /// <summary>
        /// Unit test for the ExtendedBorrowService's MaximumExtension method, ensuring it throws an exception when conditions are not met.
        /// </summary>
        [TestMethod]
        public void MaximumExtension_ThrowsExceptionWhenConditionsNotMet()
        {
            var extendedBorrow = new ExtendedBorrow
            {
                Borrow = new Borrow
                {
                    Reader = new Reader
                    {
                        Borrows = new List<Borrow>
                        {
                            new Borrow
                            {
                                ExtendedBorrows = new List<ExtendedBorrow>
                                {
                                    new ExtendedBorrow
                                    {
                                        Date = new DateTime(2022, 10, 8),
                                    },
                                    new ExtendedBorrow
                                    {
                                        Date = new DateTime(2022, 10, 7),
                                    },
                                    new ExtendedBorrow
                                    {
                                        Date = new DateTime(2022, 10, 5),
                                    },
                                    new ExtendedBorrow
                                    {
                                        Date = new DateTime(2022, 10, 3),
                                    },
                                },
                            },
                        },
                    },
                },
                Date = new DateTime(2022, 10, 20),
            };
            var exception = Assert.ThrowsException<ValidationException>(() => this.extendedBorrowService.MaximumExtension(extendedBorrow));
            Assert.AreEqual("You can extend your borrow with maximum 2 in this period!", exception.Message);
        }

        /// <summary>
        /// Unit test for the ExtendedBorrowService's MaximumExtension method, verifying no exception is thrown when the extension is successful.
        /// </summary>
        [TestMethod]
        public void MaximumExtension_NoThrowsExceptionWhenSuccess()
        {
            var extendedBorrow = new ExtendedBorrow
            {
                Borrow = new Borrow
                {
                    Reader = new Reader
                    {
                        Borrows = new List<Borrow>
                        {
                            new Borrow
                            {
                                ExtendedBorrows = new List<ExtendedBorrow>
                                {
                                    new ExtendedBorrow
                                    {
                                        Date = new DateTime(2022, 10, 8),
                                    },
                                },
                            },
                        },
                    },
                },
                Date = new DateTime(2022, 10, 20),
            };
            this.extendedBorrowService.MaximumExtension(extendedBorrow);
        }

        /// <summary>
        /// Unit test for the ExtendedBorrowService's Add method, verifying that it throws a ValidationException for an invalid extended borrow.
        /// </summary>
        [TestMethod]
        public void AddInvalidExtendedBorrowCallsIExtendedBorrowIDAO()
        {
            this.extendedBorrow = new ExtendedBorrow
            {
                Borrow = null,
                Date = new DateTime(2022, 1, 5),
            };
            var exception = Assert.ThrowsException<ValidationException>(() => this.extendedBorrowService.Add(this.extendedBorrow));
            Assert.AreEqual("A borrow is required!", exception.Message);
        }

        /// <summary>
        /// Unit test for the ExtendedBorrowService's GetById method, ensuring it returns the correct extended borrow based on the provided ID.
        /// </summary>
        [TestMethod]
        public void GetById_ReturnsCorrectExtendedBorrow()
        {
            int extendedBorrowId = 1;

            this.mockExtendedBorrowIDAO.Stub(x => x.GetById(Arg<int>.Is.Anything)).Return(this.extendedBorrow);

            var result = this.extendedBorrowService.GetById(extendedBorrowId);

            Assert.IsNotNull(result);
            Assert.AreEqual(this.extendedBorrow, result);
        }

        /// <summary>
        /// Unit test for the ExtendedBorrowService's Delete method, confirming it calls IExtendedBorrowIDAO to remove an extended borrow.
        /// </summary>
        [TestMethod]
        public void RemoveExtendedBorrowCallsIExtendedBorrowIDAO()
        {
            this.extendedBorrowService.Delete(this.extendedBorrow);
            this.mockExtendedBorrowIDAO.AssertWasCalled(mock => mock.Delete(Arg<ExtendedBorrow>.Is.Equal(this.extendedBorrow)), options => options.Repeat.Once());
        }

        /// <summary>
        /// Unit test for the ExtendedBorrowService's Update method, verifying that it calls IExtendedBorrowIDAO to update an extended borrow.
        /// </summary>
        [TestMethod]
        public void UpdateExtendedBorrowCallsIExtendedBorrowIDAO()
        {
            this.extendedBorrowService.Update(this.extendedBorrow);
            this.mockExtendedBorrowIDAO.AssertWasCalled(mock => mock.Update(Arg<ExtendedBorrow>.Is.Equal(this.extendedBorrow)), options => options.Repeat.Once());
        }
    }
}
