public sealed class UndergraduateStudentBorrowStrategy : IBorrowStrategy
{
    public Copy? CanBorrowCopy(User user, IEnumerable<Copy> copies)
    {
        
        int bookId = copies.First().Book.Id;
        
        bool userAlreadyHasCopy = 
            copies.Any(c => user.BorrowedCopies.Any(c2 => c2.Id == c.Id));
    
        if(userAlreadyHasCopy)
            return null;

        bool hasValidUserRules = user.BorrowedCopies.Count < 3
        && user.BorrowedCopies.All(b => b.BorrowedTime > DateTime.Now.TimeOfDay);

        bool libraryHasAvailableCopies = copies.Any(c => c.CopyStatus.Equals(CopyStatus.Finished));
        bool userHasBookReserve = user.BookReserves.Any(br => br.Book.Id == bookId);
        bool libraryHasEqualReservesCopiesRatio = 
            user.BookReserves.Count(br => br.Book.Id == bookId) == copies.Count();
        bool libraryHasMoreCopiesThenReserves = user.BookReserves.Count(br => br.Book.Id == bookId) < copies.Count();
        //refatorar essa desgraça de nome gigantesco
        bool copiesEqualReserveAndUserHasReserve = (userHasBookReserve && libraryHasEqualReservesCopiesRatio) || (!userHasBookReserve && libraryHasMoreCopiesThenReserves);

        bool copiesEqualReservesAndUserDontReserve = !userHasBookReserve && libraryHasEqualReservesCopiesRatio;

        if(!libraryHasMoreCopiesThenReserves || copiesEqualReservesAndUserDontReserve) return null;

        if(copiesEqualReserveAndUserHasReserve
           && hasValidUserRules 
           && libraryHasAvailableCopies)
            return copies.First(c => c.CopyStatus.Equals(CopyStatus.Finished));
        
        return null;
    }
}
