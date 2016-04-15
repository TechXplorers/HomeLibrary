using HomeLibrary.Common;
using HomeLibrary.Common.Contracts;
using HomeLibrary.Common.Dto;

namespace HomeLibrary.Service
{
    public class LibraryService : ILibraryService
    {
        private readonly IBookRepository bookRepository;
        private readonly ILendeeRepository lendeeRepository;

        public LibraryService(IBookRepository bookRepository, ILendeeRepository lendeeRepository)
        {
            this.bookRepository = bookRepository;
            this.lendeeRepository = lendeeRepository;
        }

        public Result<Book> AddBook(Book book)
        {
            //validate(book)
            var newBook = bookRepository.Create(book);
            return Result.Success(newBook);
        }

        public Result<Lendee> AddLendee(Lendee lendee)
        {
            var newLendee = lendeeRepository.Create(lendee);
            return Result.Success(newLendee);
        }

        public Result<Book[]> GetAllBooks()
        {
            var books = bookRepository.GetAll();
            return Result.Success(books);
        }

        public Result<Lendee[]> GetAllLendees()
        {
            var lendees = lendeeRepository.GetAll();
            return Result.Success(lendees);
        }
    }
}
