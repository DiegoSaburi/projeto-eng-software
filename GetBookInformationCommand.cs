public class GetBookInformationCommand : ICommand<LibraryRequest>
{
    public Response Execute(LibraryRequest data)
    {
        var libraryManagment = LibraryManagement.Instance;
        return libraryManagment.GetBookInformation(data.BookId);
    }
}
