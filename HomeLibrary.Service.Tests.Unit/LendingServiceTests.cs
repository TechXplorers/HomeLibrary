using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using HomeLibrary.Common;
using HomeLibrary.Common.Contracts;
using HomeLibrary.Common.Dto;
using HomeLibrary.Repository;
using Moq;
using NUnit.Framework;

namespace HomeLibrary.Service.Tests.Unit
{
    [TestFixture]
    public class LendingServiceTests
    {
        private LendingService sut;
        private ILoanRepository loanRepository;
        private IBookRepository bookRepository;
        private ILendeeRepository lendeeRepository;
        private readonly Book book = new Book { Id = 10, Isbn = "66272", Title = "Test" };
        private readonly Lendee lendee = new Lendee { Id = 100, FirstName = "Test" };
        private readonly Loan currentLoan = new Loan { Id = 1000, BookId = 10, LendeeId = 100, IsReturned = false, LentOn = DateTime.Today };
        
        [SetUp]
        public void SetUp()
        {
            loanRepository   = Mock.Of<ILoanRepository>();
            bookRepository   = Mock.Of<IBookRepository>();
            lendeeRepository = Mock.Of<ILendeeRepository>();

            sut = new LendingService(bookRepository, lendeeRepository, loanRepository);
        }

        [Test]
        public void CheckOut_WhenBookIdIsInvalid_Returns_Error()
        {
            Mock.Get(bookRepository)
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns<Book>(null);

            Mock.Get(lendeeRepository)
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(lendee);

            Mock.Get(loanRepository)
                .Setup(x => x.GetCurrent(It.IsAny<int>()))
                .Returns<Loan>(null);

            var result = sut.CheckOut(book.Id, lendee.Id);

            Assert.True(result != null);
            Assert.True(result.IsError);
        }

        [Test]
        public void CheckOut_WhenLendeeIdIsInvalid_Returns_Error()
        {
            Mock.Get(bookRepository)
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(book);

            Mock.Get(lendeeRepository)
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns<Lendee>(null);

            Mock.Get(loanRepository)
                .Setup(x => x.GetCurrent(It.IsAny<int>()))
                .Returns<Loan>(null);

            var result = sut.CheckOut(book.Id, lendee.Id);

            Assert.True(result != null);
            Assert.True(result.IsError);
        }

        [Test]
        public void CheckOut_WhenTheBookIsAlreadyOnLoan_Returns_Error()
        {
            Mock.Get(bookRepository)
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(book);

            Mock.Get(lendeeRepository)
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(lendee);

            Mock.Get(loanRepository)
                .Setup(x => x.GetCurrent(It.IsAny<int>()))
                .Returns(currentLoan);

            var result = sut.CheckOut(book.Id, lendee.Id);

            Assert.True(result != null);
            Assert.True(result.IsError);
        }

        [Test]
        public void CheckOut_WhenItIsValid_Returns_Success()
        {
            Mock.Get(bookRepository)
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(book);

            Mock.Get(lendeeRepository)
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(lendee);

            Mock.Get(loanRepository)
                .Setup(x => x.GetCurrent(It.IsAny<int>()))
                .Returns<Loan>(null);

            var result = sut.CheckOut(book.Id, lendee.Id);

            Assert.True(result != null);
            Assert.False(result.IsError);
        }

        [Test]
        public void CheckOut_WhenItIsValid_Invokes_LoanRepository_Create()
        {
            Mock.Get(bookRepository)
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(book);

            Mock.Get(lendeeRepository)
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(lendee);

            Mock.Get(loanRepository)
                .Setup(x => x.GetCurrent(It.IsAny<int>()))
                .Returns<Loan>(null);

            Mock.Get(loanRepository)
                .Setup(x => x.Create(It.IsAny<Loan>()))
                .Verifiable();

            sut.CheckOut(book.Id, lendee.Id);
            Expression<Func<Loan, bool>> matchingLoan =
                loan => loan.BookId == book.Id && loan.LendeeId == lendee.Id && loan.IsReturned == false;

            Mock.Get(loanRepository)
                .Verify(x => x.Create(It.Is(matchingLoan)), Times.AtLeastOnce);
        }

        [Test]
        public void CheckIn_WhenBookIdIsInvalid_Returns_Error()
        {
            Mock.Get(bookRepository)
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns<Book>(null);
            
            Mock.Get(loanRepository)
                .Setup(x => x.GetCurrent(It.IsAny<int>()))
                .Returns(currentLoan);

            var result = sut.CheckIn(book.Id);

            Assert.True(result != null);
            Assert.True(result.IsError);
        }

        [Test]
        public void CheckIn_WhenTheBookIsNotOnLoan_Returns_Error()
        {
            Mock.Get(bookRepository)
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(book);

            Mock.Get(loanRepository)
                .Setup(x => x.GetCurrent(It.IsAny<int>()))
                .Returns<Loan>(null);

            var result = sut.CheckIn(book.Id);

            Assert.True(result != null);
            Assert.True(result.IsError);
        }

        [Test]
        public void CheckIn_WhenItIsValid_Returns_Success()
        {
            Mock.Get(bookRepository)
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(book);

            Mock.Get(loanRepository)
                .Setup(x => x.GetCurrent(It.IsAny<int>()))
                .Returns(currentLoan);

            var result = sut.CheckIn(book.Id);

            Assert.True(result != null);
            Assert.False(result.IsError);
        }

        [Test]
        public void CheckIn_WhenItIsValid_Invokes_LoanRepository_Update()
        {
            Mock.Get(bookRepository)
                .Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(book);

            Mock.Get(loanRepository)
                .Setup(x => x.GetCurrent(It.IsAny<int>()))
                .Returns(currentLoan);
            
            Mock.Get(loanRepository)
                .Setup(x => x.Update(It.IsAny<int>(), It.IsAny<Loan>()))
                .Verifiable();

            sut.CheckIn(book.Id);

            Expression<Func<int, bool>> matchingLoanId = loanId => loanId == currentLoan.Id;

            Expression<Func<Loan, bool>> matchingLoan =
                loan => loan.BookId == currentLoan.BookId && loan.LendeeId == currentLoan.LendeeId && loan.IsReturned == true;

            Mock.Get(loanRepository)
                .Verify(x => x.Update(It.Is(matchingLoanId), It.Is(matchingLoan)), Times.AtLeastOnce);
        }
    }
}
