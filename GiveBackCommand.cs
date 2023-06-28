public class GiveBackCommand : ICommand<LibraryRequest>
{
    public void Execute(LibraryRequest data)
    {
        var libraryManagement = LibraryManagement.Instance;
        libraryManagement.GetBackCopy(data.UserId, data.BookId);        
    }
    
}