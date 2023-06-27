public class GraduateStudent : User
{
    public GraduateStudent(int id, string name)
    {
        Id = id;
        Name = name;
    }
    
    public override TimeSpan BorrowCopyTimeLimit { get; } = TimeSpan.FromDays(4);

    public override IBorrowStrategy BorrowStrategy { get; } = new GraduateStudentBorrowStrategy();
}
