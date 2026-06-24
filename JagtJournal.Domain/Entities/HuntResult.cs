namespace JagtJournal.Domain.Entities;

public class HuntResult
{
    public Guid Id { get; private set; }
    public Guid HuntEntryId { get; private set; }
    public Guid SpeciesId { get; private set; }
    public int Amount { get; private set; }
    public string? Note { get; private set; }

    // Bruges af EF Core
    private HuntResult()
    {
    }

    // Bruges når vi tilføjer et resultat til en jagt
    public HuntResult(Guid huntEntryId, Guid speciesId, int amount, string? note)
    {
        if (huntEntryId == Guid.Empty)
            throw new ArgumentException("HuntEntryId må ikke være tom.");

        if (speciesId == Guid.Empty)
            throw new ArgumentException("SpeciesId må ikke være tom.");

        if (amount <= 0)
            throw new ArgumentException("Antal skal være større end 0.");

        Id = Guid.NewGuid();
        HuntEntryId = huntEntryId;
        SpeciesId = speciesId;
        Amount = amount;
        Note = note;
    }
}