namespace Domain.ValueObjects;

public class ReceptionistLanguage
{
    public string Name { get; } = null!;
    public string Proficiency { get; } = null!;

    private ReceptionistLanguage() { }

    public ReceptionistLanguage(string name, string proficiency)
    {
        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(proficiency))
            throw new ArgumentNullException("Name and Proficiency can't be null");
        if (name.Length > 50 || proficiency.Length > 50)
            throw new ArgumentException("Name and Proficiency can't be longer than 50 characters");

        Name = name.Trim();
        Proficiency = proficiency.Trim();
    }
}