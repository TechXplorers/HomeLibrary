using HomeLibrary.Common.Dto;

namespace HomeLibrary.Common.Contracts
{
    public interface ILibraryService
    {
        Result<Book> AddBook(Book book);
        Result<Lendee> AddLendee(Lendee lendee);
        Result<Book[]> GetAllBooks();
        Result<Lendee[]> GetAllLendees();
    }
}
