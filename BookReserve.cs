public class BookReserve
{
    public User User { get; init; }
    
    public Book Book { get; init; }

    public bool IsActive { get; set; }
    
    public BookReserve(User user, Book book)
    {
        Book = book;
        User = user;
    }
}
