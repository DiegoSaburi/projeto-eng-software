public interface ICommand<T>
{
    void Execute(T data);
}
