using Domain.ValueObjects;

namespace Application.Requests.TPT;

public record UpdateDepartmentRequest(
    string Name,
    PhoneNumber PhoneNumber,
    EmailAddress EmailAddress
);