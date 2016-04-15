using HomeLibrary.Common.Dto;

namespace HomeLibrary.Common.Contracts
{
    public interface ILoanRepository
    {
        Loan GetCurrent(int bookId);
        Loan Create(Loan loan);
        Loan Update(int id, Loan loan);
    }
}
