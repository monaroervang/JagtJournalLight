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
        // Arrange: Vi mocker repositories,
        // fordi vi kun vil teste Application Service
        var huntEntryRepositoryMock = new Mock<IHuntEntryRepository>();
        var userRepositoryMock = new Mock<IUserRepository>();

        var user = new User("Testbruger");

        userRepositoryMock
            .Setup(repository => repository.GetByUsernameAsync(
                "Testbruger",
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        HuntEntry? capturedHuntEntry = null;

        huntEntryRepositoryMock
            .Setup(repository => repository.AddAsync(
                It.IsAny<HuntEntry>(),
                It.IsAny<CancellationToken>()))
            .Callback<HuntEntry, CancellationToken>((huntEntry, _) =>
            {
                capturedHuntEntry = huntEntry;
            })
            .Returns(Task.CompletedTask);

        var service = new HuntEntryService(
            huntEntryRepositoryMock.Object,
            userRepositoryMock.Object);

        var dto = new CreateHuntEntryDto
        {
            Username = "Testbruger",
            HuntingAreaId = Guid.NewGuid(),
            Date = new DateOnly(2026, 1, 20),
            CartridgesUsed = 4
        };

        // Act
        var result = await service.CreateAsync(dto);

        // Assert
        huntEntryRepositoryMock.Verify(repository => repository.AddAsync(
            It.IsAny<HuntEntry>(),
            It.IsAny<CancellationToken>()),
            Times.Once);

        Assert.NotNull(capturedHuntEntry);
        Assert.Equal(capturedHuntEntry.Id, result);
        Assert.Equal(user.Id, capturedHuntEntry.UserId);
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
        var userRepositoryMock = new Mock<IUserRepository>();

        repositoryMock
            .Setup(repository => repository.GetByIdAsync(
                huntEntry.Id,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(huntEntry);

        var service = new HuntEntryService(
            repositoryMock.Object,
            userRepositoryMock.Object);

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
        var userRepositoryMock = new Mock<IUserRepository>();

        repositoryMock
            .Setup(repository => repository.GetByIdAsync(
                It.IsAny<Guid>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((HuntEntry?)null);

        var service = new HuntEntryService(
        repositoryMock.Object,
        userRepositoryMock.Object);

        // Act: Vi forsøger at hente en jagtpost, der ikke findes
        var result = await service.GetDetailsAsync(Guid.NewGuid());

        // Assert: Servicen skal returnere null
        Assert.Null(result);
    }
    [Fact]
    public async Task GetLatestByUsernameAsync_ShouldReturnListDtos_WhenUserHasHuntEntries()
    {
        // Arrange
        var username = "Testbruger";
        var user = new User(username);

        IReadOnlyList<HuntEntry> huntEntries = new List<HuntEntry>
    {
        new HuntEntry(
            user.Id,
            Guid.NewGuid(),
            new DateOnly(2026, 1, 20),
            3),

        new HuntEntry(
            user.Id,
            Guid.NewGuid(),
            new DateOnly(2026, 1, 21),
            5)
    };

        var repositoryMock = new Mock<IHuntEntryRepository>();
        var userRepositoryMock = new Mock<IUserRepository>();

        userRepositoryMock
            .Setup(repository => repository.GetByUsernameAsync(
                username,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        repositoryMock
            .Setup(repository => repository.GetByUserIdAsync(
                user.Id,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(huntEntries);

        var service = new HuntEntryService(
            repositoryMock.Object,
            userRepositoryMock.Object);

        // Act
        var result = await service.GetLatestByUsernameAsync(username);

        // Assert
        Assert.Equal(2, result.Count);

        Assert.Equal(huntEntries[0].Id, result[0].Id);
        Assert.Equal(huntEntries[0].Date, result[0].Date);
        Assert.Equal(huntEntries[0].CartridgesUsed, result[0].CartridgesUsed);

        Assert.Equal(huntEntries[1].Id, result[1].Id);
        Assert.Equal(huntEntries[1].Date, result[1].Date);
        Assert.Equal(huntEntries[1].CartridgesUsed, result[1].CartridgesUsed);
    }
}