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
    /// BorrowServiceTests - This class contains unit tests for the BorrowService, responsible for managing borrowing operations.
    /// </summary>
    [TestClass]
    public class BorrowServiceTests
    {
        private IBorrowIDAO mockBorrowIDAO;
        private BorrowService borrowService;
        private Borrow borrow;

        /// <summary>
        /// Set up the test environment by creating mock objects and initializing test data.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            this.mockBorrowIDAO = MockRepository.GenerateMock<IBorrowIDAO>();
            this.borrowService = new BorrowService(this.mockBorrowIDAO, new TimeSpan(30, 0, 0, 0), 4, 4, 5, new TimeSpan(90, 0, 0, 0), 2, new TimeSpan(5, 0, 0, 0));
            this.borrow = new Borrow()
            {
                Reader = new Reader
                {
                    Borrows = new List<Borrow>
                    {
                    },
                    Role = false,
                },
                Edition = new Edition()
                {
                    EditionId = 1,
                    Book = new Book
                    {
                        BookId = 2,
                    },
                },
                BorrowStartDate = new DateTime(2003, 12, 4),
                BorrowEndDate = new DateTime(2004, 2, 3),
                IsReturned = false,
                ExtendedBorrows = new List<ExtendedBorrow>(),
            };
        }

        /// <summary>
        /// Test the <see cref="BorrowService.Add"/> method with a valid Borrow, verifying that it calls the <see cref="IBorrowIDAO"/> interface.
        /// </summary>
        [TestMethod]
        public void AddValidBorrowCallsIBorrowIDAO()
        {
            this.borrowService.Add(this.borrow);
        }

        /// <summary>
        /// Test the <see cref="BorrowService.Add"/> method with an invalid Borrow, ensuring that it throws a <see cref="ValidationException"/>.
        /// </summary>
        [TestMethod]
        public void AddInvalidBorrowCallsIBorrowIDAO()
        {
            this.borrow.Reader = null;
            var exception = Assert.ThrowsException<ValidationException>(() => this.borrowService.Add(this.borrow));
            Assert.AreEqual("Reader is required!", exception.Message);
        }

        /// <summary>
        /// Test the <see cref="BorrowService.Delete"/> method to ensure it calls the <see cref="IBorrowIDAO"/> interface when deleting a Borrow.
        /// </summary>
        [TestMethod]
        public void RemoveBorrowCallsIBorrowIDAO()
        {
            this.borrowService.Delete(this.borrow);
        }

        /// <summary>
        /// Test the <see cref="BorrowService.GetById"/> method to ensure it returns the correct Borrow based on the provided ID.
        /// </summary>
        [TestMethod]
        public void GetByIdReturnsCorrectBorrow()
        {
            int borrowId = 1;
            this.mockBorrowIDAO.Stub(x => x.GetById(borrowId)).Return(this.borrow);

            var result = this.borrowService.GetById(borrowId);

            Assert.AreEqual(this.borrow, result);
        }

        /// <summary>
        /// Test the <see cref="BorrowService.GetAll"/> method to ensure it calls the <see cref="IBorrowIDAO"/> interface and returns the expected list of Borrows.
        /// </summary>
        [TestMethod]
        public void GetAllBorrowsCallsIBorrowIDAO()
        {
            var expectedBorrows = new List<Borrow>
            {
                new Borrow
                {
                    Reader = new Reader(),
                    Edition = new Edition { Book = new Book { BookId = 1 } },
                    BorrowStartDate = new DateTime(2003, 12, 1),
                    BorrowEndDate = new DateTime(2004, 2, 3),
                    IsReturned = false,
                    ExtendedBorrows = new List<ExtendedBorrow>(),
                },
            };

            this.mockBorrowIDAO.Stub(x => x.GetAll()).Return(expectedBorrows);

            var result = this.borrowService.GetAll();

            CollectionAssert.AreEqual(expectedBorrows, result.ToList());
        }

        /// <summary>
        /// Test the <see cref="BorrowService.Update"/> method to ensure it calls the <see cref="IBorrowIDAO"/> interface when updating a Borrow.
        /// </summary>
        [TestMethod]
        public void UpdateBorrowCallsIBorrowIDAO()
        {
            this.borrowService.Update(this.borrow);
        }

        /// <summary>
        /// Ensures that CheckBorrowBook correctly calls IBorrowIDAO when there are borrowable books.
        /// </summary>
        [TestMethod]
        public void CheckBorrowBookCallsIBorrowIDAOCorrect()
        {
            this.borrow.Edition.BorrowableBooks = 50;
            this.borrow.Edition.UnBorrowableBooks = 50;
            this.borrowService.CheckBorrowBook(this.borrow);
        }

        /// <summary>
        /// Ensures that CheckBorrowBook correctly throws a ValidationException when there are no borrowable books.
        /// </summary>
        [TestMethod]
        public void CheckBorrowBookCallsIBorrowIDAONoBorrowable()
        {
            this.borrow.Edition.BorrowableBooks = 0;
            this.borrow.Edition.UnBorrowableBooks = 50;
            var exception = Assert.ThrowsException<ValidationException>(() => this.borrowService.CheckBorrowBook(this.borrow)).Message;
            Assert.AreEqual(exception, "There are no borrowable books!");
        }

        /// <summary>
        /// Ensures that CheckBorrowBook correctly throws a ValidationException when the percentage of borrowable books is insufficient.
        /// </summary>
        [TestMethod]
        public void CheckBorrowBookCallsIBorrowIDAOIncorrect()
        {
            this.borrow.Edition.BorrowableBooks = 5;
            this.borrow.Edition.UnBorrowableBooks = 50;
            var exception = Assert.ThrowsException<ValidationException>(() => this.borrowService.CheckBorrowBook(this.borrow)).Message;
            Assert.AreEqual(exception, "Borrowable books should be at least 10% of total books!");
        }

        /// <summary>
        /// Ensures that MaximumBorrowsPerDay correctly calls IBorrowIDAO when borrowing limits are not exceeded.
        /// </summary>
        [TestMethod]
        public void MaximumBorrowsPerDayCallsIBorrowIDAOCorrect()
        {
            this.borrowService.MaximumBorrowsPerDay(this.borrow);
        }

        /// <summary>
        /// Ensures that MaximumBorrowsPerDay correctly throws a ValidationException when the borrower exceeds the daily borrowing limit.
        /// </summary>
        [TestMethod]
        public void MaximumBorrowsPerDayCallsIBorrowIDAOIncorrect()
        {
            this.borrow.BorrowStartDate = new DateTime(2003, 12, 4);
            this.borrow.Reader.Borrows.Add(new Borrow
            {
                BorrowStartDate = new DateTime(2003, 12, 4),
            });
            this.borrow.Reader.Borrows.Add(new Borrow
            {
                BorrowStartDate = new DateTime(2003, 12, 4),
            });
            this.borrow.Reader.Borrows.Add(new Borrow
            {
                BorrowStartDate = new DateTime(2003, 12, 4),
            });

            var exception = Assert.ThrowsException<ValidationException>(() => this.borrowService.MaximumBorrowsPerDay(this.borrow)).Message;
            Assert.AreEqual(exception, "You cannot borrow more than 2 books per day!");
        }

        /// <summary>
        /// Ensures that MaximumBorrowsPerDay correctly calls IBorrowIDAO when the borrower with a role exceeds the daily borrowing limit.
        /// </summary>
        [TestMethod]
        public void MaximumBorrowsPerDayCallsIBorrowIDAORoleTrueCorrect()
        {
            this.borrow.Reader.Role = true;
            this.borrow.BorrowStartDate = new DateTime(2003, 12, 4);
            this.borrow.Reader.Borrows.Add(new Borrow
            {
                BorrowStartDate = new DateTime(2003, 12, 4),
            });
            this.borrow.Reader.Borrows.Add(new Borrow
            {
                BorrowStartDate = new DateTime(2003, 12, 4),
            });
            this.borrow.Reader.Borrows.Add(new Borrow
            {
                BorrowStartDate = new DateTime(2003, 12, 4),
            });

            this.borrowService.MaximumBorrowsPerDay(this.borrow);
        }

        /// <summary>
        /// Ensures that MaximumBorrowsPerDay correctly throws a ValidationException when the borrower with a role exceeds the daily borrowing limit.
        /// </summary>
        [TestMethod]
        public void MaximumBorrowsPerDayCallsIBorrowIDAORoleTrueIncorrect()
        {
            this.borrow.Reader.Role = true;
            this.borrow.BorrowStartDate = new DateTime(2003, 12, 4);
            this.borrow.Reader.Borrows.Add(new Borrow
            {
                BorrowStartDate = new DateTime(2003, 12, 4),
            });
            this.borrow.Reader.Borrows.Add(new Borrow
            {
                BorrowStartDate = new DateTime(2003, 12, 4),
            });

            this.borrow.Reader.Borrows.Add(new Borrow
            {
                BorrowStartDate = new DateTime(2003, 12, 4),
            });

            this.borrowService.MaximumBorrowsPerDay(this.borrow);
        }

        /// <summary>
        /// Ensures that CannotBorrowSameBookInAPeriod correctly throws a ValidationException when attempting to borrow the same edition in a given period.
        /// </summary>
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
                BorrowStartDate = new DateTime(2020, 12, 2),
            });

            var exception = Assert.ThrowsException<ValidationException>(() => this.borrowService.CannotBorrowSameBookInAPeriod(this.borrow)).Message;
            Assert.AreEqual(exception, "You cannot borrow same edition in this period!");
        }

        /// <summary>
        /// Ensures that CannotBorrowSameBookInAPeriod correctly allows borrowing different editions in a given period.
        /// </summary>
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
                BorrowStartDate = new DateTime(2020, 12, 2),
            });
            this.borrowService.CannotBorrowSameBookInAPeriod(this.borrow);
        }

        /// <summary>
        /// Ensures that CannotBorrowSameBookInAPeriod correctly throws a ValidationException when a user with a personal role attempts to borrow the same edition in a given period.
        /// </summary>
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
                BorrowStartDate = new DateTime(2020, 12, 2),
            });
            this.borrowService.CannotBorrowSameBookInAPeriod(this.borrow);
        }

        /// <summary>
        /// Unit tests for the BorrowService's CannotBorrowSameBookInAPeriod method with role-based restrictions.
        /// </summary>
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
                BorrowStartDate = new DateTime(2020, 12, 2),
            });
            var exception = Assert.ThrowsException<ValidationException>(() => this.borrowService.CannotBorrowSameBookInAPeriod(this.borrow)).Message;
            Assert.AreEqual(exception, "You cannot borrow same edition in this period!");
        }

        /// <summary>
        /// Unit tests for the BorrowService's MaximumBooksInASpecificPeriod method with incorrect borrow count in a specific period.
        /// </summary>
        [TestMethod]
        public void MaximumBooksInASpecificPeriodIncorrect()
        {
            this.borrow.BorrowStartDate = new DateTime(2008, 11, 22);
            this.borrow.Reader.Borrows = new List<Borrow>
            {
                new Borrow
                {
                    BorrowStartDate = new DateTime(2008, 11, 21),
                },
                new Borrow
                {
                    BorrowStartDate = new DateTime(2008, 11, 20),
                },
                new Borrow
                {
                    BorrowStartDate = new DateTime(2008, 11, 19),
                },
                new Borrow
                {
                    BorrowStartDate = new DateTime(2008, 11, 18),
                },
                new Borrow
                {
                    BorrowStartDate = new DateTime(2008, 11, 17),
                },
            };
            var exception = Assert.ThrowsException<ValidationException>(() => this.borrowService.MaximumBooksInSpecificPeriod(this.borrow)).Message;
            Assert.AreEqual(exception, "You cannot borrow more than 4 books in this period!");
        }

        /// <summary>
        /// Unit tests for the BorrowService's MaximumBooksInASpecificPeriod method with correct borrow count in a specific period.
        /// </summary>
        [TestMethod]
        public void MaximumBooksInASpecificPeriodCorrect()
        {
            this.borrow.BorrowStartDate = new DateTime(2008, 11, 22);
            this.borrow.Reader.Borrows = new List<Borrow>
            {
                new Borrow
                {
                    BorrowStartDate = new DateTime(2008, 11, 21),
                },
                new Borrow
                {
                    BorrowStartDate = new DateTime(2008, 11, 20),
                },
                new Borrow
                {
                    BorrowStartDate = new DateTime(2008, 11, 19),
                },
            };
            this.borrowService.MaximumBooksInSpecificPeriod(this.borrow);
        }

        /// <summary>
        /// Unit tests for the BorrowService's MaximumBooksInASpecificPeriod method with incorrect borrow count in a specific period for users with a personal role.
        /// </summary>
        [TestMethod]
        public void MaximumBooksInASpecificPeriodInCorrectRolePersonal()
        {
            this.borrow.Reader.Role = true;
            this.borrow.BorrowStartDate = new DateTime(2008, 11, 22);
            this.borrow.Reader.Borrows = new List<Borrow>
            {
                new Borrow
                {
                    BorrowStartDate = new DateTime(2008, 11, 21),
                },
                new Borrow
                {
                    BorrowStartDate = new DateTime(2008, 11, 20),
                },
                new Borrow
                {
                    BorrowStartDate = new DateTime(2008, 11, 19),
                },
                new Borrow
                {
                    BorrowStartDate = new DateTime(2008, 11, 19),
                },
                new Borrow
                {
                    BorrowStartDate = new DateTime(2008, 11, 19),
                },
                new Borrow
                {
                    BorrowStartDate = new DateTime(2008, 11, 19),
                },
                new Borrow
                {
                    BorrowStartDate = new DateTime(2008, 11, 19),
                },
                new Borrow
                {
                    BorrowStartDate = new DateTime(2008, 11, 19),
                },
                new Borrow
                {
                    BorrowStartDate = new DateTime(2008, 11, 19),
                },
                new Borrow
                {
                    BorrowStartDate = new DateTime(2008, 11, 19),
                },

            };
            var exception = Assert.ThrowsException<ValidationException>(() => this.borrowService.MaximumBooksInSpecificPeriod(this.borrow)).Message;
            Assert.AreEqual(exception, "You cannot borrow more than 8 books in this period!");
        }

        /// <summary>
        /// Unit tests for the BorrowService's ValidateBorrowList method with insufficient variety in borrowed books.
        /// </summary>
        [TestMethod]
        public void ValidateBorrowListThrowsExceptionWhenConditionsNotMet()
        {
            var borrow1 = new Borrow { Reader = new Reader { Role = false }, Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { DomainName = "Informatica", BookDomainId = 1 } } } } };
            var borrow2 = new Borrow { Reader = new Reader { Role = false }, Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { DomainName = "Informatica", BookDomainId = 1 } } } } };
            var borrow3 = new Borrow { Reader = new Reader { Role = false }, Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { DomainName = "Informatica", BookDomainId = 1 } } } } };
            var borrowList = new List<Borrow> { borrow1, borrow2, borrow3 };

            var exception = Assert.ThrowsException<ValidationException>(() => this.borrowService.ValidateBorrowList(borrowList));
            Assert.AreEqual("You should borrow at least two different books in two different domains!", exception.Message);
        }

        /// <summary>
        /// Unit tests for the BorrowService's ValidateBorrowList method with excessive borrows, exceeding the allowed limit.
        /// </summary>
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
            var exception = Assert.ThrowsException<ValidationException>(() => this.borrowService.ValidateBorrowList(borrowList));
            Assert.AreEqual("Your limit to borrow books has exceeded!", exception.Message);
        }

        /// <summary>
        /// Unit tests for the BorrowService's ValidateBorrowList method with excessive borrows, exceeding the allowed limit, for users with a personal role.
        /// </summary>
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
            var exception = Assert.ThrowsException<ValidationException>(() => this.borrowService.ValidateBorrowList(borrowList));
            Assert.AreEqual("Your limit to borrow books has exceeded!", exception.Message);
        }

        /// <summary>
        /// Unit test for the BorrowService's ValidateBorrowList method with a valid list of borrows.
        /// </summary>
        [TestMethod]
        public void ValidateBorrowListValidList()
        {
            var borrow1 = new Borrow { Reader = new Reader { Role = false }, Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1 } } } } };
            var borrow2 = new Borrow { Reader = new Reader { Role = false }, Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 2 } } } } };
            var borrow3 = new Borrow { Reader = new Reader { Role = false }, Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 4 } } } } };
            var borrowList = new List<Borrow> { borrow1, borrow2, borrow3 };

            this.borrowService.ValidateBorrowList(borrowList);
        }

        /// <summary>
        /// Unit test for the BorrowService's ValidateBorrowList method with a valid list of borrows for users with a personal role.
        /// </summary>
        [TestMethod]
        public void ValidateBorrowListValidListRolePersonal()
        {
            var borrow1 = new Borrow { Reader = new Reader { Role = true }, Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1 } } } } };
            var borrow2 = new Borrow { Reader = new Reader { Role = true }, Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 2 } } } } };
            var borrow3 = new Borrow { Reader = new Reader { Role = true }, Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 4 } } } } };
            var borrowList = new List<Borrow> { borrow1, borrow2, borrow3 };

            this.borrowService.ValidateBorrowList(borrowList);
        }

        /// <summary>
        /// Unit test for the BorrowService's ValidateBorrowList method with a list size lower than two (correct scenario).
        /// </summary>
        [TestMethod]
        public void ValidateBorrowListSizeLowerThanTwoCorrect()
        {
            var borrow1 = new Borrow { Reader = new Reader { Role = false }, Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1 } } } } };
            var borrowList = new List<Borrow> { borrow1 };

            this.borrowService.ValidateBorrowList(borrowList);
        }

        /// <summary>
        /// Unit test for the BorrowService's CreateBorrowList method with correct borrow details.
        /// </summary>
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
                IsReturned = false,
            };
            var borrowList = new List<Borrow> { borrow1 };
            this.borrowService.CreateBorrowList(borrowList);
        }

        /// <summary>
        /// Unit test for the BorrowService's CreateBorrowList method with incorrect borrow details.
        /// </summary>
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
                IsReturned = false,
            };
            var borrowList = new List<Borrow> { borrow1 };
            var exception = Assert.ThrowsException<ValidationException>(() => this.borrowService.CreateBorrowList(borrowList));
            Assert.AreEqual("End date must be later than start date.", exception.Message);
        }

        /// <summary>
        /// Unit test for the BorrowService's MaximumBooksInSameDomain method with an invalid number of borrows in the same domain.
        /// </summary>
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
                            Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1 } } } },
                        },
                        new Borrow
                        {
                            BorrowStartDate = dateTime.AddDays(-30),
                            Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1 } } } },
                        },
                        new Borrow
                        {
                            BorrowStartDate = dateTime.AddDays(-30),
                            Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1 } } } },
                        },
                        new Borrow
                        {
                            BorrowStartDate = dateTime.AddDays(-30),
                            Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1 } } } },
                        },
                        new Borrow
                        {
                            BorrowStartDate = dateTime.AddDays(-30),
                            Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1 } } } },
                        },
                        new Borrow
                        {
                            BorrowStartDate = dateTime.AddDays(-30),
                            Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1 } } } },
                        },
                    },
                },
                BorrowStartDate = dateTime,
                Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1 } } } },
            };
            var exception = Assert.ThrowsException<ValidationException>(() => this.borrowService.MaximumBooksInTheSameDomain(this.borrow));
            Assert.AreEqual("You borrowed a book with same domain too much!", exception.Message);
        }

        /// <summary>
        /// Unit test for the BorrowService's MaximumBooksInSameDomain method with a valid number of borrows in the same domain.
        /// </summary>
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
                            Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1 } } } },
                        },
                        new Borrow
                        {
                            BorrowStartDate = dateTime.AddDays(-30),
                            Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1 } } } },
                        },
                        new Borrow
                        {
                            BorrowStartDate = dateTime.AddDays(-30),
                            Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1 } } } },
                        },
                    },
                },
                BorrowStartDate = dateTime,
                Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1 } } } },
            };
            this.borrowService.MaximumBooksInTheSameDomain(this.borrow);
        }

        /// <summary>
        /// Unit test for the BorrowService's MaximumBooksInSameDomainValidRolePersonal method with a valid number of borrows in the same domain for users with a personal role.
        /// </summary>
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
                            Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1 } } } },
                        },
                        new Borrow
                        {
                            BorrowStartDate = dateTime.AddDays(-30),
                            Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1 } } } },
                        },
                        new Borrow
                        {
                            BorrowStartDate = dateTime.AddDays(-30),
                            Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1 } } } },
                        },
                        new Borrow
                        {
                            BorrowStartDate = dateTime.AddDays(-30),
                            Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1 } } } },
                        },
                        new Borrow
                        {
                            BorrowStartDate = dateTime.AddDays(-30),
                            Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1 } } } },
                        },
                        new Borrow
                        {
                            BorrowStartDate = dateTime.AddDays(-30),
                            Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1 } } } },
                        },
                        new Borrow
                        {
                            BorrowStartDate = dateTime.AddDays(-30),
                            Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1 } } } },
                        },
                        new Borrow
                        {
                            BorrowStartDate = dateTime.AddDays(-30),
                            Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1 } } } },
                        },
                        new Borrow
                        {
                            BorrowStartDate = dateTime.AddDays(-30),
                            Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1 } } } },
                        },
                        new Borrow
                        {
                            BorrowStartDate = dateTime.AddDays(-30),
                            Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1 } } } },
                        },
                        new Borrow
                        {
                            BorrowStartDate = dateTime.AddDays(-30),
                            Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1 } } } },
                        },
                        new Borrow
                        {
                            BorrowStartDate = dateTime.AddDays(-30),
                            Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1 } } } },
                        },
                    },
                },

                BorrowStartDate = dateTime,
                Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1 } } } },
            };
            var exception = Assert.ThrowsException<ValidationException>(() => this.borrowService.MaximumBooksInTheSameDomain(this.borrow));
            Assert.AreEqual("You borrowed a book with same domain too much!", exception.Message);
        }

        /// <summary>
        /// Unit test for the BorrowService's MaximumBooksInSameDomainValidWithParents method with a valid number of borrows in the same domain with parent domains.
        /// </summary>
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
                            Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 3, ParentDomain = new BookDomain { BookDomainId = 1 } } } } },
                        },
                        new Borrow
                        {
                            BorrowStartDate = dateTime.AddDays(-30),
                            Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 5, ParentDomain = new BookDomain { BookDomainId = 1 } } } } },
                        },
                        new Borrow
                        {
                            BorrowStartDate = dateTime.AddDays(-30),
                            Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1 } } } },
                        },
                    },
                },
                BorrowStartDate = dateTime,
                Edition = new Edition { Book = new Book { Domains = new List<BookDomain> { new BookDomain { BookDomainId = 1 } } } },
            };
            this.borrowService.MaximumBooksInTheSameDomain(this.borrow);
        }
    }
}
