public enum CopyStatus
{
    Running,
    Finished
}

public class Copy
{
    public int Id { get; init; }
    public Book Book { get; init; }
    public CopyStatus CopyStatus{ get; set; }
    public DateTime BorrowedDate { get; private set; }
    public User? Borrower { get; set; }
    
    public Copy(int id, Book book)
    {
        Id = id;
        Book = book;
        Borrower = null;
    }

    public TimeSpan BorrowedTime { get; private set; }

    public void Borrow(TimeSpan borrowedTime, User borrower)
    {
        Borrower = borrower;
        CopyStatus = CopyStatus.Running;
        BorrowedTime = borrowedTime;
    }
}
