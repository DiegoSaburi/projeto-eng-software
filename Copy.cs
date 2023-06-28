public class Copy
{
    public int Id { get; init; }
    public Book Book { get; init; }
    public bool IsBorrowed{ get; set; }
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
