public class Book : ISubject
{
    public Book(int id, string title, List<string> authors,string editor, string edition,
            string publicationYear)
    {
        Id = id;
        Title = title;
        Authors = authors;
        Edition = edition;
        Editor = editor;
        PublicationYear = publicationYear;
    }

    public int Id { get; init; }
    public string Title { get; init; }
    public List<string> Authors { get; init; }
    public string Edition { get; init; }
    public string Editor { get; init; }
    public string PublicationYear { get; init; }

    public List<Copy> Copies { get; set; }

    private List<BookReserve> _reservations = new List<BookReserve>();
    public List<BookReserve> Reservations 
    { 
        get => _reservations; 
        set 
        {
            _reservations = value;
            if(_reservations.Count > 2)
                Notify();
        }
    }

    private List<IObserver> Observers = new List<IObserver>();

    public void Attach(IObserver observer) =>
        Observers.Add(observer);

    public void Detach(IObserver observer) =>
        Observers.Remove(observer);

    public void Notify() =>
        Observers.ForEach(o => o.Update(this));
}
