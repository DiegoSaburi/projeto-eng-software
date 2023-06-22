public abstract class User
{
    public int Id { get; protected set; }
    public string Name { get; protected set; } = string.Empty;
    public virtual TimeSpan BorrowCopyTime { get; }
    public List<Copy> BorrowedCopies { get; protected set; } = new List<Copy>();
    public abstract bool CanBorrow();
    
    public void BorrowCopy(Copy Copy) 
    {
        if(CanBorrow()) 
        {
            Copy.Borrow(BorrowCopyTime);
            BorrowedCopies.Add(Copy);
        }
    }

    public void ReturnBook(Copy Copy)
    {
        BorrowedCopies.Remove(Copy);
    }
}
