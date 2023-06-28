public abstract class User
{
    public int Id { get; protected set; }
    
    public string Name { get; protected set; } = string.Empty;
    
    public virtual TimeSpan BorrowCopyTimeLimit { get; }
    
    public List<Copy> BorrowedCopies { get; protected set; } = new List<Copy>();
    
    public List<BookReserve> BookReserves { get; protected set; } = new List<BookReserve>();
    
    protected virtual bool CanReserve { get => BookReserves.Count()< 3; }
    
    public virtual IBorrowStrategy BorrowStrategy { get; } 
    
    public string TryBorrowCopy(Copy? copy)
    {
        if(copy is not null)
        {
            copy.Borrow(BorrowCopyTimeLimit, this);
            BorrowedCopies.Add(copy);
            return "Empréstimo feito com sucesso";
        }
        return "Não foi possível concluir o empréstimo";
    }

    public virtual string ReserveBook(BookReserve bookReserve)
    {
        if(CanReserve)
        {
            BookReserves.Add(bookReserve);
            return $"Livro {bookReserve.Book.Title} reservado para usuário {Name} com sucesso";
        }
        return $"Já existem mais de três reservas para o usuário {Name}";
    }

    public (bool,int) ReturnCopy(int bookId)
    {
        var copyGot = BorrowedCopies.FirstOrDefault(c => c.Book.Id == bookId);
        if (copyGot is not null)
        {
            copyGot.CopyStatus = CopyStatus.Finished;
            return (true,copyGot.Id);
        }
        return (false, 0);
    }
}
