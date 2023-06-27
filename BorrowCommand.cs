public class BorrowCommand : ICommand<LibraryRequest>
{
    public void Execute(LibraryRequest data)
    {
        var libraryManagement = LibraryManagement.Instance;
        libraryManagement.LendCopy(data.UserId, data.BookId);
    }
}
