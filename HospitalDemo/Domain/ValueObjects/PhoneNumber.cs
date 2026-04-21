namespace Domain.ValueObjects;

public record PhoneNumber
{
    public string Number { get; } = null!;
    public string Label { get; } = null!;

    private PhoneNumber()
    {
    }

    public PhoneNumber(string number, string label)
    {
        if (string.IsNullOrWhiteSpace(number))
            throw new ArgumentException("Phone number cannot be empty", nameof(number));
        if (number.Length > 20)
            throw new ArgumentException("Phone number cannot exceed 20 characters", nameof(number));

        if (string.IsNullOrWhiteSpace(label))
            throw new ArgumentException("Label cannot be empty", nameof(label));
        if (label.Length > 120)
            throw new ArgumentException("Label cannot exceed 120 characters", nameof(label));

        Number = number;
        Label = label;
    }
}