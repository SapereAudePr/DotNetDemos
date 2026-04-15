using Domain.Common;

namespace Domain.Entities.TPT;

public class Technician : Personnel
{
    private string _technicalCategory = null!;
    public string TechnicalCategory => _technicalCategory;


    private string _equipmentSpecialty = null!;
    public string EquipmentSpecialty => _equipmentSpecialty;


    private string _certificationNumber = null!;
    public string CertificationNumber => _certificationNumber;

    public Technician(
        string technicalCategory,
        string equipmentSpecialty,
        string certificationNumber)
    {
        SetTechnicalCategory(technicalCategory);
        SetEquipmentSpecialty(equipmentSpecialty);
        SetCertificationNumber(certificationNumber);
    }

    private Technician() { }

    public void SetTechnicalCategory(string category)
    {
        _technicalCategory = _technicalCategory.CheckTooLongOrEmpty(30);
    }

    public void SetEquipmentSpecialty(string specialty)
    {
        _equipmentSpecialty = _equipmentSpecialty.CheckTooLongOrEmpty(30);
    }

    public void SetCertificationNumber(string number)
    {
        _certificationNumber = _certificationNumber.CheckTooLongOrEmpty(80);
    }
}