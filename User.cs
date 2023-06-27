public abstract class User
{
    public int Id { get; protected set; }
    public string Name { get; protected set; } = string.Empty;
    public virtual TimeSpan BorrowCopyTimeLimit { get; }
    public List<Copy> BorrowedCopies { get; protected set; } = new List<Copy>();
    public List<BookReserve> BookReserves { get; protected set; } = new List<BookReserve>();
    public virtual IBorrowStrategy BorrowStrategy { get; } 
    
    public string TryBorrowCopy(Copy? copy)
    {
        if(copy is not null)
        {
            copy.Borrow(BorrowCopyTimeLimit);
            BorrowedCopies.Add(copy);
            return "Empréstimo feito com sucesso";
        }
        return "Não foi possível concluir o empréstimo";
    }

    public void ReturnBook(Copy Copy)
    {
        BorrowedCopies.Remove(Copy);
    }
}
