using Domain.Common;

namespace Domain.ValueObjects;

public record EmailAddress
{
    public string Value { get; } = null!;
    private EmailAddress() { }

    public EmailAddress(string value)
    {
        Value = value.ValidateEmailRegex(normalize: true);
    }
}