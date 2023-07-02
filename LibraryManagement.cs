using System.Text;

public sealed class LibraryManagement
{

    public LibraryManagement()
    {
        Books.ForEach(b => 
        {
            b.Copies = Copies.Where(cp => cp.Book.Id == b.Id).ToList();
        });
    }

    public static List<Book> Books = new List<Book>() 
    {
        new Book(100, "Engenharia de Software", new List<string> { "Ian Sommervile"},"AddisonWesley", "6ª", "2000"),
        new Book(101, "UML - Guia do Usuário", new List<string> { "Grady Booch", "JamesRumbaugh", "IvarJacobson"},"Campus ", "7ª", "2000"),
        new Book(200, "Code Complete", new List<string> { "Ian Sommervile"},"MicrosoftPress", "2ª", "2014"),
        new Book(201, "Agile Software Development, Principles, Patterns, and Practices", new List<string> { "Robert Martin "},"Prentice Hall", "1ª", "2002"),
        new Book(300, "Refactoring: Improving the Design of Existing Code", new List<string> { "Martin Fowler"},"AddisonWesley Professional", "1ª", "1999"),
        new Book(301, "Software Metrics: A Rigorous and Practical Approach", new List<string> { "Norman Fenton", "James Bieman"},"CRC Press", "3ª", "2014"),
        new Book(400, "Design Patterns: Elements of Reusable Object-Oriented Software", new List<string> { "Erich Gamma", "Richard Helm", "Ralph Johnson", "John Vlissides"},"AddisonWesley Professional", "1ª", "1994"),
        new Book(401, "UML Distilled: A Brief Guide to the Standard Object Modeling Language", new List<string> { "Martin Fowler"},"AddisonWesley Professional", "3ª", "2003")
    };

    public static List<Copy> Copies = new List<Copy>() 
    {
        new Copy(1, Books.First(b => b.Id == 100)),
        new Copy(2, Books.First(b => b.Id == 100)),
        new Copy(3, Books.First(b => b.Id == 101)),
        new Copy(4, Books.First(b => b.Id == 200)),
        new Copy(5, Books.First(b => b.Id == 201)),
        new Copy(6, Books.First(b => b.Id == 300)),
        new Copy(7, Books.First(b => b.Id == 300)),
        new Copy(8, Books.First(b => b.Id == 400)),
        new Copy(9, Books.First(b => b.Id == 400))
    };

    public static List<User> Users = new List<User>()
    {
        new Teacher(100, "Carlos Lucena"),
        new UndergraduateStudent(123, "João da Silva"),
        new UndergraduateStudent(789, "Pedro Paulo"),
        new GraduateStudent(456, "Luiz Fernando Rodrigues")
    };

    public static List<BookReserve> BookReserves = new List<BookReserve>() { };

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

    public Response LendCopy(int userId, int bookId)
    {
        var user = Users.First(u => u.Id == userId);
        var copies = Copies.Where(c => c.Book.Id == bookId);
        
        if(copies is null)
            return new CopyResponse(null, "Não existem exemplares este livro", null);

        CopyResponse response = user.BorrowStrategy.CanBorrowCopy(user, copies);
        if(!response.HasError)
        {
            var copy = response.Copy!;
            user.BorrowCopy(copy);
            var copyToBorrow = Copies.First(cp => cp.Id == copy.Id);
            copyToBorrow.Borrow(copy!.BorrowedTime, user);
            if(user.BookReserves.Any(br => br.Book.Id == bookId))
            {
                var bookReserve = BookReserves.First(br => br.Book.Id == bookId);
                bookReserve.FinishReservation();
            }
        }
        return response;
    }

    public Response AddBookObserver(int userId, int bookId)
    {
        var book = Books.First(b => b.Id == bookId);
        book.Attach((IObserver)Users.First(u => u.Id == userId));
        return new Response($"Observador {userId} do livro {bookId} adicionado.", null);
    }

    public Response HowManyNotifications(int userId)
    {
        var observer = (IObserver)Users.First(u => u.Id == userId);
        return new Response($"Usuária(o) id:[{userId}] foi notificada(o) [{observer.HowManyNotifications}] vezes sobre os livros que observa", null);
    }

    public Response GetUserBorrowsAndReserves(int userId)
    {
        var user = Users.First(u => u.Id == userId);
        var sb = new StringBuilder();
        if(!user.BookReserves.Any() && !user.BorrowedCopies.Any())
            sb.AppendLine($"Não existem reservas nem empréstimos para a usuária {user.Name}");

        if(user.BookReserves.Any())
        {
            sb.AppendLine($"Livros reservados pela(o) usuária(o) {user.Name}:");
            user.BookReserves.ForEach(br => 
                {
                    sb.AppendLine(
                        $"Título: {br.Book.Title} Data da reserva: {br.ReservationDate.ToString()}"
                    );
                }
            );
        }
        
        if(user.BorrowedCopies.Any())
        {
            sb.AppendLine($"Livros emprestados a(o) usuária(o) {user.Name}");
            user.BorrowedCopies.ForEach(bc => 
                {
                    sb.AppendLine(
                        $"Título: {bc.Book.Title}" +
                        $"Data de devolução do empréstimo: {bc.BorrowedDate.ToString()}" +
                        $"Status do empréstimo: {bc.CopyStatus}"
                    );
                }
            );
        }
        return new Response(sb.ToString(), null);
    }

    public Response GetBookInformation(int bookId)
    {
        var book = Books.First(b => b.Id == bookId);
        var reservationsCount = book.Reservations.Count;
        var sb = new StringBuilder();
        if(reservationsCount > 0)
        {
            sb.AppendLine($"Título do livro: {book.Title} tem {reservationsCount} reservas");
            book.Reservations.ForEach(br => 
                {
                    sb.AppendLine($"Reserva {br.Id} reservada por {br.User.Name}");
                });
        }
        book.Copies.ForEach(cp => 
            {
                if(cp.Borrower is not null)
                    sb.AppendLine($"Exemplar [{cp.Id}] com status {cp.CopyStatus} atualmente em posse da(o) usuária(o) {cp.Borrower.Name}");
                else
                    sb.AppendLine($"Exemplar [{cp.Id}] com status {cp.CopyStatus}");
            });
        return new Response(sb.ToString(), null);
    }

    public Response GetBackCopy(int userId, int bookId)
    {
        var user = Users.First(u => u.Id == userId);
        var copy = Copies.FirstOrDefault(cp => cp.Book.Id == bookId && cp.Borrower?.Id == user.Id);
        if(copy is not null)
        {            
            user.ReturnCopy(copy);
            copy.CopyStatus = CopyStatus.Finished;
            return new Response($"{user.Name} devolveu o livro {copy.Book.Title} com sucesso", null);
        }
        return new Response(null, $"{user.Name} não tem empréstimos ativos do livro indicado");
    }

    public Response ReserveBook(int userId, int bookId)
    {
        var user = Users.First(u => u.Id == userId);
        var book = Books.First(b => b.Id == bookId);
        var newId = 0;
        
        if(BookReserves.Any())
            newId = BookReserves.Last().Id + 1;

        var bookReserve = new BookReserve(newId, user, book);
        var reserveResult = user.ReserveBook(bookReserve);
        if(reserveResult)
        {
            book.ReservationUpdate(bookReserve);
            BookReserves.Add(bookReserve);
            return new Response($"Livro {bookReserve.Book.Title}, reservado para {user.Name} com sucesso", null);
        }
        return new Response(null, $"Já existem mais de três reservas para o usuário {user.Name}");
    }
}
