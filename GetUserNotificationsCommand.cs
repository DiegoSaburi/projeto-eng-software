public class GetUserNotificationsCommand : ICommand<LibraryRequest>
{
    public Response Execute(LibraryRequest data)
    {
        var libraryManagment = LibraryManagement.Instance;
        return libraryManagment.HowManyNotifications(data.UserId);
    }
}
