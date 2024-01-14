using DataMapper;
using DomainModel;
using DomainModel.CustomValidationHelpers;
using Library.models;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using Rhino.Mocks.Constraints;
using ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Security;
using static log4net.Appender.RollingFileAppender;

namespace TestServiceLayer.ServicesTests
{
    [TestClass]
    public class BorrowServiceTests
    {
        private IBorrowIDAO mockBorrowIDAO;
        private BorrowService borrowService;
        private Borrow borrow;

        [TestInitialize]
        public void SetUp()
        {
            mockBorrowIDAO = MockRepository.GenerateMock<IBorrowIDAO>();
            borrowService = new BorrowService(mockBorrowIDAO, new TimeSpan(30, 0, 0, 0), 4, 4, 5, new TimeSpan(90, 0, 0, 0), 2, new TimeSpan(5, 0, 0, 0));
            borrow = new Borrow()
            {
                Reader = new Reader
                {
                    Borrows = new List<Borrow>
                    {
                    },
                    Role = false
                    
                },
                Edition = new Edition()
                {
                    EditionId = 1,
                    Book = new Book
                    {
                        BookId =2
                    }
                },
                BorrowStartDate = new DateTime(2003, 12, 4),
                BorrowEndDate = new DateTime(2004, 2, 3),
                IsReturned = false,
                ExtendedBorrows = new List<ExtendedBorrow>()
            };
        }

        [TestMethod]
        public void AddValidBorrowCallsIBorrowIDAO()
        {
            borrowService.Add(this.borrow);
        }

        [TestMethod]
        public void AddInvalidBorrowCallsIBorrowIDAO()
        {
            this.borrow.Reader = null;
            var exception = Assert.ThrowsException<ValidationException>(() => borrowService.Add(this.borrow));
            Assert.AreEqual("Reader is required!", exception.Message);
        }

        [TestMethod]
        public void RemoveBorrowCallsIBorrowIDAO()
        {
            borrowService.Delete(this.borrow);
        }

        [TestMethod]
        public void GetByIdReturnsCorrectBorrow()
        {
            int borrowId = 1;
            mockBorrowIDAO.Stub(x => x.GetById(borrowId)).Return(borrow);

            var result = borrowService.GetById(borrowId);

            Assert.AreEqual(borrow, result);
        }

        [TestMethod]
        public void GetAllBorrowsCallsIBorrowIDAO()
        {
            var expectedBorrows = new List<Borrow>
            {
                new Borrow
                {
                    Reader = new Reader(),
                    Edition = new Edition{Book = new Book {BookId =1 } },
                    BorrowStartDate = new DateTime(2003,12,1),
                    BorrowEndDate = new DateTime(2004,2,3),
                    IsReturned = false,
                    ExtendedBorrows = new List<ExtendedBorrow>()
                },
            };

            mockBorrowIDAO.Stub(x => x.GetAll()).Return(expectedBorrows);

            var result = borrowService.GetAll();

            CollectionAssert.AreEqual(expectedBorrows, result.ToList());
        }


        [TestMethod]
        public void UpdateBorrowCallsIBorrowIDAO()
        {
            borrowService.Update(this.borrow);
        }

        [TestMethod]
        public void CheckBorrowBookCallsIBorrowIDAOCorrect()
        {
            this.borrow.Edition.BorrowableBooks = 50;
            this.borrow.Edition.UnBorrowableBooks = 50;
            borrowService.CheckBorrowBook(this.borrow);
        }

        [TestMethod]
        public void CheckBorrowBookCallsIBorrowIDAONoBorrowable()
        {
            this.borrow.Edition.BorrowableBooks = 0;
            this.borrow.Edition.UnBorrowableBooks = 50;
            var exception = Assert.ThrowsException<ValidationException>(() => borrowService.CheckBorrowBook(this.borrow)).Message;
            Assert.AreEqual(exception, "There are no borrowable books!");
        }

