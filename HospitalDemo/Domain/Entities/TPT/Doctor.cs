using Domain.Common;

namespace Domain.Entities.TPT;

public class Doctor : Personnel
{
    private string _specialization = null!;
    public string Specialization => _specialization;


    private string _licenseNumber = null!;
    public string LicenseNumber => _licenseNumber;

    public Doctor(string specialization, string licenseNumber)
    {
        UpdateSpecialization(specialization);
        UpdateLicenseNumber(licenseNumber);
    }

    private Doctor() { }

    public void UpdateSpecialization(string specialization)
    {
        _specialization = specialization.CheckTooLongOrEmpty(50);
    }

    public void UpdateLicenseNumber(string licenseNumber)
    {
        _licenseNumber = licenseNumber.CheckTooLongOrEmpty(50);
    }
}