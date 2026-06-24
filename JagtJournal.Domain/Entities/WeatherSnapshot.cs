namespace JagtJournal.Domain.Entities;

public class WeatherSnapshot
{
    public Guid Id { get; private set; }
    public Guid HuntEntryId { get; private set; }
    public decimal Temperature { get; private set; }
    public string WindDirection { get; private set; }
    public decimal WindSpeedMs { get; private set; }
    public string WeatherDescription { get; private set; }

    // Bruges af EF Core
    private WeatherSnapshot()
    {
        WindDirection = null!;
        WeatherDescription = null!;
    }

    // Bruges når vi gemmer vejrdata for en jagt
    public WeatherSnapshot(
        Guid huntEntryId,
        decimal temperature,
        string windDirection,
        decimal windSpeedMs,
        string weatherDescription)
    {
        if (huntEntryId == Guid.Empty)
            throw new ArgumentException("HuntEntryId må ikke være tom.");

        if (string.IsNullOrWhiteSpace(windDirection))
            throw new ArgumentException("Vindretning skal udfyldes.");

        if (windSpeedMs < 0)
            throw new ArgumentException("Vindhastighed kan ikke være negativ.");

        if (string.IsNullOrWhiteSpace(weatherDescription))
            throw new ArgumentException("Vejrbeskrivelse skal udfyldes.");

        Id = Guid.NewGuid();
        HuntEntryId = huntEntryId;
        Temperature = temperature;
        WindDirection = windDirection;
        WindSpeedMs = windSpeedMs;
        WeatherDescription = weatherDescription;
    }
}