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

    public List<Copy> Copies { get; set; } = new List<Copy>();

    public List<BookReserve> Reservations { get; private set; } = new List<BookReserve>();

    private bool _hasAlreadyNotified = false;

    public void ReservationUpdate(BookReserve reservation)
    {
        Reservations.Add(reservation);
        var currentActiveReservations = Reservations.Where(r => r.IsActive);
        if(currentActiveReservations.Count() > 1 && !_hasAlreadyNotified)
        {
            _hasAlreadyNotified = true;
            Notify();
        }
        else
            _hasAlreadyNotified = false;
    }

    private List<IObserver> Observers = new List<IObserver>();

    public void Attach(IObserver observer) =>
        Observers.Add(observer);

    public void Detach(IObserver observer) =>
        Observers.Remove(observer);

    public void Notify() =>
        Observers.ForEach(o => o.Update(this));
}
