public class BookReserve (Book Book, User User)
{
    public User User { get; init; }
    
    public Book Book { get; init; }
    
    public BookReserve(Book book, User user)
    {
        Book = book;
        User = user;
    }

    public void Method()
    {
        Console.WriteLine("Test");
    }
}
