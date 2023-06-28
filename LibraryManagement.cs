public sealed class LibraryManagement
{
    public static List<Book> Books = new List<Book>() 
    {
        new Book(0, "Livro 1", new List<string> { "Autor 1", "Autor 2" }, "edição 1", "2017"),
        new Book(1, "Livro 2", new List<string> { "Autor 1", "Autor 2" }, "edição 1", "2017")
    };

    public static List<Copy> Copies = new List<Copy>() 
    {
        new Copy(0, Books[0]),
        new Copy(1, Books[1])
    };

    public static List<User> Users = new List<User>()
    {
        new Teacher(0, "Professor 1"),
        new UndergraduateStudent(1, "Estudante de graduação 1"),
        new GraduateStudent(2, "Estudante de pós-graduação 1")
    };

    public static List<BookReserve> BookReserves = new List<BookReserve>()
    {
        new BookReserve(Users[0], Books[0]),
        new BookReserve(Users[1], Books[1])
    };

    private static LibraryManagement instance;
    private static readonly object lockObject = new object();
    
    public static LibraryManagement Instance
    {
        get
        {
            if(instance is null)
            {
                lock(lockObject)
                {
                    if(instance == null)
                        instance = new LibraryManagement();
                }
            }
            return instance;
        }

    }

    public void LendCopy(int userId, int bookId)
    {
        var user = Users.First(u => u.Id == userId);
        var copies = Copies.Where(c => c.Book.Id == bookId);
        var copy = user.BorrowStrategy.CanBorrowCopy(user, copies);
        string response = user.TryBorrowCopy(copy);
        if(string.Equals(response, "Empréstimo feito com sucesso"))
        {
            var copyIndex = Copies.IndexOf(Copies.First(cp => cp.Id == copy!.Id));
            Copies[copyIndex] = copy!;
            if(user.BookReserves.Any(br => br.Book.Id == bookId))
            {
                var bookReserve = BookReserves.First(br => br.Book.Id == bookId);
                bookReserve.IsActive = false;
            }
            Console.WriteLine(response);
        }
        else
        {
            Console.WriteLine(response);
        }
    }

    public void ReserveBook(int userId, int bookId)
    {
        var user = Users.First(u => u.Id == userId);
        var book = Books.First(b => b.Id == bookId);
        var bookReserve = new BookReserve(user, book);
        user.ReserveBook(bookReserve);
        BookReserves.Add(bookReserve);
    }
}
