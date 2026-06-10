public abstract record AppResult
{
    public abstract string Status { get; }
}
public record Success<T>(T Data) : AppResult
{
    public override string Status => "success";
}
public record Error(string Message, int? Code = null) : AppResult
{
    public override string Status => "error";
}