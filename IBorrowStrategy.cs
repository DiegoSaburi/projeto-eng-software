public interface IBorrowStrategy
{
    Copy? CanBorrowCopy(User user, IEnumerable<Copy> copies);
}
