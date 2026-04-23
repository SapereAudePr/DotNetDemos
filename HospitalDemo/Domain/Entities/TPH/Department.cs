using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities.TPH;

public class Department : AuditableEntity
{
    public Hospital Hospital { get; private set; } = null!;
    public int HospitalId { get; private set; }

    private readonly List<PhoneNumber> _phoneNumbers = [];
    private readonly List<EmailAddress> _emailAddresses = [];
    private readonly List<Personnel> _personnel = [];

    public IReadOnlyCollection<PhoneNumber> PhoneNumbers => _phoneNumbers;
    public IReadOnlyCollection<EmailAddress> EmailAddresses => _emailAddresses;
    public IReadOnlyCollection<Personnel> Personnel => _personnel;

    protected Department()
    {
    }

    public Department(
        int hospitalId,
        IEnumerable<PhoneNumber> phoneNumbers,
        IEnumerable<EmailAddress> emailAddresses)
    {
        HospitalId = hospitalId;

        foreach (var phone in phoneNumbers)
        {
            AddPhoneNumber(phone);
        }

        foreach (var email in emailAddresses)
        {
            AddEmailAddress(email);
        }
    }

    public void AddPhoneNumber(PhoneNumber phoneNumber)
    {
        phoneNumber.Number.CheckTooLongOrEmpty(20);
        phoneNumber.Label.CheckTooLongOrEmpty(120);

        if (_phoneNumbers.Any(x => x.Number == phoneNumber.Number))
            throw new ArgumentException("Same number exists");

        _phoneNumbers.Add(phoneNumber);
    }
    
    public void RemovePhoneNumber(PhoneNumber phoneNumber)
    {
        phoneNumber.Number.CheckNullOrEmpty();

        var found = _phoneNumbers.FirstOrDefault(x => x.Number == phoneNumber.Number);
        if (found is null)
            throw new ArgumentException("Phone number not found");

        _phoneNumbers.Remove(found);
    }

    public void AddEmailAddress(EmailAddress email)
    {
        email.Value.ValidateEmailRegex(normalize: true);

        if (_emailAddresses.Any(x => x.Value == email.Value))
            throw new ArgumentException("Same email exists");

        _emailAddresses.Add(email);
    }

    public void RemoveEmailAddress(EmailAddress email)
    {
        email.Value.CheckNullOrEmpty();

        var found = _emailAddresses.FirstOrDefault(x => x.Value == email.Value);
        if (found is null)
            throw new ArgumentException("Email address not found");

        _emailAddresses.Remove(found);
    }
}