public class GetUserNotificationsCommand : ICommand<LibraryRequest>
{
    public void Execute(LibraryRequest data)
    {
        var libraryManagment = LibraryManagement.Instance;
        libraryManagment.HowManyNotifications(data.UserId);
    }
}
