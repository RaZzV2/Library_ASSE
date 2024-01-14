using DataMapper;
using DomainModel;
using DomainModel.CustomValidationHelpers;
using Library.models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using ServiceLayer.IServices;
using ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using static log4net.Appender.RollingFileAppender;

namespace TestServiceLayer.ServicesTests
{
    [TestClass]
    public class ExtendedBorrowServiceTests
    {
        private IExtendedBorrowIDAO mockExtendedBorrowIDAO;
        private ExtendedBorrowService extendedBorrowService;
        private ExtendedBorrow extendedBorrow;

        [TestInitialize]
        public void SetUp()
        {
            var dateTime = new DateTime(2022, 1, 1, 12, 0, 0);
            mockExtendedBorrowIDAO = MockRepository.GenerateMock<IExtendedBorrowIDAO>();
            extendedBorrowService = new ExtendedBorrowService(mockExtendedBorrowIDAO,2);
            this.extendedBorrow = new ExtendedBorrow()
            {
                Id = 1,

                Borrow = new Borrow
                {
                    Reader = new Reader { },
                    Edition = new Edition { },
                    BorrowStartDate = dateTime,
                    BorrowEndDate = dateTime.AddDays(7),
                    IsReturned = false
                },
                Date = new DateTime(2023, 1, 1, 12, 0, 0)
            };
        }

        [TestMethod]
        public void AddValidExtendedBorrowCallsIExtendedBorrowIDAO()
        {
            extendedBorrowService.Add(this.extendedBorrow);
            mockExtendedBorrowIDAO.AssertWasCalled(mock => mock.Add(Arg<ExtendedBorrow>.Is.Equal(this.extendedBorrow)), options => options.Repeat.Once());
        }

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
                        IsReturned = false
                    },
                    Date = new DateTime(2023, 1, 1, 12, 0, 0)
                },
        
            };

            mockExtendedBorrowIDAO.Stub(x => x.GetAll()).Return(expectedExtendedBorrows);

            var result = extendedBorrowService.GetAll();

            mockExtendedBorrowIDAO.AssertWasCalled(mock => mock.GetAll(), options => options.Repeat.Once());
            CollectionAssert.AreEqual(expectedExtendedBorrows, result.ToList());
        }

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
                                        Date = new DateTime(2022,10,8)
                                    },
                                    new ExtendedBorrow
                                    {
                                        Date = new DateTime(2022,10,7)
                                    },
                                    new ExtendedBorrow
                                    {
                                        Date = new DateTime(2022,10,5)
                                    },
                                    new ExtendedBorrow
                                    {
                                        Date = new DateTime(2022,10,3)
                                    }
                                }
                            }
                        }
                    }
                },
                Date = new DateTime(2022,10,20)
            };

            
            var exception = Assert.ThrowsException<ValidationException>(() => extendedBorrowService.MaximumExtension(extendedBorrow));
            Assert.AreEqual("You can extend your borrow with maximum 2 in this period!", exception.Message);
        }

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
                                        Date = new DateTime(2022,10,8)
                                    },
                                }
                            }
                        }
                    }
                },
                Date = new DateTime(2022, 10, 20)
            };
            extendedBorrowService.MaximumExtension(extendedBorrow);
        }

        [TestMethod]
        public void AddInvalidExtendedBorrowCallsIExtendedBorrowIDAO()
        {
            this.extendedBorrow = new ExtendedBorrow
            {
                Borrow = null,
                Date = new DateTime(2022, 1, 5)
            };
            var exception = Assert.ThrowsException<ValidationException>(() => extendedBorrowService.Add(this.extendedBorrow));
            Assert.AreEqual("A borrow is required!", exception.Message);
        }

        [TestMethod]
        public void GetById_ReturnsCorrectExtendedBorrow()
        {
            int extendedBorrowId = 1;

            mockExtendedBorrowIDAO.Stub(x => x.GetById(Arg<int>.Is.Anything)).Return(this.extendedBorrow);

            var result = extendedBorrowService.GetById(extendedBorrowId);

            Assert.IsNotNull(result);
            Assert.AreEqual(this.extendedBorrow, result);
        }

        [TestMethod]
        public void RemoveExtendedBorrowCallsIExtendedBorrowIDAO()
        {
            extendedBorrowService.Delete(this.extendedBorrow);
            mockExtendedBorrowIDAO.AssertWasCalled(mock => mock.Delete(Arg<ExtendedBorrow>.Is.Equal(this.extendedBorrow)), options => options.Repeat.Once());
        }

        [TestMethod]
        public void UpdateExtendedBorrowCallsIExtendedBorrowIDAO()
        {
            extendedBorrowService.Update(this.extendedBorrow);
            mockExtendedBorrowIDAO.AssertWasCalled(mock => mock.Update(Arg<ExtendedBorrow>.Is.Equal(this.extendedBorrow)), options => options.Repeat.Once());
        }
    }
}
