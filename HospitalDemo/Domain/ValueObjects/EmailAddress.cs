using Domain.Common;

namespace Domain.ValueObjects;

public record EmailAddress
{
    public string Value { get; } = null!;

    private EmailAddress()
    {
    }

    public EmailAddress(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(value));

        Value = value.ValidateEmailRegex(normalize: true);
    }
}