using Domain.ValueObjects;

namespace Api.Requests.TPH;

public record UpdateHospitalRequest(
    string Name,
    string Address,
    PhoneNumber PhoneNumber,
    EmailAddress EmailAddress,
    DateTimeOffset BuiltDate
);