namespace Coffee.Bean.Shop.Core;

public class Result<T>
{
    public T? Value { get; }
    public bool IsSuccess { get; }
    public IEnumerable<string> Errors { get; }

    private Result(T? value, bool isSuccess, IEnumerable<string> errors)
    {
        Value = value;
        IsSuccess = isSuccess;
        Errors = errors;
    }

    public static Result<T> Success(T value) => new(value, true, Array.Empty<string>());

    public static Result<T> Failure(IEnumerable<string> errors) => new(default, false, errors);
}
