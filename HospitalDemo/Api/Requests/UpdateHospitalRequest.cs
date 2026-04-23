using Domain.ValueObjects;

namespace Api.Requests;

public record UpdateHospitalRequest(
    string Name,
    string Address,
    PhoneNumber PhoneNumber,
    EmailAddress EmailAddress,
    DateTimeOffset BuiltDate
);