public class GiveBackCommand : ICommand<LibraryRequest>
{
    public Response Execute(LibraryRequest data)
    {
        var libraryManagement = LibraryManagement.Instance;
        return libraryManagement.GetBackCopy(data.UserId, data.BookId);        
    }
    
}
