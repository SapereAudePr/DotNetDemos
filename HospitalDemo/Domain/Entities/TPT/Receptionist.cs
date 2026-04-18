using Domain.Common;
using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Entities.TPT;

public class Receptionist : Personnel
{
    private readonly List<ReceptionistLanguage> _knownLanguages = new();
    public IReadOnlyCollection<ReceptionistLanguage> KnownLanguages => _knownLanguages;

    private string _deskLocation = null!;
    public string DeskLocation => _deskLocation;

    public bool HandlesInsuranceBilling { get; private set; }

    public Receptionist(
        int departmentId,
        Gender gender,
        DateTime shiftStart,
        DateTime shiftEnd,
        PhoneNumber phoneNumber,
        EmailAddress emailAddress,
        IEnumerable<ReceptionistLanguage> knownLanguages,
        string deskLocation,
        bool handlesInsuranceBilling) :
        base(departmentId, gender, shiftStart, shiftEnd, phoneNumber, emailAddress)
    {
        _knownLanguages.CheckNull();
        _knownLanguages = knownLanguages.ToList();
        ChangeDeskLocation(deskLocation);
        SetInsuranceBilling(handlesInsuranceBilling);
    }

    protected Receptionist()
    {
    }

    public void AddLanguage(string name, string proficiency)
    {
        if (_knownLanguages.Any(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationException("Language name already exists");

        _knownLanguages
            .Add(new ReceptionistLanguage(name, proficiency));
    }

    public void RemoveLanguage(string name)
    {
        var language = _knownLanguages.FirstOrDefault
            (x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

        if (language is null)
            throw new InvalidOperationException("Language not found");

        _knownLanguages.Remove(language);
    }

    public void ChangeDeskLocation(string location) => _deskLocation =
        location.CheckTooLongOrEmpty(50);

    public void SetInsuranceBilling(bool enabled) => HandlesInsuranceBilling = enabled;
}