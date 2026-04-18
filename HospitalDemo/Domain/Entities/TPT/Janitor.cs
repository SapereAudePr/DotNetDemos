using Domain.Common;
using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Entities.TPT;

public class Janitor : Personnel
{
    private string _assignedZone = null!;
    public string AssignedZone => _assignedZone;
    public bool BiohazardCertified { get; private set; }

    private string _securityClearanceLevel = null!;
    public string SecurityClearanceLevel => _securityClearanceLevel;

    public Janitor(
        int departmentId,
        Gender gender,
        DateTime shiftStart,
        DateTime shiftEnd,
        PhoneNumber phoneNumber,
        EmailAddress emailAddress,
        string assignedZone,
        bool biohazardCertified,
        string securityClearanceLevel) : 
        base(departmentId, gender, shiftStart, shiftEnd, phoneNumber, emailAddress)
    {
        UpdateAssignedZone(assignedZone);
        SetBiohazardCertificate(biohazardCertified);
        UpdateSecurityClearanceLevel(securityClearanceLevel);
    }

    protected Janitor() { }

    public void UpdateAssignedZone(string zone)
    {
        _assignedZone = zone.CheckTooLongOrEmpty(50);
    }

    public void SetBiohazardCertificate(bool certified)
    {
        BiohazardCertified = certified;
    }

    public void UpdateSecurityClearanceLevel(string clearanceLevel)
    {
        _securityClearanceLevel = clearanceLevel.CheckTooLongOrEmpty(50);
    }
}