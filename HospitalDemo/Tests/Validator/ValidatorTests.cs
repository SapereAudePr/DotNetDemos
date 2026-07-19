using Application.Validators;
using Application.Validators.Validation;
using Xunit.Abstractions;

namespace Tests.Validator;

public class ValidatorTests
{
    private record Subject(string Name, int Age);

    private readonly ITestOutputHelper _output;

    public ValidatorTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void NotEmpty_StringIsEmpty_AddsError(string str)
    {
        var subject = new Subject(str, 10);

        var result = new Validator<Subject>(subject)
            .RuleFor("Name", x => x.Name)
            .NotEmpty()
            .Validate();

        _output.WriteLine(result.ToString());

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.Field == "Name");
    }

    [Fact]
    public void NotEmpty_StringIsNotEmpty_DoesNotAddError()
    {
        var subject = new Subject("John", 10);
        var result = new Validator<Subject>(subject)
            .RuleFor("Name", x => x.Name)
            .NotEmpty()
            .Validate();

        _output.WriteLine(result.ToString());

        Assert.True(result.IsValid);
    }

    [Fact]
    public void NotNull_StringIsNull_AddsError()
    {
        var subject = new Subject(null!, 10);
        var result = new Validator<Subject>(subject)
            .RuleFor("Name", x => x.Name)
            .NotNull()
            .Validate();

        _output.WriteLine(result.ToString());

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.Field == "Name");
    }

    [Fact]
    public void NotNull_StringIsNotNull_DoesNotAddError()
    {
        var subject = new Subject("John", 10);
        var result = new Validator<Subject>(subject)
            .RuleFor("Name", x => x.Name)
            .NotNull()
            .Validate();

        _output.WriteLine(result.ToString());

        Assert.True(result.IsValid);
    }

    [Fact]
    public void MaxLength_WhenStringIsTooLong_AddsError()
    {
        var subject = new Subject("John", 10);
        var result = new Validator<Subject>(subject)
            .RuleFor("Name", x => x.Name)
            .MaxLength(3)
            .Validate();

        _output.WriteLine(result.ToString());

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.Field == "Name");
    }

    [Fact]
    public void MaxLength_WhenStringIsNotLong_DoesNotAddError()
    {
        var subject = new Subject("John", 10);
        var result = new Validator<Subject>(subject)
            .RuleFor("Name", x => x.Name)
            .MaxLength(5)
            .Validate();

        _output.WriteLine(result.ToString());

        Assert.True(result.IsValid);
    }

    [Fact]
    public void MinLength_WhenStringIsNotLongEnough_AddsError()
    {
        var subject = new Subject("John", 10);
        var result = new Validator<Subject>(subject)
            .RuleFor("Name", x => x.Name)
            .MinLength(5)
            .Validate();

        _output.WriteLine(result.ToString());

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.Field == "Name");
    }

    [Fact]
    public void MinLength_WhenStringIsLongEnough_DoesNotAddError()
    {
        var subject = new Subject("John", 10);
        var result = new Validator<Subject>(subject)
            .RuleFor("Name", x => x.Name)
            .MinLength(3)
            .Validate();

        _output.WriteLine(result.ToString());

        Assert.True(result.IsValid);
    }


    [Fact]
    public void Max_ValueExceedsMaxLimit_AddsError()
    {
        var subject = new Subject("John", 10);
        var result = new Validator<Subject>(subject)
            .RuleFor("Age", x => x.Age)
            .Max(1)
            .Validate();

        _output.WriteLine(result.ToString());

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.Field == "Age");
    }

    [Fact]
    public void Max_ValueNotExceedsMaxLimit_DoesNotAddError()
    {
        var subject = new Subject("John", 10);
        var result = new Validator<Subject>(subject)
            .RuleFor("Age", x => x.Age)
            .Max(10)
            .Validate();

        _output.WriteLine(result.ToString());

        Assert.True(result.IsValid);
    }

    [Fact]
    public void Min_ValueExceedsMinLimit_AddsError()
    {
        var subject = new Subject("John", 3);
        var result = new Validator<Subject>(subject)
            .RuleFor("Age", x => x.Age)
            .Min(10)
            .Validate();

        _output.WriteLine(result.ToString());

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.Field == "Age");
    }

    [Fact]
    public void Min_ValueNotExceedsMinLimit_DoesNotAddError()
    {
        var subject = new Subject("John", 11);
        var result = new Validator<Subject>(subject)
            .RuleFor("Age", x => x.Age)
            .Min(11)
            .Validate();

        _output.WriteLine(result.ToString());

        Assert.True(result.IsValid);
    }

    [Theory]
    [InlineData("wqfqwfgq")]
    [InlineData("       ")]
    [InlineData("dwqmdqwk@1com")]
    public void Matches_WhenEmailIsNotValid_AddsError(string email)
    {
        var subject = new Subject(email, 10);
        var result = new Validator<Subject>(subject)
            .RuleFor("Name", x => x.Name)
            .Matches(ValidationPatterns.EmailPattern)
            .Validate();

        _output.WriteLine(result.ToString());

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.Field == "Name");
    }

    [Theory]
    [InlineData("dqwmkfgqw@gmail.com")]
    [InlineData("test@test.com")]
    [InlineData("23124251235@xxcwq.com")]
    public void Matches_WhenEmailIsValid_DoesNotAddError(string email)
    {
        var subject = new Subject(email, 10);
        var result = new Validator<Subject>(subject)
            .RuleFor("Name", x => x.Name)
            .Matches(ValidationPatterns.EmailPattern)
            .Validate();

        _output.WriteLine(result.ToString());

        Assert.True(result.IsValid);
    }


    [Fact]
    public void GreaterThan_WhenNumberIsNotGreaterThanTheValue_AddsError()
    {
        var subject = new Subject("John", 0);
        var result = new Validator<Subject>(subject)
            .RuleFor("Age", x => x.Age)
            .GreaterThan(1)
            .Validate();

        _output.WriteLine(result.ToString());

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.Field == "Age");
    }

    [Fact]
    public void GreaterThan_WhenNumberIsGreaterThanTheValue_DoesNotAddError()
    {
        var subject = new Subject("John", 3);
        var result = new Validator<Subject>(subject)
            .RuleFor("Age", x => x.Age)
            .GreaterThan(2)
            .Validate();

        _output.WriteLine(result.ToString());

        Assert.True(result.IsValid);
    }
    
    [Fact]
    public void Validate_WhenMultipleFieldsAreInvalid_CollectsErrors()
    {
        var subject = new Subject(" ", 9);

        var result = new Validator<Subject>(subject)
            .RuleFor("Name", x => x.Name)
            .NotEmpty()
            .RuleFor("Age", x => x.Age)
            .GreaterThan(10)
            .Validate();

        // foreach (var error in result.Errors)
        // {
        //     _output.WriteLine(error.ToString());
        // }
        
        _output.WriteLine(
            string.Join(Environment.NewLine,
                result.Errors.Select(x => x.Message)));
        
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, x => x.Field == "Name");
        Assert.Contains(result.Errors, x => x.Field == "Age");
        Assert.Equal(2, result.Errors.Count);
    }

    [Fact]
    public void Validate_WhenFieldsAreValid_DoesNotCollectErrors()
    {
        var subject = new Subject("Name", 10);
        var result = new Validator<Subject>(subject)
            .RuleFor("Name", x => x.Name)
            .NotEmpty()
            .MaxLength(5)
            .RuleFor("Age", x => x.Age)
            .Max(11)
            .Validate();

        // foreach (var err in result.Errors)
        // {
        //     _output.WriteLine(err.ToString());
        // }

        _output.WriteLine(
            string.Join(Environment.NewLine, result.Errors.Select(
                x => x.Message)));
        
        Assert.True(result.IsValid);
    }

    [Fact]
    public void Validate_WhenOneFieldIsInvalid_CollectsError()
    {
        var subject = new Subject("     ", 10);
        var result = new Validator<Subject>(subject)
            .RuleFor("Name", x => x.Name)
            .NotEmpty()
            .RuleFor("Age", x => x.Age)
            .Max(12)
            .Validate();

        _output.WriteLine(result.Errors.First().Message);
        
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, x => x.Field == "Name");
        Assert.Single(result.Errors);
    }
}