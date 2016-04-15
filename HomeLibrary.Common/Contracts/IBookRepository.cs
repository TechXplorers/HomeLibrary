using HomeLibrary.Common.Dto;

namespace HomeLibrary.Common.Contracts
{
    public interface IBookRepository
    {
        Book GetById(int id);
        Book[] GetAll();
        Book Create(Book book);
    }
}
