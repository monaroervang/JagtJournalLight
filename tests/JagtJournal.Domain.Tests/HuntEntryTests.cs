using JagtJournal.Domain.Entities;

namespace JagtJournal.Domain.Tests;

public class HuntEntryTests
{
    [Fact]
    public void Constructor_ShouldCreateHuntEntryWithCorrectInitialState_WhenDataIsValid()
    {
        // Arrange: Vi forbereder gyldige testdata
        var userId = Guid.NewGuid();
        var huntingAreaId = Guid.NewGuid();
        var date = new DateOnly(2026, 1, 20);

        // Act: Vi opretter en ny HuntEntry
        var huntEntry = new HuntEntry(userId, huntingAreaId, date, 5);

        // Assert: Jagtposten skal være oprettet med gyldig starttilstand
        Assert.NotEqual(Guid.Empty, huntEntry.Id);
        Assert.Equal(userId, huntEntry.UserId);
        Assert.Equal(huntingAreaId, huntEntry.HuntingAreaId);
        Assert.Equal(date, huntEntry.Date);
        Assert.Equal(5, huntEntry.CartridgesUsed);

        Assert.Empty(huntEntry.Results);
        Assert.Empty(huntEntry.Companions);
        Assert.Null(huntEntry.WeatherSnapshot);
    }

    [Fact]
    public void Constructor_ShouldThrowException_WhenCartridgesUsedIsNegative()
    {
        // Arrange: Vi forbereder testdata med et ugyldigt antal patroner
        var userId = Guid.NewGuid();
        var huntingAreaId = Guid.NewGuid();
        var date = new DateOnly(2026, 1, 20);

        // Act + Assert: Domænet skal afvise negative patroner
        Assert.Throws<ArgumentException>(() =>
            new HuntEntry(userId, huntingAreaId, date, -1));
    }

    [Fact]
    public void AddResult_ShouldAddResult_WhenDataIsValid()
    {
        // Arrange: Vi opretter en gyldig jagtjournal-post
        var huntEntry = new HuntEntry(
            Guid.NewGuid(),
            Guid.NewGuid(),
            new DateOnly(2026, 1, 20),
            5);

        // Act: Vi tilføjer et jagtresultat
        huntEntry.AddResult(Guid.NewGuid(), 2, "To fasaner");

        // Assert: Der skal nu være ét resultat på jagten
        Assert.Single(huntEntry.Results);
    }

    [Fact]
    public void AddResult_ShouldThrowException_WhenAmountIsZero()
    {
        // Arrange: Vi opretter en gyldig jagtjournal-post
        var huntEntry = new HuntEntry(
            Guid.NewGuid(),
            Guid.NewGuid(),
            new DateOnly(2026, 1, 20),
            5);

        // Act + Assert: Domænet skal afvise resultater med antal 0
        Assert.Throws<ArgumentException>(() =>
            huntEntry.AddResult(Guid.NewGuid(), 0, "Ugyldigt antal"));
    }

    [Fact]
    public void AddCompanion_ShouldAddCompanion_WhenNameIsValid()
    {
        // Arrange: Vi opretter en gyldig jagtjournal-post
        var huntEntry = new HuntEntry(
            Guid.NewGuid(),
            Guid.NewGuid(),
            new DateOnly(2026, 1, 20),
            5);

        // Act: Vi tilføjer en jagtkammerat med manuelt navn
        huntEntry.AddCompanion(null, "Kaj");

        // Assert: Der skal nu være én jagtkammerat på jagten
        Assert.Single(huntEntry.Companions);
    }

    [Fact]
    public void AddCompanion_ShouldThrowException_WhenNameIsEmpty()
    {
        // Arrange: Vi opretter en gyldig jagtjournal-post
        var huntEntry = new HuntEntry(
            Guid.NewGuid(),
            Guid.NewGuid(),
            new DateOnly(2026, 1, 20),
            5);

        // Act + Assert: Domænet skal afvise tomme navne
        Assert.Throws<ArgumentException>(() =>
            huntEntry.AddCompanion(null, ""));
    }

    [Fact]
    public void SetWeatherSnapshot_ShouldSetWeatherSnapshot_WhenDataIsValid()
    {
        // Arrange: Vi opretter en gyldig jagtjournal-post
        var huntEntry = new HuntEntry(
            Guid.NewGuid(),
            Guid.NewGuid(),
            new DateOnly(2026, 1, 20),
            5);

        // Act: Vi tilføjer vejrdata til jagten
        huntEntry.SetWeatherSnapshot(
            temperature: 6.5m,
            windDirection: "Vest",
            windSpeedMs: 4.2m,
            weatherDescription: "Overskyet");

        // Assert: Jagtposten skal nu have et vejr-snapshot
        Assert.NotNull(huntEntry.WeatherSnapshot);
        Assert.Equal("Vest", huntEntry.WeatherSnapshot.WindDirection);
        Assert.Equal(4.2m, huntEntry.WeatherSnapshot.WindSpeedMs);
        Assert.Equal("Overskyet", huntEntry.WeatherSnapshot.WeatherDescription);
    }

    [Fact]
    public void SetWeatherSnapshot_ShouldThrowException_WhenWindSpeedIsNegative()
    {
        // Arrange: Vi opretter en gyldig jagtjournal-post
        var huntEntry = new HuntEntry(
            Guid.NewGuid(),
            Guid.NewGuid(),
            new DateOnly(2026, 1, 20),
            5);

        // Act + Assert: Domænet skal afvise negativ vindhastighed
        Assert.Throws<ArgumentException>(() =>
            huntEntry.SetWeatherSnapshot(
                temperature: 6.5m,
                windDirection: "Vest",
                windSpeedMs: -1,
                weatherDescription: "Overskyet"));
    }
}