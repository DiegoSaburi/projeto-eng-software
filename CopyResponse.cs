public record class CopyResponse(string? SuccessMessage, string? ErrorMessage, Copy? Copy = null) 
    : Response(ErrorMessage, SuccessMessage);
