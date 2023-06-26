public class BorrowCommand : ICommand<LibraryRequest>
{
    public void Execute(LibraryRequest data)
    {
        var libraryManagment = LibraryManagement.GetInstance();
        libraryManagment.LendCopy(data.UserId, data.BookId);
    }
}
