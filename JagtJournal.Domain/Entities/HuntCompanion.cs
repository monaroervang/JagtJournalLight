namespace JagtJournal.Domain.Entities;

public class HuntCompanion
{
    public Guid Id { get; private set; }
    public Guid HuntEntryId { get; private set; }
    public Guid? UserId { get; private set; }
    public string Name { get; private set; }

    // Bruges af EF Core
    private HuntCompanion()
    {
        Name = null!;
    }

    // Bruges når vi tilføjer en jagtkammerat til en jagt
    public HuntCompanion(Guid huntEntryId, Guid? userId, string name)
    {
        if (huntEntryId == Guid.Empty)
            throw new ArgumentException("HuntEntryId må ikke være tom.");

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Navn skal udfyldes.");

        Id = Guid.NewGuid();
        HuntEntryId = huntEntryId;
        UserId = userId;
        Name = name;
    }
}