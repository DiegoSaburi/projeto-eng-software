public class AttachObserverCommand : ICommand<LibraryRequest>
{
    public Response Execute(LibraryRequest data)
    {
        var libraryManagment = LibraryManagement.Instance;
        return libraryManagment.AddBookObserver(data.UserId, data.BookId);
    }
}
