using Domain.ValueObjects;

namespace Api.Requests.TPH;

public record UpdateDepartmentRequest(
    string Name,
    PhoneNumber PhoneNumber,
    EmailAddress Email);