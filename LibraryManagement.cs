public sealed class LibraryManagement
{

    public LibraryManagement()
    {
        Books.ForEach(b => 
        {
            b.Reservations = BookReserves.Where(br => br.Book.Id == b.Id).ToList();
        });
        Books.ForEach(b => 
        {
            b.Copies = Copies.Where(br => br.Book.Id == b.Id).ToList();
        });
    }

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
        new BookReserve(0, Users[0], Books[0]),
        new BookReserve(1, Users[1], Books[1])
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
        if(copy is not null)
        {
            var newCopy = Copies.First(cp => cp.Id == copy!.Id);
            newCopy.Borrow(copy!.BorrowedTime);
            if(user.BookReserves.Any(br => br.Book.Id == bookId))
            {
                var bookReserve = BookReserves.First(br => br.Book.Id == bookId);
                bookReserve.IsActive = false;
            }
        }
        Console.WriteLine(response);
    
    }

    public void AddBookObserver(int userId, int bookId)
    {
        var book = Books.First(b => b.Id == bookId);
        book.Attach((IObserver)Users.First(u => u.Id == userId));
    }

    public void HowManyNotifications(int userId)
    {
        var observer = (IObserver)Users.First(u => u.Id == userId);
        Console.WriteLine($"Usuário id:[{userId}] foi notificado [{observer.HowManyNotifications}] vezes sobre os livros que observa");
    }

    public void GetUserBorrowsAndReserves(int userId)
    {
        var user = Users.First(u => u.Id == userId);
        Console.WriteLine($"Livros reservados pela(o) usuária(o) {user.Name}:");
        user.BookReserves.ForEach(br => 
            {
                Console
                .WriteLine(
                    $"Título: {br.Book.Title} Data da reserva: {br.ReservationDate.ToString()}"
                );
            }
        );
        Console.WriteLine($"Livros emprestados a(o) usuária(o) {user.Name}");
        user.BorrowedCopies.ForEach(bc => 
            {
                Console
                .WriteLine(
                    $"Título: {bc.Book.Title}" +
                    $"Data de devolução do empréstimo: {bc.BorrowedDate.ToString()}" +
                    $"Status do empréstimo: {bc.CopyStatus}"
                );
            }
        );
    }

    public void GetBookInformation(int bookId)
    {
        var book = Books.First(b => b.Id == bookId);
        var reservationsCount = book.Reservations.Count;
        Console.WriteLine($"Título do livro: {book.Title} tem {reservationsCount} reservas");
        if(reservationsCount > 0)
        {
            book.Reservations.ForEach(br => 
                {
                    Console.WriteLine($"Reserva {br.Id} reservada por {br.User.Name}");
                });
        }
        book.Copies.ForEach(cp => 
            {
                if(cp.Borrower is not null)
                    Console.WriteLine($"Exemplar [{cp.Id}] com status {cp.CopyStatus} atualmente em posse da(o) usuária(o) {cp.Borrower.Name}");
                else
                    Console.WriteLine($"Exemplar [{cp.Id}] com status {cp.CopyStatus}");
            });
    }

    public void GetBackCopy(int userId, int bookId)
    {
        var user = Users.First(u => u.Id == userId);
        var (returned,returnedCopyId)= user.ReturnCopy(bookId);
        if(returned)
        {            
            var originalCopy = Copies.First(c => c.Id == returnedCopyId);
            originalCopy.CopyStatus = CopyStatus.Finished;

            Console.WriteLine($"usuário {user.Name} devolveu o livro {originalCopy.Book.Title} com sucesso");
        }
        //TODO: printar linha de erro ao tentar retornar o livro
    }

    public void ReserveBook(int userId, int bookId)
    {
        var user = Users.First(u => u.Id == userId);
        var book = Books.First(b => b.Id == bookId);
        var lastId = BookReserves.Last().Id;
        var bookReserve = new BookReserve(lastId + 1,user, book);
        user.ReserveBook(bookReserve);
        BookReserves.Add(bookReserve);
    }
}
