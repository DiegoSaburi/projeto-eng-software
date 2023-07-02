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
    public DateTime GiveBackDate { get; private set; }
    public User? Borrower { get; set; }
    
    public Copy(int id, Book book)
    {
        Id = id;
        Book = book;
        Borrower = null;
        CopyStatus = CopyStatus.Finished;
    }

    public TimeSpan BorrowedTime { get; private set; }

    public void Borrow(TimeSpan borrowedTime, User borrower)
    {
        BorrowedDate = DateTime.Now;
        Borrower = borrower;
        CopyStatus = CopyStatus.Running;
        BorrowedTime = borrowedTime;
        GiveBackDate = BorrowedDate + borrowedTime;
    }

    public void GiveBackCopy(){
        CopyStatus = CopyStatus.Finished;
        Borrower = null;
        GiveBackDate = DateTime.Now;
    }
}
