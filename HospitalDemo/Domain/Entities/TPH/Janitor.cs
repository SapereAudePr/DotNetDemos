using Domain.Common;

namespace Domain.Entities.TPH;

public class Janitor : Personnel
{
    private string _assignedZone = null!;
    public string AssignedZone => _assignedZone;
    public bool BiohazardCertified { get; private set; }

    private string _securityClearanceLevel = null!;
    public string SecurityClearanceLevel => _securityClearanceLevel;

    public Janitor(
        string assignedZone,
        bool biohazardCertified,
        string securityClearanceLevel)
    {
        UpdateAssignedZone(assignedZone);
        EnableBiohazardCertificate(biohazardCertified);
        UpdateSecurityClearanceLevel(securityClearanceLevel);
    }

    private Janitor() { }

    public void UpdateAssignedZone(string zone)
    {
        _assignedZone = zone.CheckTooLongOrEmpty(50);
    }

    public void EnableBiohazardCertificate(bool enabled)
    {
        BiohazardCertified = enabled;
    }

    public void UpdateSecurityClearanceLevel(string clearanceLevel)
    {
        _securityClearanceLevel = clearanceLevel.CheckTooLongOrEmpty(50);
    }
}