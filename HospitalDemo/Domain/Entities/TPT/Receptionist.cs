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
        var list = knownLanguages.CheckNull().ToList();

        foreach (var language in list)
        {
            AddLanguage(language);
        }
        
        ChangeDeskLocation(deskLocation);
        SetInsuranceBilling(handlesInsuranceBilling);
    }

    protected Receptionist()
    {
    }

    public void AddLanguage(ReceptionistLanguage language)
    {
        language.CheckNull();

        if (_knownLanguages.Any(x => x.Name.Equals(language.Name, StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationException("Language name already exists");

        _knownLanguages
            .Add(language);
    }

    public void RemoveLanguage(ReceptionistLanguage language)
    {
        language.CheckNull();

        if(_knownLanguages.Remove(language))
            throw new InvalidOperationException("Language not found");
    }

    public void ChangeDeskLocation(string location) => _deskLocation =
        location.CheckTooLongOrEmpty(30);

    public void SetInsuranceBilling(bool enabled) => HandlesInsuranceBilling = enabled;
}