using Domain.ValueObjects;

namespace Api.Requests.TPT;

public record UpdateDepartmentRequest(
    string Name,
    PhoneNumber PhoneNumber,
    EmailAddress EmailAddress
);