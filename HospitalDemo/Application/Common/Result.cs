namespace Application.Common;

public class Result<T>
{
    public bool IsSuccess { get; }
    public T? Value { get; }
    public string? Error { get; }
    public ResultStatus Status { get; }

    private Result(bool isSuccess, T? value, string? error, ResultStatus status)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
        Status = status;
    }

    public static Result<T> Success(T value)
        => new(true, value, null, ResultStatus.Ok);

    public static Result<T> NotFound(string error)
        => new(false, default, error, ResultStatus.NotFound);

    public static Result<T> BadRequest(string error)
        => new(false, default, error, ResultStatus.BadRequest);

    public static Result<T> ValidationFailure(string error)
        => new(false, default, error, ResultStatus.ValidationFailure);
    
    public static Result<T> NoContent()
    => new(true, default, null, ResultStatus.NoContent);
}

public enum ResultStatus
{
    Ok,
    NotFound,
    BadRequest,
    ValidationFailure,
    NoContent
}