public interface ICommand<T>
{
    public void Execute(T data);
}
