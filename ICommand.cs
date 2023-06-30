public interface ICommand<T>
{
    Response Execute(T data);
}
