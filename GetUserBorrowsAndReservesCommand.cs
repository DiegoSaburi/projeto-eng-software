public class GetUserBorrowsAndReservesCommand : ICommand<LibraryRequest>
{
    public Response Execute(LibraryRequest data)
    {
        var libraryManagement = LibraryManagement.Instance;
        return libraryManagement.GetUserBorrowsAndReserves(data.UserId);
    }
}
