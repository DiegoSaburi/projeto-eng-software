public class ReserveBookCommand : ICommand<LibraryRequest>
{
    public void Execute(LibraryRequest data)
    {
        var libraryManagment = LibraryManagement.Instance;
        libraryManagment.ReserveBook(data.UserId, data.BookId);
    }
}
