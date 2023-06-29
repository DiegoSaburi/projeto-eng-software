public sealed class UndergraduateStudentBorrowStrategy : IBorrowStrategy
{
    public Copy? CanBorrowCopy(User user, IEnumerable<Copy> copies)
    {
        if(copies is null || !copies.Any())
            return null;

        int bookId = copies.First().Book.Id;
        Book book = copies.First().Book;
        
        bool userAlreadyHasCopy = 
            copies.Any(c => user.BorrowedCopies.Any(c2 => c2.Id == c.Id));
    
        if(userAlreadyHasCopy)
            return null;

        bool hasValidUserRules = user.BorrowedCopies.Count < 3
        && user.BorrowedCopies.All(b => b.BorrowedTime > DateTime.Now.TimeOfDay);

        bool libraryHasAvailableCopies = copies.Any(c => c.CopyStatus.Equals(CopyStatus.Finished));

        bool userHasBookReserve = user.BookReserves.Any(br => br.Book.Id == bookId);

        bool libraryHasEqualReservesCopiesRatio = 
            book.Reservations.Count == copies.Count();
        bool libraryHasMoreCopiesThenReserves = user.BookReserves.Count(br => br.Book.Id == bookId) < copies.Count();

        bool UserHasReserveOrLibraryHasSufficientCopies = (userHasBookReserve && libraryHasEqualReservesCopiesRatio) || (!userHasBookReserve && libraryHasMoreCopiesThenReserves);

        bool copiesEqualReservesAndUserDoesntHaveReserve = !userHasBookReserve && libraryHasEqualReservesCopiesRatio;

        if(!libraryHasMoreCopiesThenReserves || copiesEqualReservesAndUserDoesntHaveReserve) 
            return null;

        if(UserHasReserveOrLibraryHasSufficientCopies
           && hasValidUserRules 
           && libraryHasAvailableCopies)
            return copies.First(c => c.CopyStatus.Equals(CopyStatus.Finished));
        
        return null;
    }
}
