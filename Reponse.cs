public record class Response (string? SuccessMessage, string? ErrorMessage)
{
    public bool HasError { get => ErrorMessage is not null; }
}
