public static class Result
{
    public static Success<T> Success<T>(T data)
        => new(data);

    public static Error Error(string message, int? code = null)
        => new(message, code);
}