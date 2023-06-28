public class AttachObserverCommand : ICommand<LibraryRequest>
{
    public void Execute(LibraryRequest data)
    {
        var libraryManagment = LibraryManagement.Instance;
        libraryManagment.AddBookObserver(data.UserId, data.BookId);
    }
}
