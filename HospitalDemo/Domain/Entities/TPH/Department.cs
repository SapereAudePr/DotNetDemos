using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities.TPH;

public class Department : AuditableEntity
{
    public Hospital Hospital { get; private set; } = null!;
    public int HospitalId { get; private set; }

    private readonly List<PhoneNumber> _phoneNumbers = new();
    private readonly List<EmailAddress> _emailAddresses = new();
    private readonly HashSet<Personnel> _personnel = new();

    public IReadOnlyCollection<PhoneNumber> PhoneNumbers => _phoneNumbers;
    public IReadOnlyCollection<EmailAddress> EmailAddresses => _emailAddresses;
    public IReadOnlyCollection<Personnel> Personnel => _personnel;

    private Department() { }

    public Department(
        int hospitalId,
        IEnumerable<PhoneNumber> phoneNumbers,
        IEnumerable<EmailAddress> emailAddresses,
        IEnumerable<Personnel> personnel)
    {
        HospitalId = hospitalId;

        foreach (var phone in phoneNumbers)
        {
            _phoneNumbers.Add(phone);
        }
        foreach (var email in emailAddresses)
        {
            _emailAddresses.Add(email);
        }
        foreach (var p in personnel)
        {
            _personnel.Add(p);
        }
    }

    public void AddPhoneNumber(string number, string label)
    {
        number = number.CheckNullOrWhiteSpace(trimValue: true);
        label.CheckTooLongOrEmpty(120);

        if (_phoneNumbers.Any(x => x.Number == number))
            throw new ArgumentException("Same number exists");

        _phoneNumbers.Add(new PhoneNumber(number, label));
    }

    public void RemovePhoneNumber(string number)
    {
        number = number.CheckNullOrWhiteSpace(trimValue: true);

        var phoneNumber = _phoneNumbers.FirstOrDefault(x => x.Number == number);
        if (phoneNumber is not null)
            _phoneNumbers.Remove(phoneNumber);
    }

    public void AddEmailAddress(string email)
    {
        email = email.ValidateEmailRegex(normalize: true);

        if (_emailAddresses.Any(x => x.Value == email))
            throw new ArgumentException("Same email exists");

        _emailAddresses.Add(new EmailAddress(email));
    }

    public void RemoveEmailAddress(string email)
    {
        email = email.CheckNullOrWhiteSpace(trimValue: true);

        var mail = _emailAddresses.FirstOrDefault(x => x.Value == email);
        if (mail is not null)
            _emailAddresses.Remove(mail);
    }

    public void AddPersonnel(Personnel person)
    {
        _personnel.Add(person);
    }

    public void RemovePersonnel(Personnel person)
    {
        _personnel.Remove(person);
    }
}
