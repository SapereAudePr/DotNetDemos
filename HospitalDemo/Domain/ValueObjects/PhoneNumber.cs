namespace Domain.ValueObjects;

public record PhoneNumber
{
    public string Number { get; init; } = null!;
    public string Label { get; init; } = null!;

    private PhoneNumber() {}
    
    public PhoneNumber(string number, string label)
    {
        if (string.IsNullOrWhiteSpace(number) || number.Length > 20)
            throw new ArgumentException("Invalid number");

        if (string.IsNullOrWhiteSpace(label) || label.Length > 120)
            throw new ArgumentException("Invalid label");

        Number = number;
        Label = label;
    }
}