using Domain.Common;
using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Entities.TPH;

public class Technician : Personnel
{
    private string _technicalCategory = null!;
    public string TechnicalCategory => _technicalCategory;


    private string _equipmentSpecialty = null!;
    public string EquipmentSpecialty => _equipmentSpecialty;


    private string _certificationNumber = null!;
    public string CertificationNumber => _certificationNumber;

    public Technician(
        int departmentId,
        Gender gender,
        DateTime shiftStart,
        DateTime shiftEnd,
        PhoneNumber phoneNumber,
        EmailAddress emailAddress,
        string technicalCategory,
        string equipmentSpecialty,
        string certificationNumber) :  
        base(departmentId, gender, shiftStart, shiftEnd, phoneNumber, emailAddress)
    {
        SetTechnicalCategory(technicalCategory);
        SetEquipmentSpecialty(equipmentSpecialty);
        SetCertificationNumber(certificationNumber);
    }

    protected Technician() { }

    public void SetTechnicalCategory(string category)
    {
        _technicalCategory = category.CheckTooLongOrEmpty(30);
    }

    public void SetEquipmentSpecialty(string specialty)
    {
        _equipmentSpecialty = specialty.CheckTooLongOrEmpty(30);
    }

    public void SetCertificationNumber(string number)
    {
        _certificationNumber = number.CheckTooLongOrEmpty(80);
    }
}