namespace JagtJournal.Domain.Entities;

public class HuntingSeason
{
    public Guid Id { get; private set; }
    public Guid SpeciesId { get; private set; }
    public DateOnly StartDate { get; private set; }
    public DateOnly EndDate { get; private set; }
    public bool IsAllowed { get; private set; }

    // Bruges af EF Core
    private HuntingSeason()
    {
    }

    // Bruges når vi opretter en jagtsæson for en art
    public HuntingSeason(Guid speciesId, DateOnly startDate, DateOnly endDate, bool isAllowed)
    {
        if (speciesId == Guid.Empty)
            throw new ArgumentException("SpeciesId må ikke være tom.");

        if (endDate < startDate)
            throw new ArgumentException("Slutdato kan ikke være før startdato.");

        Id = Guid.NewGuid();
        SpeciesId = speciesId;
        StartDate = startDate;
        EndDate = endDate;
        IsAllowed = isAllowed;
    }
}