        [TestMethod]
        public void CheckBorrowBookCallsIBorrowIDAOIncorrect()
        {
            this.borrow.Edition.BorrowableBooks = 5;
            this.borrow.Edition.UnBorrowableBooks = 50;
            var exception = Assert.ThrowsException<ValidationException>(() => borrowService.CheckBorrowBook(this.borrow)).Message;
            Assert.AreEqual(exception, "Borrowable books should be at least 10% of total books!");
        }

        [TestMethod]
        public void MaximumBorrowsPerDayCallsIBorrowIDAOCorrect()
        {
            borrowService.MaximumBorrowsPerDay(this.borrow);
        }

        [TestMethod]
        public void MaximumBorrowsPerDayCallsIBorrowIDAOIncorrect()
        {
            this.borrow.BorrowStartDate = new DateTime(2003, 12, 4);
            this.borrow.Reader.Borrows.Add(new Borrow
            {
                BorrowStartDate = new DateTime(2003, 12, 4)
            });
            this.borrow.Reader.Borrows.Add(new Borrow
            {
                BorrowStartDate = new DateTime(2003, 12, 4)
            });
            this.borrow.Reader.Borrows.Add(new Borrow
            {
                BorrowStartDate = new DateTime(2003, 12, 4)
            });

            var exception = Assert.ThrowsException<ValidationException>(() => borrowService.MaximumBorrowsPerDay(this.borrow)).Message;
            Assert.AreEqual(exception, "You cannot borrow more than 2 books per day!");
        }

        [TestMethod]
        public void MaximumBorrowsPerDayCallsIBorrowIDAORoleTrueCorrect()
        {
            this.borrow.Reader.Role = true;
            this.borrow.BorrowStartDate = new DateTime(2003, 12, 4);
            this.borrow.Reader.Borrows.Add(new Borrow
            {
                BorrowStartDate = new DateTime(2003, 12, 4)
            });
            this.borrow.Reader.Borrows.Add(new Borrow
            {
                BorrowStartDate = new DateTime(2003, 12, 4)
            });
            this.borrow.Reader.Borrows.Add(new Borrow
            {
                BorrowStartDate = new DateTime(2003, 12, 4)
            });

            borrowService.MaximumBorrowsPerDay(this.borrow);
        }


        [TestMethod]
        public void MaximumBorrowsPerDayCallsIBorrowIDAORoleTrueIncorrect()
        {
            this.borrow.Reader.Role = true;
            this.borrow.BorrowStartDate = new DateTime(2003, 12, 4);
            this.borrow.Reader.Borrows.Add(new Borrow
            {
                BorrowStartDate = new DateTime(2003, 12, 4)
            });
            this.borrow.Reader.Borrows.Add(new Borrow
            {
                BorrowStartDate = new DateTime(2003, 12, 4)
            });

            this.borrow.Reader.Borrows.Add(new Borrow
            {
                BorrowStartDate = new DateTime(2003, 12, 4)
            });

            borrowService.MaximumBorrowsPerDay(this.borrow);
        }

        [TestMethod]
        public void CannotBorrowSameBookInAPeriodCallsIBorrowIDAOInCorrect()
        {
            this.borrow.BorrowStartDate = new DateTime(2020, 12, 3);
            this.borrow.Edition.EditionId = 1;
            this.borrow.Reader.Borrows.Add(new Borrow
            {
                Edition = new Edition
                {
                    EditionId = 1,
                },
                BorrowStartDate = new DateTime(2020, 12, 2)
            });

            var exception = Assert.ThrowsException<ValidationException>(() => borrowService.CannotBorrowSameBookInAPeriod(this.borrow)).Message;
            Assert.AreEqual(exception, "You cannot borrow same edition in this period!");
        }

        [TestMethod]
        public void CannotBorrowSameBookInAPeriodCallsIBorrowIDAOCorrect()
        {
            this.borrow.BorrowStartDate = new DateTime(2020, 11, 3);
            this.borrow.Edition.EditionId = 1;
            this.borrow.Reader.Borrows.Add(new Borrow
            {
                Edition = new Edition
                {
                    EditionId = 1,
                },
                BorrowStartDate = new DateTime(2020, 12, 2)
            });
            borrowService.CannotBorrowSameBookInAPeriod(this.borrow);
        }

