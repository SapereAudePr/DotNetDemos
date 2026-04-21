using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities.TPT;

public class Hospital : AuditableEntity
{
    private string _address = null!;
    public string Address => _address;

    private readonly List<Department> _departments = [];
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

    protected Hospital()
    {
    }

    public void UpdateAddress(string address)
    {
        if (_address.Equals(address))
            throw new InvalidOperationException($"Same {address} already exists");
        
        _address = address.CheckTooLongOrEmpty(256);
    }

    public void UpdatePhoneNumber(PhoneNumber phoneNumber)
    {
        if (_mainPhoneNumber is not null && _mainPhoneNumber.Equals(phoneNumber))
            throw new InvalidOperationException($"Same {phoneNumber} already exists");
        
        _mainPhoneNumber = phoneNumber.CheckNull();
    }

    public void UpdateEmailAddress(EmailAddress emailAddress)
    {
        if (_mainEmailAddress is not null && _mainEmailAddress.Equals(emailAddress))
            throw new InvalidOperationException($"Same {emailAddress} already exists");
        
        _mainEmailAddress = emailAddress.CheckNull();
    }

    public void UpdateBuiltDate(DateTimeOffset builtDate)
    {
        builtDate.CheckCreationDateTimeOffset();

        _builtDate = builtDate;
    }
}