using Domain.Common;

namespace Domain.Entities.TPT;

public class Nurse : Personnel
{
    public bool IsHeadNurse { get; private set; }

    private string _certificationLevel = null!;
    public string CertificationLevel => _certificationLevel;

    private string _assignedWard = null!;
    public string AssignedWard => _assignedWard;

    private string _shiftType = null!;

    public string ShiftType => _shiftType;


    public Nurse(string certificationLevel,
        string assignedWard,
        string shiftType,
        bool isHeadNurse = false)
    {
        UpdateCertificationLevel(certificationLevel);
        UpdateAssignedWard(assignedWard);
        UpdateShiftType(shiftType);
        EnableHeadNurse(isHeadNurse);
    }

    private Nurse() { }


    public void EnableHeadNurse(bool enabled)
    {
        IsHeadNurse = enabled;
    }

    public void UpdateCertificationLevel(string certLevel)
    {
        _certificationLevel = _certificationLevel.CheckTooLongOrEmpty(30);
    }

    public void UpdateAssignedWard(string ward)
    {
        _assignedWard = _assignedWard.CheckTooLongOrEmpty(30);
    }

    public void UpdateShiftType(string shiftType)
    {
        _shiftType = _shiftType.CheckTooLongOrEmpty(30);
    }

}