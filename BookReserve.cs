public class BookReserve
{
    public int Id { get; init; }

    public User User { get; init; }
    
    public Book Book { get; init; }

    public bool IsActive { get; private set; }

    public DateTime ReservationDate { get; private set; }
    
    public BookReserve(int id, User user, Book book, bool isActive = true)
    {
        Id = id;
        Book = book;
        User = user;
        IsActive = isActive;
    }

    public void Reserve()
    {
        ReservationDate = DateTime.Now;
        IsActive = true;
    }
    
    public void FinishReservation() =>
        IsActive = false;
}
