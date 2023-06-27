public class Copy
{
    public int Id { get; init; }
    public Book Book { get; init; }
    public bool IsBorrowed 
    { 
        get => BorrowedTime == null ? false 
            : BorrowedTime > DateTime.Now.TimeOfDay; 
    }
    public DateTime BorrowedDate { get; private set; }
    
    public Copy(int id, Book book)
    {
        Id = id;
        Book = book;
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
