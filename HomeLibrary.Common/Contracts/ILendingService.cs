namespace HomeLibrary.Common.Contracts
{
    public interface ILendingService
    {
        Result CheckOut(int bookId, int lendeeId);
        Result CheckIn(int bookId);
    }
}
