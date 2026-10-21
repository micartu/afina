namespace AfinApp.Data.Shared;

public enum ErrorType
{
    None,
    General,
    Validation,
    Credentials,
    Unauthorized,
    NotFound
}

public sealed record Error(ErrorType Type, string Code, string? Description = null)
{
    public static readonly Error None = new(ErrorType.None, string.Empty, default);
}