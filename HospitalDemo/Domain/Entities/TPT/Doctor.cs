using Domain.Common;
using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Entities.TPT;

public class Doctor : Personnel
{
    private string _specialization = null!;
    public string Specialization => _specialization;


    private string _licenseNumber = null!;
    public string LicenseNumber => _licenseNumber;

    public Doctor(
        int departmentId,
        Gender gender,
        DateTime shiftStart,
        DateTime shiftEnd,
        PhoneNumber phoneNumber,
        EmailAddress emailAddress,
        string specialization,
        string licenseNumber) : 
        base (departmentId, gender, shiftStart, shiftEnd, phoneNumber, emailAddress)
    {
        UpdateSpecialization(specialization);
        UpdateLicenseNumber(licenseNumber);
    }

    protected Doctor() { }

    public void UpdateSpecialization(string specialization)
    {
        _specialization = specialization.CheckTooLongOrEmpty(50);
    }

    public void UpdateLicenseNumber(string licenseNumber)
    {
        _licenseNumber = licenseNumber.CheckTooLongOrEmpty(50);
    }
}