public class BookReserve
{
    public User User { get; init; }
    
    public Book Book { get; init; }

    public bool IsActive { get; set; }

    public DateTime ReservationDate { get; set; }
    
    public BookReserve(User user, Book book, bool isActive = true)
    {
        Book = book;
        User = user;
        IsActive = isActive;
    }
}
