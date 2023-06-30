public sealed class GraduateStudentBorrowStrategy : IBorrowStrategy
{
    private List<string> Errors { get; set; } = new List<string>();
    private string? BookTitle { get; set; }

    public CopyResponse CanBorrowCopy(User user, IEnumerable<Copy> copies)
    {
        ValidateUserAlreadyHasCopy(user, copies);
        ValidateUserIsUnderCopiesCountLimit(user);
        ValidateUserHasNoOverdue(user);
        ValidateLibraryHasAvailableCopies(copies);
        ValidateLibraryHasEqualCopiesAndReserves(user, copies);
        if(Errors.Count > 0)
            return new CopyResponse(null, Errors.First(), null);
        return new CopyResponse($"Empréstimo efetuado do livro {BookTitle} para {user.Name} com sucesso", null, copies.First(c => c.CopyStatus.Equals(CopyStatus.Finished)));
    }
    
    private void ValidateUserAlreadyHasCopy(User user, IEnumerable<Copy> copies)
    {
        if(copies.Any(c => user.BorrowedCopies.Any(c2 => c2.Id == c.Id)))
            Errors.Add($"{user.Name} já detém um exemplar deste livro, não será possível efetuar o empréstimo.");
    }

    private void ValidateUserIsUnderCopiesCountLimit(User user)
    {
        if(!(user.BorrowedCopies.Count < 4))
            Errors.Add($"{user.Name} já alcançou o limite de empréstimos, não será possível efetuar o empréstimo.");
    }
    
    private void ValidateUserHasNoOverdue(User user)
    {
        if(!user.BorrowedCopies
                .All(b => b.BorrowedTime > DateTime.Now.TimeOfDay))
            Errors.Add($"{user.Name} tem um débito em aberto, não será possível efetuar o empréstimo.");
    }
    
    private void ValidateLibraryHasAvailableCopies(IEnumerable<Copy> copies)
    {
        if(!copies.Any(c => c.CopyStatus.Equals(CopyStatus.Finished)))
            Errors.Add("Não existem exemplares deste livro disponíveis no momento, não será possível efetuar o empréstimo.");
    }
    
    private void ValidateLibraryHasEqualCopiesAndReserves(User user, IEnumerable<Copy> copies)
    {
        int bookId = copies.First().Book.Id;
        Book book = copies.First().Book;
        
        bool userHasBookReserve = user.BookReserves.Any(br => br.Book.Id == bookId);
        
        bool libraryHasMoreCopiesThenReserves = user.BookReserves.Count(br => br.Book.Id == bookId) < copies.Count();
        
        bool libraryHasEqualReservesCopiesRatio = 
            book.Reservations.Count == copies.Count();
        
        bool copiesEqualReservesAndUserDoesntHaveReserve = !userHasBookReserve && libraryHasEqualReservesCopiesRatio;

        if(!libraryHasMoreCopiesThenReserves || copiesEqualReservesAndUserDoesntHaveReserve) 
            Errors.Add("Todos os exemplares estão reservados, não será possível efetuar o empréstimo.");
    }
}
