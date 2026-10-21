using AfinApp.Data.Shared;

namespace AfinApp.ApiService.Helpers;

public static class ApiResultHelper
{
    public static IResult HandleError(Error? error, string defaultMessage)
    {
        var desc = error?.Description ?? defaultMessage;
        return error?.Type switch
        {
            ErrorType.Validation => Results.BadRequest(desc),
            ErrorType.NotFound => Results.NotFound(desc),
            ErrorType.Credentials => Results.Json(new { error = desc }, statusCode: 403),
            ErrorType.Unauthorized => Results.Problem(desc),
            _ => Results.Problem(desc)
        };
    }
}