using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities.TPT;

public class Hospital : AuditableEntity
{
    private string _address = null!;
    public string Address => _address;

    private readonly HashSet<Department> _departments = new();
    public IReadOnlyCollection<Department> Departments => _departments;

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

    private Hospital()
    {
    }

    public void AddDepartment(Department department)
    {
        _departments.Add(department.CheckNull());
    }

    public void RemoveDepartment(Department department)
    {
        _departments.Remove(department);
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