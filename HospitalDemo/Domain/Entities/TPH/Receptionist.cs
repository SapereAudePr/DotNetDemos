using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities.TPH;

public class Receptionist : Personnel
{
    private readonly List<ReceptionistLanguage> _knownLanguages = new();
    public IReadOnlyCollection<ReceptionistLanguage> KnownLanguages => _knownLanguages;

    private string _deskLocation = null!;
    public string DeskLocation => _deskLocation;

    public bool HandlesInsuranceBilling { get; private set; }
    
    //TODO: Update constructor and add base(Personnel) parameters and call base with that parameters for each
    //                                                              derived classes
    
    public Receptionist(
        IEnumerable<ReceptionistLanguage> knownLanguages,
        string deskLocation,
        bool handlesInsuranceBilling)
    {
        var list = knownLanguages.CheckNull().ToList();

        foreach (var language in list)
        {
            _knownLanguages.Add(language);
        }
        
        ChangeDeskLocation(deskLocation);
        HandlesInsuranceBilling = handlesInsuranceBilling;
    }

    private Receptionist() { }

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
        location.CheckNullOrWhiteSpace();

    public void SetInsuranceBilling(bool enabled) => HandlesInsuranceBilling = enabled;
}