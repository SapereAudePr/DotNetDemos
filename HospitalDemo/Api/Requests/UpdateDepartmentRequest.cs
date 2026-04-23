using Domain.ValueObjects;

namespace Api.Requests;

public record UpdateDepartmentRequest(
    string Name,
    PhoneNumber PhoneNumber,
    EmailAddress Email);