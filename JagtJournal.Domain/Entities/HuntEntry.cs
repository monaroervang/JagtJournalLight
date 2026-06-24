namespace JagtJournal.Domain.Entities;

public class HuntEntry
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid HuntingAreaId { get; private set; }
    public DateOnly Date { get; private set; }
    public int CartridgesUsed { get; private set; }

    // Backing field, så listen kun kan ændres gennem domænets metoder
    private readonly List<HuntResult> _results = new();
    public IReadOnlyCollection<HuntResult> Results => _results;

    // Backing field, så jagtkammerater kun kan tilføjes kontrolleret
    private readonly List<HuntCompanion> _companions = new();
    public IReadOnlyCollection<HuntCompanion> Companions => _companions;

    public WeatherSnapshot? WeatherSnapshot { get; private set; }

    // Bruges af EF Core
    private HuntEntry()
    {
    }

    // Bruges når vi selv opretter en ny jagtjournal-post
    public HuntEntry(Guid userId, Guid huntingAreaId, DateOnly date, int cartridgesUsed)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("UserId må ikke være tom.");

        if (huntingAreaId == Guid.Empty)
            throw new ArgumentException("HuntingAreaId må ikke være tom.");

        if (cartridgesUsed < 0)
            throw new ArgumentException("Antal patroner kan ikke være negativt.");

        Id = Guid.NewGuid();
        UserId = userId;
        HuntingAreaId = huntingAreaId;
        Date = date;
        CartridgesUsed = cartridgesUsed;
    }

    // Tilføjer et resultat til jagten
    public void AddResult(Guid speciesId, int amount, string? note)
    {
        if (speciesId == Guid.Empty)
            throw new ArgumentException("SpeciesId må ikke være tom.");

        if (amount <= 0)
            throw new ArgumentException("Antal skal være større end 0.");

        _results.Add(new HuntResult(Id, speciesId, amount, note));
    }

    // Tilføjer en jagtkammerat, enten som registreret bruger eller manuelt navn
    public void AddCompanion(Guid? userId, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Navn skal udfyldes.");

        _companions.Add(new HuntCompanion(Id, userId, name));
    }

    // Tilføjer eller erstatter vejrdata på jagten
    public void SetWeatherSnapshot(
        decimal temperature,
        string windDirection,
        decimal windSpeedMs,
        string weatherDescription)
    {
        WeatherSnapshot = new WeatherSnapshot(
            Id,
            temperature,
            windDirection,
            windSpeedMs,
            weatherDescription
        );
    }
}