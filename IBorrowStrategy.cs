public interface IBorrowStrategy
{
    CopyResponse CanBorrowCopy(User user, IEnumerable<Copy> copies);
}
