public class Copie
{
    public int Id { get; init; }
    public int BookId { get; init; }
    public bool CopieStatus { get => BorrowedTime > DateTime.Now.TimeOfDay; }
    
    public Copie(int id, int bookId)
    {
        Id = id;
        BookId = bookId;
    }

    public TimeSpan BorrowedTime { get; private set; }

    public bool Borrow(TimeSpan borrowedTime) 
    {
        if(BorrowedTime >= DateTime.Now.TimeOfDay)
        {
            BorrowedTime = borrowedTime;
            return true;
        }
        return false;
    }
}
