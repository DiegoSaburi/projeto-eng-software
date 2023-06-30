public class BorrowCommand : ICommand<LibraryRequest>
{
    public Response Execute(LibraryRequest data)
    {
        var libraryManagement = LibraryManagement.Instance;
        return libraryManagement.LendCopy(data.UserId, data.BookId);
    }
}
