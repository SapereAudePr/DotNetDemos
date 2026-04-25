namespace Api.Responses.TPH;

public record DepartmentResponse(
    int Id,
    string Name,
    int HospitalId,
    IReadOnlyCollection<PhoneNumberResponse> PhoneNumbers,
    IReadOnlyCollection<EmailAddressResponse> EmailAddresses);

public record PhoneNumberResponse(string Number, string Label );
public record EmailAddressResponse(string Value);