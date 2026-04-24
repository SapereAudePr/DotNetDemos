using Domain.ValueObjects;

namespace Api.Requests.TPH;

public record PatchDepartmentRequest(
    string? Name,
    PhoneNumber? PhoneNumber,
    EmailAddress? Email);