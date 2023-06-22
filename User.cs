public abstract class User
{
    public int Id { get; protected set; }
    public string Name { get; protected set; } = string.Empty;
    public virtual TimeSpan BorrowCopieTime { get; }
    public List<Copie> BorrowedCopies { get; protected set; } = new List<Copie>();
    public abstract bool CanBorrow();
    
    public void BorrowCopie(Copie copie) 
    {
        if(CanBorrow()) 
        {
            copie.Borrow(BorrowCopieTime);
            BorrowedCopies.Add(copie);
        }
    }

    public void ReturnBook(Copie copie)
    {
        BorrowedCopies.Remove(copie);
    }
}
