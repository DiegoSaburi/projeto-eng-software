public class GetUserBorrowsAndReservesCommand : ICommand<LibraryRequest>
{
    public void Execute(LibraryRequest data)
    {
        var libraryManagement = LibraryManagement.Instance;
        libraryManagement.GetUserBorrowsAndReserves(data.UserId);
    }
}
