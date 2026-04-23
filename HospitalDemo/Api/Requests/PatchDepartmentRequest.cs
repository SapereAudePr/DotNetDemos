using Domain.ValueObjects;

namespace Api.Requests;

public record PatchDepartmentRequest(
    string? Name,
    PhoneNumber? PhoneNumber,
    EmailAddress? Email);