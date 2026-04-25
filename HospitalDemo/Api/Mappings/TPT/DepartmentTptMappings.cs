using Api.Responses.TPT;
using Domain.Entities.TPT;

namespace Api.Mappings.TPT;

public static class DepartmentMappings
{
    public static List<DepartmentResponse> ToResponse(this IEnumerable<Department> departments)
    {
        return departments.Select(d => d.ToResponse()).ToList();
    }

    public static DepartmentResponse ToResponse(this Department d)
    {
        return new DepartmentResponse(
            d.Id,
            d.Name,
            d.HospitalId,
            d.PhoneNumbers.Select(x => new PhoneNumberResponse(x.Number, x.Label)).ToList(),
            d.EmailAddresses.Select(x => new EmailAddressResponse(x.Value)).ToList());
    }
}