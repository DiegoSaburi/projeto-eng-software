public sealed class LibraryManagement
{
    public static List<Book> Books = new List<Book>() 
    {
        new Book(0, "Livro 1", new List<string> { "Autor 1", "Autor 2" }, "edição 1", "2017"),
        new Book(1, "Livro 2", new List<string> { "Autor 1", "Autor 2" }, "edição 1", "2017")
    };

    public static List<User> Users = new List<User>()
    {
        new Teacher(0, "Professor 1"),
        new UndergraduateStudent(1, "Estudante de graduação 1"),
        new GraduateStudent(2, "Estudante de pós-graduação 1")
    };

    private static LibraryManagement _instance = new LibraryManagement();
    
    public static LibraryManagement GetInstance()
    {
        if(_instance is null)
            _instance = new LibraryManagement();
        return _instance;
    }
}
