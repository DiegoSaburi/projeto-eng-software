public class GraduateStudent : User
{
    public GraduateStudent(int id, string name)
    {
        Id = id;
        Name = name;
    }
    
    public override TimeSpan BorrowCopyTime { get; } = TimeSpan.FromDays(4);

    public override bool CanBorrow() =>
        BorrowedCopies.Count < 4 
        && BorrowedCopies.All(b => b.BorrowedTime > DateTime.Now.TimeOfDay);
}
