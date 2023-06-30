public class ReserveBookCommand : ICommand<LibraryRequest>
{
    public Response Execute(LibraryRequest data)
    {
        var libraryManagment = LibraryManagement.Instance;
        return libraryManagment.ReserveBook(data.UserId, data.BookId);
    }
}
