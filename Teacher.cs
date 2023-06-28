public class Teacher : User, IObserver
{
    public Teacher(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public override TimeSpan BorrowCopyTimeLimit { get; } = TimeSpan.FromDays(7);
    
    public int HowManyNotifications { get; private set; }

    public void Update(ISubject subject)
    {
        HowManyNotifications++;
    }
}
