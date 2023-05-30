namespace Karaoke.Application.Common.Models;

public sealed class Result
{
    public bool Succeeded { get; set; }

    public string? Message { get; set; }

    public static Result Success()
    {
        return new Result { Succeeded = true };
    }

    public static Result Failure(string message)
    {
        return new Result { Succeeded = false, Message = message };
    }
}