        [TestMethod]
        public void CannotBorrowSameBookInAPeriodCallsIBorrowIDAOCorrectRolePersonal()
        {
            this.borrow.Reader.Role = true;
            this.borrow.BorrowStartDate = new DateTime(2020, 11, 3);
            this.borrow.Edition.EditionId = 1;
            this.borrow.Reader.Borrows.Add(new Borrow
            {
                Edition = new Edition
                {
                    EditionId = 1,
                },
                BorrowStartDate = new DateTime(2020, 12, 2)
            });
            borrowService.CannotBorrowSameBookInAPeriod(this.borrow);
        }

        [TestMethod]
        public void CannotBorrowSameBookInAPeriodCallsIBorrowIDAOIncorrectRolePersonal()
        {
            this.borrow.Reader.Role = true;
            this.borrow.BorrowStartDate = new DateTime(2020, 12, 3);
            this.borrow.Edition.EditionId = 1;
            this.borrow.Reader.Borrows.Add(new Borrow
            {
                Edition = new Edition
                {
                    EditionId = 1,
                },
                BorrowStartDate = new DateTime(2020, 12, 2)
            });
            var exception = Assert.ThrowsException<ValidationException>(() => borrowService.CannotBorrowSameBookInAPeriod(this.borrow)).Message;
            Assert.AreEqual(exception, "You cannot borrow same edition in this period!");
        }

        [TestMethod]
        public void MaximumBooksInASpecificPeriodIncorrect()
        {
            this.borrow.BorrowStartDate = new DateTime(2008, 11, 22);
            this.borrow.Reader.Borrows = new List<Borrow>
            {
                new Borrow
                {
                    BorrowStartDate = new DateTime(2008,11,21),
                },
                new Borrow
                {
                    BorrowStartDate = new DateTime(2008,11,20),
                },
                new Borrow
                {
                    BorrowStartDate = new DateTime(2008,11,19),
                },
                new Borrow
                {
                    BorrowStartDate = new DateTime(2008,11,18),
                },
                new Borrow
                {
                    BorrowStartDate = new DateTime(2008,11,17),
                }
            };
            var exception = Assert.ThrowsException<ValidationException>(() => borrowService.MaximumBooksInSpecificPeriod(this.borrow)).Message;
            Assert.AreEqual(exception, "You cannot borrow more than 4 books in this period!");
        }

        [TestMethod]
        public void MaximumBooksInASpecificPeriodCorrect()
        {
            this.borrow.BorrowStartDate = new DateTime(2008, 11, 22);
            this.borrow.Reader.Borrows = new List<Borrow>
            {
                new Borrow
                {
                    BorrowStartDate = new DateTime(2008,11,21),
                },
                new Borrow
                {
                    BorrowStartDate = new DateTime(2008,11,20),
                },
                new Borrow
                {
                    BorrowStartDate = new DateTime(2008,11,19),
                },

            };
            borrowService.MaximumBooksInSpecificPeriod(this.borrow);
        }

        [TestMethod]
        public void MaximumBooksInASpecificPeriodInCorrectRolePersonal()
        {
            this.borrow.Reader.Role = true;
            this.borrow.BorrowStartDate = new DateTime(2008, 11, 22);
            this.borrow.Reader.Borrows = new List<Borrow>
            {
                new Borrow
                {
                    BorrowStartDate = new DateTime(2008,11,21),
                },
                new Borrow
                {
                    BorrowStartDate = new DateTime(2008,11,20),
                },
                new Borrow
                {
                    BorrowStartDate = new DateTime(2008,11,19),
                },
                new Borrow
                {
                    BorrowStartDate = new DateTime(2008,11,19),
                },
                new Borrow
                {
                    BorrowStartDate = new DateTime(2008,11,19),
                },
                new Borrow
                {
                    BorrowStartDate = new DateTime(2008,11,19),
                },
                new Borrow
                {
                    BorrowStartDate = new DateTime(2008,11,19),
                },
                new Borrow
                {
                    BorrowStartDate = new DateTime(2008,11,19),
                },
                new Borrow
                {
                    BorrowStartDate = new DateTime(2008,11,19),
                },
                new Borrow
                {
                    BorrowStartDate = new DateTime(2008,11,19),
                },

            };
            var exception = Assert.ThrowsException<ValidationException>(() => borrowService.MaximumBooksInSpecificPeriod(this.borrow)).Message;
            Assert.AreEqual(exception, "You cannot borrow more than 8 books in this period!");
        }

