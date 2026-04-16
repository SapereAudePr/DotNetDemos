using Domain.Common;

namespace Domain.ValueObjects;

public record ReceptionistLanguage
{
    public string Name { get; } = null!;
    public string Proficiency { get; } = null!;

    private ReceptionistLanguage() { }

    public ReceptionistLanguage(string name, string proficiency)
    {
        name.CheckTooLongOrEmpty(50);
        proficiency.CheckTooLongOrEmpty(50);
        
        Name = name.Trim();
        Proficiency = proficiency.Trim();
    }
}