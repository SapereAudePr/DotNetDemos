using Api.Requests.TPT;

namespace Api.Validators.Validation;

public class DepartmentTptValidation
{
    public static ValidationResult ValidateCreate(
        CreateDepartmentRequest request) => new Validator<CreateDepartmentRequest>(request)
        .RuleFor("Name", x => x.Name)
        .NotEmpty()
        .MinLength(2)
        .MaxLength(100)
        .RuleFor("HospitalId", x => x.HospitalId)
        .GreaterThanZero(0)
        .RuleFor("PhoneNumber", x => x.PhoneNumber.Number)
        .MinLength(3)
        .MaxLength(60)
        .Matches(@"^\+?[0-9\s\-]{7,15}$", "PhoneNumber format is invalid.")
        .RuleFor("PhoneLabel", x => x.PhoneNumber.Label)
        .MinLength(2)
        .MaxLength(60)
        .RuleFor("EmailAddress", x => x.Email.Value)
        .NotEmpty()
        .NotNull()
        .MinLength(4)
        .MaxLength(60)
        .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", "Email format is invalid.")
        .Validate();

    public static ValidationResult ValidateUpdate(
        UpdateDepartmentRequest request) => new Validator<UpdateDepartmentRequest>(request)
        .RuleFor("Name", x => x.Name)
        .NotEmpty()
        .NotNull()
        .MinLength(2)
        .MaxLength(100)
        .RuleFor("PhoneNumber", x => x.PhoneNumber.Number)
        .MinLength(3)
        .MaxLength(60)
        .RuleFor("PhoneLabel", x => x.PhoneNumber.Label)
        .MinLength(2)
        .MaxLength(60)
        .RuleFor("Email", x => x.EmailAddress.Value)
        .NotEmpty()
        .NotNull()
        .MinLength(4)
        .MaxLength(60)
        .Validate();
}