        [TestMethod]
        public void ValidateBorrowListThrowsExceptionWhenConditionsNotMet()
        {

            var borrow1 = new Borrow { Reader = new Reader { Role = false},Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { DomainName = "Informatica", BookDomainId = 1 } } } } };
            var borrow2 = new Borrow { Reader = new Reader{ Role = false }, Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { DomainName = "Informatica", BookDomainId = 1 } } } } };
            var borrow3 = new Borrow { Reader = new Reader{ Role = false }, Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { DomainName = "Informatica", BookDomainId = 1 } } } } };
            var borrowList = new List<Borrow> { borrow1, borrow2, borrow3 };



            var exception = Assert.ThrowsException<ValidationException>(() => borrowService.ValidateBorrowList(borrowList));
            Assert.AreEqual("You should borrow at least two different books in two different domains!", exception.Message);
        }

        [TestMethod]
        public void ValidateBorrowListTooManyBorrows()
        {
            var borrow1 = new Borrow { Reader = new Reader { Role = false }, Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1 } } } } };
            var borrow2 = new Borrow { Reader = new Reader { Role = false }, Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 2 } } } } };
            var borrow3 = new Borrow { Reader = new Reader { Role = false }, Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 4 } } } } };
            var borrow4 = new Borrow { Reader = new Reader { Role = false }, Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 5 } } } } };
            var borrow5 = new Borrow { Reader = new Reader { Role = false }, Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 7 } } } } };
            var borrow6 = new Borrow { Reader = new Reader { Role = false }, Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 8 } } } } };
            var borrowList = new List<Borrow> { borrow1, borrow2, borrow3, borrow4, borrow5, borrow6 };
            var exception = Assert.ThrowsException<ValidationException>(() => borrowService.ValidateBorrowList(borrowList));
            Assert.AreEqual("Your limit to borrow books has exceeded!", exception.Message);
        }

        [TestMethod]
        public void ValidateBorrowListTooManyBorrowsRolePersonal()
        {
            var borrow1 = new Borrow { Reader = new Reader { Role = true }, Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1 } } } } };
            var borrow2 = new Borrow { Reader = new Reader { Role = true }, Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 2 } } } } };
            var borrow3 = new Borrow { Reader = new Reader { Role = true }, Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 4 } } } } };
            var borrow4 = new Borrow { Reader = new Reader { Role = true }, Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 5 } } } } };
            var borrow5 = new Borrow { Reader = new Reader { Role = true }, Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 7 } } } } };
            var borrow6 = new Borrow { Reader = new Reader { Role = true }, Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 8 } } } } };
            var borrow7 = new Borrow { Reader = new Reader { Role = true }, Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 11 } } } } };
            var borrow8 = new Borrow { Reader = new Reader { Role = true }, Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 12 } } } } };
            var borrow9 = new Borrow { Reader = new Reader { Role = true }, Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 13 } } } } };
            var borrow10 = new Borrow { Reader = new Reader { Role = true }, Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 14 } } } } };
            var borrow11 = new Borrow { Reader = new Reader { Role = true }, Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 15 } } } } };
            var borrowList = new List<Borrow> { borrow1, borrow2, borrow3, borrow4, borrow5, borrow6, borrow7, borrow8, borrow9, borrow10, borrow11 };
            var exception = Assert.ThrowsException<ValidationException>(() => borrowService.ValidateBorrowList(borrowList));
            Assert.AreEqual("Your limit to borrow books has exceeded!", exception.Message);
        }

        [TestMethod]
        public void ValidateBorrowListValidList()
        {

            var borrow1 = new Borrow { Reader = new Reader { Role = false }, Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1 } } } } };
            var borrow2 = new Borrow { Reader = new Reader { Role = false }, Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 2 } } } } };
            var borrow3 = new Borrow { Reader = new Reader { Role = false }, Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 4 } } } } };
            var borrowList = new List<Borrow> { borrow1, borrow2, borrow3 };

            borrowService.ValidateBorrowList(borrowList);

        }

        [TestMethod]
        public void ValidateBorrowListValidListRolePersonal()
        {
            var borrow1 = new Borrow { Reader = new Reader { Role = true }, Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1 } } } } };
            var borrow2 = new Borrow { Reader = new Reader { Role = true }, Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 2 } } } } };
            var borrow3 = new Borrow { Reader = new Reader { Role = true }, Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 4 } } } } };
            var borrowList = new List<Borrow> { borrow1, borrow2, borrow3 };

            borrowService.ValidateBorrowList(borrowList);
        }

        [TestMethod]
        public void ValidateBorrowListSizeLowerThanTwoCorrect()
        {

            var borrow1 = new Borrow { Reader = new Reader { Role = false }, Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1 } } } } };
            var borrowList = new List<Borrow> { borrow1 };

            borrowService.ValidateBorrowList(borrowList);

        }

        [TestMethod]
        public void CreateBorrowListCorrect()
        {
            DateTime dateTime = new DateTime(2011, 10, 10);
            var borrow1 = new Borrow
            {
                Reader = new Reader { },
                Edition = new Edition { BorrowableBooks = 50, UnBorrowableBooks = 50 },
                BorrowStartDate = dateTime,
                BorrowEndDate = dateTime.AddDays(7),
                IsReturned = false
            };
            var borrowList = new List<Borrow> { borrow1 };
            borrowService.CreateBorrowList(borrowList);
        }

        [TestMethod]
        public void CreateBorrowListIncorrect()
        {
            DateTime dateTime = new DateTime(2011, 10, 10);
            var borrow1 = new Borrow
            {
                Reader = new Reader { },
                Edition = new Edition { BorrowableBooks = 50, UnBorrowableBooks = 50 },
                BorrowStartDate = dateTime,
                BorrowEndDate = dateTime.AddDays(-3),
                IsReturned = false
            };
            var borrowList = new List<Borrow> { borrow1 };
            var exception = Assert.ThrowsException<ValidationException>(() => borrowService.CreateBorrowList(borrowList));
            Assert.AreEqual("End date must be later than start date.", exception.Message);
        }



        [TestMethod]
        public void MaximumBooksInSameDomainInvalid()
        {
            DateTime dateTime = new DateTime(2020, 10, 28);
            this.borrow = new Borrow
            {
                Reader = new Reader
                {
                    Borrows = new List<Borrow>
                    {
                        new Borrow
                        {
                            BorrowStartDate = dateTime.AddDays(-30),
                            Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1} } } }
                        },
                        new Borrow
                        {
                            BorrowStartDate = dateTime.AddDays(-30),
                            Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1} } } }
                        },
                        new Borrow
                        {
                            BorrowStartDate = dateTime.AddDays(-30),
                            Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1} } } }
                        },
                        new Borrow
                        {
                            BorrowStartDate = dateTime.AddDays(-30),
                            Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1} } } }
                        },
                        new Borrow
                        {
                            BorrowStartDate = dateTime.AddDays(-30),
                            Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1} } } }
                        },
                        new Borrow
                        {
                            BorrowStartDate = dateTime.AddDays(-30),
                            Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1} } } }
                        },
                    }
                },
                BorrowStartDate = dateTime,
                Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1 } } } }
            };
            var exception = Assert.ThrowsException<ValidationException>(() => borrowService.MaximumBooksInTheSameDomain(this.borrow));
            Assert.AreEqual("You borrowed a book with same domain too much!", exception.Message);
        }

        [TestMethod]
        public void MaximumBooksInSameDomainValid()
        {
            DateTime dateTime = new DateTime(2020, 10, 28);
            this.borrow = new Borrow
            {
                Reader = new Reader
                {
                    Borrows = new List<Borrow>
                    {
                        new Borrow
                        {
                            BorrowStartDate = dateTime.AddDays(-30),
                            Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1} } } }
                        },
                        new Borrow
                        {
                            BorrowStartDate = dateTime.AddDays(-30),
                            Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1} } } }
                        },
                        new Borrow
                        {
                            BorrowStartDate = dateTime.AddDays(-30),
                            Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1} } } }
                        },
                    }
                },
                BorrowStartDate = dateTime,
                Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1 } } } }
            };
            borrowService.MaximumBooksInTheSameDomain(this.borrow);
        }

        [TestMethod]
        public void MaximumBooksInSameDomainValidRolePersonal()
        {
            DateTime dateTime = new DateTime(2020, 10, 28);
            this.borrow = new Borrow
            {
                Reader = new Reader
                {
                    Role = true,
                    Borrows = new List<Borrow>
                    {
                        new Borrow
                        {
                            BorrowStartDate = dateTime.AddDays(-30),
                            Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1} } } }
                        },
                        new Borrow
                        {
                            BorrowStartDate = dateTime.AddDays(-30),
                            Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1} } } }
                        },
                        new Borrow
                        {
                            BorrowStartDate = dateTime.AddDays(-30),
                            Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1} } } }
                        },
                        new Borrow
                        {
                            BorrowStartDate = dateTime.AddDays(-30),
                            Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1} } } }
                        },
                        new Borrow
                        {
                            BorrowStartDate = dateTime.AddDays(-30),
                            Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1} } } }
                        },
                        new Borrow
                        {
                            BorrowStartDate = dateTime.AddDays(-30),
                            Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1} } } }
                        },
                        new Borrow
                        {
                            BorrowStartDate = dateTime.AddDays(-30),
                            Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1} } } }
                        },
                        new Borrow
                        {
                            BorrowStartDate = dateTime.AddDays(-30),
                            Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1} } } }
                        },
                        new Borrow
                        {
                            BorrowStartDate = dateTime.AddDays(-30),
                            Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1} } } }
                        },
                        new Borrow
                        {
                            BorrowStartDate = dateTime.AddDays(-30),
                            Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1} } } }
                        },
                        new Borrow
                        {
                            BorrowStartDate = dateTime.AddDays(-30),
                            Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1} } } }
                        },
                        new Borrow
                        {
                            BorrowStartDate = dateTime.AddDays(-30),
                            Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1} } } }
                        }
                    }
                },

                BorrowStartDate = dateTime,
                Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1 } } } }
            };
            var exception = Assert.ThrowsException<ValidationException>(() => borrowService.MaximumBooksInTheSameDomain(this.borrow));
            Assert.AreEqual("You borrowed a book with same domain too much!", exception.Message);
        }

        [TestMethod]
        public void MaximumBooksInSameDomainValidWithParents()
        {
            DateTime dateTime = new DateTime(2020, 10, 28);
            this.borrow = new Borrow
            {
                Reader = new Reader
                {
                    Borrows = new List<Borrow>
                    {
                        new Borrow
                        {
                            BorrowStartDate = dateTime.AddDays(-30),
                            Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 3, ParentDomain = new BookDomain{BookDomainId = 1} } } } }
                        },
                        new Borrow
                        {
                            BorrowStartDate = dateTime.AddDays(-30),
                            Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 5, ParentDomain = new BookDomain { BookDomainId = 1 } } } } }
                        },
                        new Borrow
                        {
                            BorrowStartDate = dateTime.AddDays(-30),
                            Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1} } } }
                        },
                    }
                },
                BorrowStartDate = dateTime,
                Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1 } } } }
            };
            borrowService.MaximumBooksInTheSameDomain(this.borrow);
        }
    }
}
