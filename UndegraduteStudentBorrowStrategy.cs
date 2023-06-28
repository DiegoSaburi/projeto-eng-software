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

        bool libraryHasAvailableCopies = copies.Any(c => !c.IsBorrowed);
        bool userHasBookReserve = user.BookReserves.Any(br => br.Book.Id == bookId);
        bool libraryHasMoreOrEqualReservesCopiesRatio = 
            user.BookReserves.Count(br => br.Book.Id == bookId) < copies.Count();

        if(userHasBookReserve
           && libraryHasMoreOrEqualReservesCopiesRatio 
           && hasValidUserRules 
           && libraryHasAvailableCopies)
            return copies.First(c => !c.IsBorrowed);
        
        bool libraryHasMoreReservesThanCopies = 
            user.BookReserves.Count(br => br.Book.Id == bookId) < copies.Count();
        if(libraryHasMoreReservesThanCopies)
            return null;
        return copies.First(c => !c.IsBorrowed);
    }
}