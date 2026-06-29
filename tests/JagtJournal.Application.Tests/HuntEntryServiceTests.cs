using JagtJournal.Application.Dtos.HuntEntries;
using JagtJournal.Application.Interfaces;
using JagtJournal.Application.Services;
using JagtJournal.Domain.Entities;
using Moq;

namespace JagtJournal.Application.Tests;

public class HuntEntryServiceTests
{
    [Fact]
    public async Task CreateAsync_ShouldCreateHuntEntryAndCallRepository_WhenDtoIsValid()
    {
        // Arrange: Vi mocker repository, fordi vi kun vil teste Application Service
        var repositoryMock = new Mock<IHuntEntryRepository>();

        HuntEntry? capturedHuntEntry = null;

        // Vi gemmer den HuntEntry, som servicen sender videre til repository
        repositoryMock
            .Setup(repository => repository.AddAsync(
                It.IsAny<HuntEntry>(),
                It.IsAny<CancellationToken>()))
            .Callback<HuntEntry, CancellationToken>((huntEntry, _) =>
            {
                capturedHuntEntry = huntEntry;
            })
            .Returns(Task.CompletedTask);

        var service = new HuntEntryService(repositoryMock.Object);

        var dto = new CreateHuntEntryDto
        {
            UserId = Guid.NewGuid(),
            HuntingAreaId = Guid.NewGuid(),
            Date = new DateOnly(2026, 1, 20),
            CartridgesUsed = 4
        };

        // Act: Vi kalder servicen
        var result = await service.CreateAsync(dto);

        // Assert: Repository skal kaldes præcis én gang
        repositoryMock.Verify(repository => repository.AddAsync(
            It.IsAny<HuntEntry>(),
            It.IsAny<CancellationToken>()),
            Times.Once);

        // Assert: DTO'en skal være omsat korrekt til en domain entity
        Assert.NotNull(capturedHuntEntry);
        Assert.Equal(capturedHuntEntry.Id, result);
        Assert.Equal(dto.UserId, capturedHuntEntry.UserId);
        Assert.Equal(dto.HuntingAreaId, capturedHuntEntry.HuntingAreaId);
        Assert.Equal(dto.Date, capturedHuntEntry.Date);
        Assert.Equal(dto.CartridgesUsed, capturedHuntEntry.CartridgesUsed);
    }

    [Fact]
    public async Task GetDetailsAsync_ShouldReturnDto_WhenHuntEntryExists()
    {
        // Arrange: Repository returnerer en eksisterende HuntEntry
        var huntEntry = new HuntEntry(
            Guid.NewGuid(),
            Guid.NewGuid(),
            new DateOnly(2026, 1, 20),
            3);

        var repositoryMock = new Mock<IHuntEntryRepository>();

        repositoryMock
            .Setup(repository => repository.GetByIdAsync(
                huntEntry.Id,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(huntEntry);

        var service = new HuntEntryService(repositoryMock.Object);

        // Act: Vi henter detaljer via servicen
        var result = await service.GetDetailsAsync(huntEntry.Id);

        // Assert: Domæneobjektet skal være mappet til en DTO
        Assert.NotNull(result);
        Assert.Equal(huntEntry.Id, result.Id);
        Assert.Equal(huntEntry.UserId, result.UserId);
        Assert.Equal(huntEntry.HuntingAreaId, result.HuntingAreaId);
        Assert.Equal(huntEntry.Date, result.Date);
        Assert.Equal(huntEntry.CartridgesUsed, result.CartridgesUsed);
    }

    [Fact]
    public async Task GetDetailsAsync_ShouldReturnNull_WhenHuntEntryDoesNotExist()
    {
        // Arrange: Repository returnerer null, fordi jagtposten ikke findes
        var repositoryMock = new Mock<IHuntEntryRepository>();

        repositoryMock
            .Setup(repository => repository.GetByIdAsync(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((HuntEntry?)null);

        var service = new HuntEntryService(repositoryMock.Object);

        // Act: Vi forsøger at hente en jagtpost, der ikke findes
        var result = await service.GetDetailsAsync(Guid.NewGuid());

        // Assert: Servicen skal returnere null
        Assert.Null(result);
    }
    [Fact]
    public async Task GetByUserIdAsync_ShouldReturnListDtos_WhenUserHasHuntEntries()
    {
        // Arrange: Repository returnerer to jagtposter for samme bruger
        var userId = Guid.NewGuid();

        IReadOnlyList<HuntEntry> huntEntries = new List<HuntEntry>
    {
        new HuntEntry(userId, Guid.NewGuid(), new DateOnly(2026, 1, 20), 3),
        new HuntEntry(userId, Guid.NewGuid(), new DateOnly(2026, 1, 21), 5)
    };

        var repositoryMock = new Mock<IHuntEntryRepository>();

        repositoryMock
            .Setup(repository => repository.GetByUserIdAsync(
                userId,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(huntEntries);

        var service = new HuntEntryService(repositoryMock.Object);

        // Act: Vi henter brugerens jagtposter gennem servicen
        var result = await service.GetByUserIdAsync(userId);

        // Assert: Resultatet skal være mappet til liste-DTO'er
        Assert.Equal(2, result.Count);

        Assert.Equal(huntEntries[0].Id, result[0].Id);
        Assert.Equal(huntEntries[0].Date, result[0].Date);
        Assert.Equal(huntEntries[0].CartridgesUsed, result[0].CartridgesUsed);

        Assert.Equal(huntEntries[1].Id, result[1].Id);
        Assert.Equal(huntEntries[1].Date, result[1].Date);
        Assert.Equal(huntEntries[1].CartridgesUsed, result[1].CartridgesUsed);
    }
}