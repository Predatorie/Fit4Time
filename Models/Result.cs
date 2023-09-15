namespace Fit4Time.Models;

/// <summary> A result. </summary>
public class Result
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Result" /> class.
    /// </summary>
    /// <exception cref="InvalidOperationException"> Thrown when the requested operation is invalid. </exception>
    /// <param name="isSuccess"> True if this object is success. </param>
    /// <param name="error">The error. </param>
    protected Result(bool isSuccess, string error)
    {
        switch (isSuccess)
        {
            case true when error != string.Empty:
                throw new InvalidOperationException();

            case false when error == string.Empty:
                throw new InvalidOperationException();

            default:
                IsSuccess = isSuccess;
                Error = error;
                break;
        }
    }

    /// <summary> Gets a value indicating whether this object is success./ </summary>
    /// <value> True if this object is success, false if not. </value>
    protected bool IsSuccess { get; }

    /// <summary> Gets the error./ </summary>
    /// <value> The error. </value>
    public string Error { get; }

    /// <summary> True if this object is failure. </summary>
    public bool IsFailure => !IsSuccess;

    /// <summary> Fails. </summary>
    /// <param name="message"> The message. </param>
    /// <returns> A Result. </returns>
    public static Result Fail(string message)
    {
        return new Result(false, message);
    }

    /// <summary> Fails. </summary>
    /// <typeparam name="T"> Generic type parameter. </typeparam>
    /// <param name="message"> The message. </param>
    /// <returns> A Result. </returns>
    /// /
    public static Result<T> Fail<T>(string message)
    {
        return new Result<T>(default, false, message);
    }

    /// <summary> Gets the ok. </summary>
    /// <returns> A Result. </returns>
    public static Result Ok()
    {
        return new Result(true, string.Empty);
    }

    /// <summary> Gets the ok. </summary>
    /// <typeparam name="T"> Generic type parameter. </typeparam>
    /// <param name="value"> The value. </param>
    /// <returns> A Result. </returns>
    public static Result<T> Ok<T>(T value)
    {
        return new Result<T>(value, true, string.Empty);
    }
}

public class Result<T> : Result
{
    /// <summary> The value. </summary>
    private readonly T result;

    /// <summary> Initializes a new instance of the MDFDoors.Models.Result&lt;T&gt; class. </summary>
    /// <param name="value">	 The value. </param>
    /// <param name="isSuccess"> True if this object is success. </param>
    /// <param name="error">	 The error. </param>
    protected internal Result(T value, bool isSuccess, string error)
        : base(isSuccess, error)
    {
        result = value;
    }

    /// <summary> Gets the value. </summary>
    /// <exception cref="InvalidOperationException"> Thrown when the requested operation is invalid. </exception>
    /// <value> The value. </value>
    public T Value
    {
        get
        {
            if (!IsSuccess) throw new InvalidOperationException();

            return result;
        }
    }
}
