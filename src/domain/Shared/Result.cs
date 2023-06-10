namespace Domain.Shared;

public class Result
{
    protected Result(bool isSuccess, string error)
    {
        if(isSuccess && !string.IsNullOrWhiteSpace(error))
        {
            throw new InvalidOperationException("A success result can not contain an error message.");
        }

        if(!isSuccess && string.IsNullOrWhiteSpace(error))
        {
            throw new InvalidOperationException("A failure result must contain an error message.");
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public string Error { get; }

    public static Result Ok() => new Result(true, string.Empty);

    public static Result<TValue> Ok<TValue>(TValue? value)
        where TValue : class
        => new Result<TValue>(value, true, string.Empty);

    public static Result Fail(string error) => new Result(false, error);

    public static Result<TValue> Fail<TValue>(string error)
        where TValue : class
        => new Result<TValue>(null, false, error);
}

public class Result<TValue> : Result
    where TValue : class
{
    private readonly TValue? _value;

    protected internal Result(TValue? value, bool isSuccess, string error)
        : base(isSuccess, error)
    {
        _value = value;
    }

    public TValue? Value()
    {
        if(IsFailure)
        {
            throw new InvalidOperationException("Can not access the value of a failure result.");
        }

        return _value;
    }
}