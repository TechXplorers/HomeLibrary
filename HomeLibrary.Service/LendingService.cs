using System;
using HomeLibrary.Common;
using HomeLibrary.Common.Contracts;
using HomeLibrary.Common.Dto;

namespace HomeLibrary.Service
{
    public class LendingService : ILendingService
    {
        private readonly IBookRepository bookRepository;
        private readonly ILendeeRepository lendeeRepository;
        private readonly ILoanRepository loanRepository;

        public LendingService(IBookRepository bookRepository, ILendeeRepository lendeeRepository, ILoanRepository loanRepository)
        {
            this.bookRepository = bookRepository;
            this.lendeeRepository = lendeeRepository;
            this.loanRepository = loanRepository;
        }

        public Result CheckOut(int bookId, int lendeeId)
        {
            var book = bookRepository.GetById(bookId);
            if (book == null)
                return Result.Error("Invalid bookId passed in");

            var lendee = lendeeRepository.GetById(lendeeId);
            if(lendee == null)
                return Result.Error("Invalid lendeeId passed in");

            var currentLoan = loanRepository.GetCurrent(bookId);
            if (currentLoan != null)
                return Result.Error("This book is already on loan");

            loanRepository.Create(new Loan
            {
                BookId = bookId,
                LendeeId = lendeeId,
                LentOn = DateTime.Today,
                IsReturned = false
            });

            return Result.Success("Check out was successful");
        }

        public Result CheckIn(int bookId)
        {
            var book = bookRepository.GetById(bookId);
            if (book == null)
                return Result.Error("Invalid bookId passed in");

            var currentLoan = loanRepository.GetCurrent(bookId);
            if (currentLoan == null)
                return Result.Error("This book is not on loan");

            currentLoan.IsReturned = true;
            loanRepository.Update(currentLoan.Id, currentLoan);

            return Result.Success("Check in was successful");
        }
    }
}
