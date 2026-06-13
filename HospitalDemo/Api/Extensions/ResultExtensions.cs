using Application.Common;

namespace Api.Extensions;

public static class ResultExtensions
{
    public static IResult ToHttpResult<T>(
        this Result<T> result,
        Func<T, IResult> onSuccess)
    {
        return result.Status switch
        {
            ResultStatus.Ok => onSuccess(result.Value!),
            ResultStatus.NotFound => Results.NotFound(result.Error),
            ResultStatus.BadRequest => Results.BadRequest(result.Error),
            ResultStatus.ValidationFailure => Results.UnprocessableEntity(result.Error),
            _ => throw new InvalidOperationException($"Invalid result status {result.Status}")
        };
    }
}
