using static System.Runtime.InteropServices.JavaScript.JSType;
namespace SharedKernel.Result;

public interface IResult
{
    List<Error> Errors { get; }
    bool IsFailure { get; }
    bool IsSuccess { get; }
}

public class Result : IResult
{
    private Result(bool isSuccess, List<Error> errors)
    {
        if (isSuccess && errors.Count > 0 ||
            !isSuccess && errors.Count == 0)
            throw new ArgumentException("Invalid errors", nameof(errors));
        IsSuccess = isSuccess;
        Errors = errors;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public List<Error> Errors { get; }

    public static Result Success() => new Result(true, new List<Error>());

    public static Result Failure(Error error) => new Result(false, new List<Error> { error });

    public static Result Failure(List<Error> errors) => new Result(false, errors);

    public TNextValue Match<TNextValue>(Func<TNextValue> onSuccess, Func<List<Error>, TNextValue> onError)
    {
        return IsFailure ? onError(Errors) : onSuccess();
    }
}

public class Result<T> : IResult
{
    private Result(bool isSuccess, T value, List<Error> errors)
    {
        if (isSuccess && errors.Count > 0 ||
            !isSuccess && errors.Count == 0)
            throw new ArgumentException("Invalid errors", nameof(errors));
        IsSuccess = isSuccess;
        Value = value;
        Errors = errors;
    }

    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public T Value { get; }
    public List<Error> Errors { get; }

    public static Result<T> Success(T value) => new Result<T>(true, value, new List<Error>());

    public static Result<T> Failure(Error error) => new Result<T>(false, default, new List<Error> { error });

    public static Result<T> Failure(List<Error> errors) => new Result<T>(false, default, errors);

    public TNextValue Match<TNextValue>(Func<T, TNextValue> onValue, Func<List<Error>, TNextValue> onError)
    {
        return IsFailure ? onError(Errors) : onValue(Value);
    }
}