using Domain.ValueObjects;

namespace Api.Requests;

public record CreateDepartmentRequest(
    string Name,
    int HospitalId,
    PhoneNumber PhoneNumber,
    EmailAddress Email);