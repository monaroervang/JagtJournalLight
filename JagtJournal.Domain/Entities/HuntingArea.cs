namespace JagtJournal.Domain.Entities;

public class HuntingArea
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }

    // Bruges af EF Core
    private HuntingArea()
    {
        Name = null!;
    }

    // Bruges når vi selv opretter et jagtområde
    public HuntingArea(string name, string? description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Navn skal udfyldes.");

        Id = Guid.NewGuid();
        Name = name;
        Description = description;
    }
}