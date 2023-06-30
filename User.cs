public abstract class User
{
    public int Id { get; protected set; }
    
    public string Name { get; protected set; } = string.Empty;
    
    public virtual TimeSpan BorrowCopyTimeLimit { get; }
    
    public List<Copy> BorrowedCopies { get; protected set; } = new List<Copy>();
    
    public List<BookReserve> BookReserves { get; protected set; } = new List<BookReserve>();
    
    private bool CanReserve { get => BookReserves.Count() < 3; }
    
    public virtual IBorrowStrategy BorrowStrategy { get; } = new UndergraduateStudentBorrowStrategy();
    
    public void BorrowCopy(Copy copy)
    {
        copy.Borrow(BorrowCopyTimeLimit, this);
        FinishReservation(copy.Book.Id);
        BorrowedCopies.Add(copy);
    }

    private void FinishReservation(int bookId)
    {
        var bookReservation = BookReserves.FirstOrDefault(br => br.Book.Id == bookId);
        if(bookReservation is not null)
            bookReservation.FinishReservation();
    }

    public string ReserveBook(BookReserve bookReserve)
    {
        if(CanReserve)
        {
            bookReserve.Reserve();
            BookReserves.Add(bookReserve);
            return $"Livro {bookReserve.Book.Title}, reservado para usuário {Name} com sucesso";
        }
        return $"Já existem mais de três reservas para o usuário {Name}";
    }

    public (bool, int) ReturnCopy(int bookId)
    {
        var borroweredCopy = BorrowedCopies.FirstOrDefault(c => c.Book.Id == bookId);
        if (borroweredCopy is not null)
        {
            borroweredCopy.CopyStatus = CopyStatus.Finished;
            return (true, borroweredCopy.Id);
        }
        return (false, 0);
    }
}
