public interface IBorrowStrategy
{
    public Copy? CanBorrowCopy(User user, IEnumerable<Copy> copies);
}
