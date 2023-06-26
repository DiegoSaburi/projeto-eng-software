public abstract class User
{
    public int Id { get; protected set; }
    public string Name { get; protected set; } = string.Empty;
    public virtual TimeSpan BorrowCopyTimeLimit { get; }
    public List<Copy> BorrowedCopies { get; protected set; } = new List<Copy>();
    public List<BookReserve> BookReserves { get; protected set; } = new List<BookReserve>();
    public abstract bool CanBorrow();
    
    public void TryBorrowCopy(Copy Copy) 
    {
        if(CanBorrow()) 
        {
            Copy.Borrow(BorrowCopyTimeLimit);
            BorrowedCopies.Add(Copy);
        }
    }

    public void ReturnBook(Copy Copy)
    {
        BorrowedCopies.Remove(Copy);
    }
}
