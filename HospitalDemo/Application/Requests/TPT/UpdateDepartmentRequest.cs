using Domain.ValueObjects;

namespace Application.Requests.TPT;

public record UpdateDepartmentRequest(
    string Name,
    PhoneNumberDto PhoneNumber,
    EmailDto EmailAddress
);