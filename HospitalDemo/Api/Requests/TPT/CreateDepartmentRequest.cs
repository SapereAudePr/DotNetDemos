namespace Api.Requests.TPT;

public record CreateDepartmentRequest(
    string Name,
    int HospitalId,
    PhoneNumberDto PhoneNumber,
    EmailDto Email
);
