using FluentResults;

namespace Utapoi.Application.Common.Extensions;

public static class ResultExtensions
{
    /// <summary>
    ///    Checks if the result has an error of type <typeparamref name="TError" />
    ///    and returns the value of the metadata with the specified <paramref name="key" />.
    /// </summary>
    /// <typeparam name="TError">The type of the error.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="key">The metadata key.</param>
    /// <returns>
    ///   The value of the metadata with the specified <paramref name="key" />.
    /// </returns>
    public static object? GetValueFromError<TError>(this IResultBase? result, string key) where TError : IError
    {
        if (result is null)
        {
            return default;
        }

        if (!result.Errors.Any(x => x is TError))
        {
            return default;
        }

        var error = result.Errors.First(x => x is TError);

        if (error.Metadata.TryGetValue(key, out var value))
        {
            return value;
        }

        return default;
    }
}
