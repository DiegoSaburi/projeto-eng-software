public class Teacher : User
{
    public Teacher(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public override TimeSpan BorrowCopyTime { get; } = TimeSpan.FromDays(7);
    
    public override bool CanBorrow() =>
        BorrowedCopies.All(b => b.BorrowedTime > DateTime.Now.TimeOfDay);
}
