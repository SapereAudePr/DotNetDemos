namespace Application.Validators.Validation;

public static class ValidationPatterns
{
    public const string EmailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
    public const string PhonePattern = @"^\+?[0-9\s\-]{7,15}$";
}