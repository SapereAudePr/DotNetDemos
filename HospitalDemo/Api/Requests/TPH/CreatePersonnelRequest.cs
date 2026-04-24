namespace Api.Requests.TPH;

public record CreatePersonnelRequest(
        string Type,
        string Name,
        int DepartmentId,
        string Gender,
        DateTime ShiftStart,
        DateTime ShiftEnd,
        string PhoneNumber,
        string PhoneLabel,
        string Email,
        // Type-specific — nullable, validated in the endpoint
        string? Specialization,
        string? LicenseNumber,
        string? AssignedZone,
        bool? BiohazardCertified,
        string? SecurityClearanceLevel,
        string? DeskLocation,
        bool? HandlesInsuranceBilling);