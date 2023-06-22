public class Copy
{
    public int Id { get; init; }
    public int BookId { get; init; }
    public bool Copiestatus { get => BorrowedTime > DateTime.Now.TimeOfDay; }
    
    public Copy(int id, int bookId)
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
