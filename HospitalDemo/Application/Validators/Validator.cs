using System.Text.RegularExpressions;

namespace Application.Validators;

public record ValidationResult(bool IsValid, IReadOnlyList<ValidationError> Errors)
{
    public static ValidationResult Success() =>
        new(true, Array.Empty<ValidationError>());
}

public record ValidationError(string Field, string Message);

public class Validator<T>
{
    private readonly T _subject;
    private readonly List<ValidationError> _errors = new();

    public Validator(T subject) => _subject = subject;

    public PropertyValidator<T, TProp> RuleFor<TProp>(
        string fieldName,
        Func<T, TProp> selector)
        => new(this, fieldName, selector(_subject));

    internal void AddError(string field, string message)
        => _errors.Add(new ValidationError(field, message));

    public ValidationResult Validate()
        => _errors.Count == 0
            ? ValidationResult.Success()
            : new ValidationResult(false, _errors);
}

public class PropertyValidator<T, TProp>
{
    private readonly Validator<T> _parent;
    private readonly string _field;
    private readonly TProp _value;

    public PropertyValidator(Validator<T> parent, string field, TProp value)
    {
        _parent = parent;
        _field = field;
        _value = value;
    }

    public PropertyValidator<T, TProp> NotNull()
    {
        if (_value is null)
            _parent.AddError(_field, $"{_field} is required.");
        return this;
    }

    public PropertyValidator<T, TProp> NotEmpty()
    {
        if (_value is string s && string.IsNullOrWhiteSpace(s))
            _parent.AddError(_field, $"{_field} must not be empty.");
        return this;
    }

    public PropertyValidator<T, TProp> MaxLength(int max)
    {
        if (_value is string s && s.Length > max)
            _parent.AddError(_field, $"{_field} must not exceed {max} characters.");
        return this;
    }

    public PropertyValidator<T, TProp> MinLength(int min)
    {
        if (_value is string s && s.Length < min)
            _parent.AddError(_field, $"{_field} must be at least {min} characters.");
        return this;
    }

    public PropertyValidator<T, TProp> Min(object min, string? errorMessage = null)
    {
        if (_value is IComparable comparable && comparable.CompareTo(min) < 0)
        {
            _parent.AddError(_field, errorMessage ?? $"{_field} must be greater than {min}.");
        }

        return this;
    }

    public PropertyValidator<T, TProp> Max(object max, string? errorMessage = null)
    {
        if (_value is IComparable comparable && comparable.CompareTo(max) > 0)
        {
            _parent.AddError(_field, errorMessage ?? $"{_field} must be less than {max}.");
        }

        return this;
    }

    public PropertyValidator<T, TProp> Matches(string pattern, string? errorMessage = null)
    {
        if (_value is string s && !Regex.IsMatch(s, pattern))
            _parent.AddError(_field, errorMessage ?? $"{_field} format is invalid.");
        return this;
    }

    public PropertyValidator<T, TProp> GreaterThanZero(TProp min)
    {
        if (_value is IComparable comparable && comparable.CompareTo(min) <= 0)
            _parent.AddError(_field, $"{_field} must be greater than {min}.");
        return this;
    }

    public PropertyValidator<T, TNext> RuleFor<TNext>(
        string fieldName,
        Func<T, TNext> selector)
        => _parent.RuleFor(fieldName, selector);

    public ValidationResult Validate() => _parent.Validate();
}