using HomeLibrary.Common.Dto;

namespace HomeLibrary.Common.Contracts
{
    public interface ILendeeRepository
    {
        Lendee GetById(int id);
        Lendee[] GetAll();
        Lendee Create(Lendee lendee);
    }
}
