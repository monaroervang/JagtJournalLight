namespace JagtJournal.Domain.Entities;

public class Species
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Category { get; private set; }

    // Bruges af EF Core
    private Species()
    {
        Name = null!;
        Category = null!;
    }

    // Bruges når vi opretter en ny art
    public Species(string name, string category)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Navn skal udfyldes.");

        if (string.IsNullOrWhiteSpace(category))
            throw new ArgumentException("Kategori skal udfyldes.");

        Id = Guid.NewGuid();
        Name = name;
        Category = category;
    }
}