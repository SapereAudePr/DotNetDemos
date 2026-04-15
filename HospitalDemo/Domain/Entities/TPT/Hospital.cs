using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities.TPT;

public class Hospital : AuditableEntity
{
    private string _address = null!;
    public string Address => _address;

    public ICollection<TPH.Department> Departments { get; set; } = new HashSet<TPH.Department>();


    private PhoneNumber _mainPhoneNumber = null!;
    public PhoneNumber MainPhoneNumber => _mainPhoneNumber;


    private EmailAddress _mainEmailAddress = null!;
    public EmailAddress MainEmailAddress => _mainEmailAddress;

    private DateTimeOffset _builtDate;

    public DateTimeOffset BuiltDate => _builtDate;

    public Hospital(
        string address,
        PhoneNumber mainPhoneNumber,
        EmailAddress mainEmailAddress,
        DateTimeOffset builtDate)
    {
        UpdateAddress(address);
        UpdatePhoneNumber(mainPhoneNumber);
        UpdateEmailAddress(mainEmailAddress);
        UpdateBuiltDate(builtDate);
    }

    private Hospital() { }

    public void AddDepartment(TPH.Department department)
    {
        Departments.Add(department);
    }

    public void RemoveDepartment(TPH.Department department)
    {
        Departments.Remove(department);
    }

    public void UpdateAddress(string address)
    {
        _address = address.CheckTooLongOrEmpty(256);
    }

    public void UpdatePhoneNumber(PhoneNumber phoneNumber)
    {
        _mainPhoneNumber = phoneNumber;
    }

    public void UpdateEmailAddress(EmailAddress emailAddress)
    {
        _mainEmailAddress = emailAddress;
    }

    public void UpdateBuiltDate(DateTimeOffset builtDate)
    {
        builtDate.CheckCreationDateTimeOffset();

        _builtDate = builtDate;
    }
}