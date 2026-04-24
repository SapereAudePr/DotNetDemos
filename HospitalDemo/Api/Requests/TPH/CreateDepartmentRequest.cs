using Domain.ValueObjects;

namespace Api.Requests.TPH;

public record CreateDepartmentRequest(
    string Name,
    int HospitalId,
    PhoneNumber PhoneNumber,
    EmailAddress Email);