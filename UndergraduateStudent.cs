public class UndergraduateStudent : User
{
    public UndergraduateStudent(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public override TimeSpan BorrowCopyTime { get; } = TimeSpan.FromDays(3);

    public override bool CanBorrow() =>
        BorrowedCopies.Count < 3
        && BorrowedCopies.All(b => b.BorrowedTime > DateTime.Now.TimeOfDay);
}
