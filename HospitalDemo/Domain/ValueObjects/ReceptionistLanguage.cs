using Domain.Common;

namespace Domain.ValueObjects;

public record ReceptionistLanguage
{
    public string Name { get; } = null!;
    public string Proficiency { get; } = null!;

    private ReceptionistLanguage() { }

    public ReceptionistLanguage(string name, string proficiency)
    {
        name = name.CheckTooLongOrEmpty(50).TrimValue();
        proficiency = proficiency.CheckTooLongOrEmpty(50).TrimValue();

        Name = name;
        Proficiency = proficiency;
    }
}