public class GetBookInformationCommand : ICommand<LibraryRequest>
{
    public void Execute(LibraryRequest data)
    {
        var libraryManagment = LibraryManagement.Instance;
        libraryManagment.GetBookInformation(data.BookId);
    }
}
