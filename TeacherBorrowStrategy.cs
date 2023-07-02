public sealed class TeacherBorrowStrategy : IBorrowStrategy
{
    private List<string> Errors { get; set; } = new List<string>();

    public CopyResponse CanBorrowCopy(User user, IEnumerable<Copy> copies)
    {
        Book book = copies.First().Book;
        ValidateUserHasNoOverdue(user);
        ValidateLibraryHasAvailableCopies(copies);

        if(Errors.Count > 0)
            return new CopyResponse(null, Errors.First());
            return new CopyResponse($"Empréstimo efetuado do livro {book.Title} para {user.Name} com sucesso", null, copies.First(c => c.CopyStatus.Equals(CopyStatus.Finished)));
    }
    
    private void ValidateUserHasNoOverdue(User user)
    {
        if(!user.BorrowedCopies
                .All(b => b.CopyStatus == CopyStatus.Running && b.BorrowedTime > DateTime.Now.TimeOfDay))
            Errors.Add($"{user.Name} tem um débito em aberto, não será possível efetuar o empréstimo.");
    }
    
    private void ValidateLibraryHasAvailableCopies(IEnumerable<Copy> copies)
    {
        if(!copies.Any(c => c.CopyStatus.Equals(CopyStatus.Finished)))
            Errors.Add("Não existem exemplares deste livro disponíveis no momento, não será possível efetuar o empréstimo.");
    }
}
