public interface IObserver
{
    void Update(ISubject subject);
    int HowManyNotifications { get; }
}
