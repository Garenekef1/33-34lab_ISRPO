namespace NotesApp.Helpers;

public class ApiResponse<T>
{
    public bool Success { get; set; } = true;
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
    public int StatusCode { get; set; }

    public static ApiResponse<T> Ok(T data, string message = "Успешно") => new()
    {
        Message = message,
        Data = data,
        StatusCode = 200
    };

    public static ApiResponse<T> Created(T data, string message = "Создано успешно") => new()
    {
        Message = message,
        Data = data,
        StatusCode = 201
    };
}

public class ApiError
{
    public bool Success { get; set; } = false;
    public string Message { get; set; } = string.Empty;
    public List<string> Errors { get; set; } = new();
    public int StatusCode { get; set; }

    public static ApiError NotFound(string message) => new()
    {
        Message = message,
        StatusCode = 404
    };

    public static ApiError BadRequest(string message, List<string>? errors = null) => new()
    {
        Message = message,
        Errors = errors ?? new(),
        StatusCode = 400
    };

    public static ApiError Internal(string message = "Внутренняя ошибка сервера") => new()
    {
        Message = message,
        StatusCode = 500
    };
}
