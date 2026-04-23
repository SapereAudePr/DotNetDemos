using Domain.ValueObjects;

namespace Api.Requests;

public record CreateHospitalRequest(
    string Name,
    string Address,
    PhoneNumber PhoneNumber,
    EmailAddress EmailAddress,
    DateTimeOffset BuiltDate
);