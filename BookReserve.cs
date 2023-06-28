public class BookReserve
{
    public int Id { get; init; }

    public User User { get; init; }
    
    public Book Book { get; init; }

    public bool IsActive { get; set; }

    public DateTime ReservationDate { get; set; }
    
    public BookReserve(int id, User user, Book book, bool isActive = true)
    {
        Id = id;
        Book = book;
        User = user;
        IsActive = isActive;
    }
}
