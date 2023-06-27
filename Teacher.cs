public class Teacher : User
{
    public Teacher(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public override TimeSpan BorrowCopyTimeLimit { get; } = TimeSpan.FromDays(7);
}
