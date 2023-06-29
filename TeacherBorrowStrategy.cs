public sealed class TeacherBorrowStrategy : IBorrowStrategy
{
    public Copy? CanBorrowCopy(User user, IEnumerable<Copy> copies)
    {
        int bookId = copies.First().Book.Id;

        bool hasValidUserRules = user.BorrowedCopies.All(b => b.BorrowedTime > DateTime.Now.TimeOfDay);

        bool libraryHasAvailableCopies = copies.Any(c => c.CopyStatus.Equals(CopyStatus.Finished));

        if(hasValidUserRules && libraryHasAvailableCopies)
            return copies.First(c => c.CopyStatus.Equals(CopyStatus.Finished));
        return null;
    }
}
