public class UndergraduateStudent : User
{
    public UndergraduateStudent(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public override TimeSpan BorrowCopyTimeLimit { get; } = TimeSpan.FromDays(3);
}
