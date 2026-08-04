using JagtJournal.Application.Dtos.HuntingAreas;

namespace JagtJournal.Application.Interfaces;

/// <summary>
/// Beskriver de use cases, som Application-laget
/// understøtter for jagtområder.
/// </summary>
public interface IHuntingAreaService
{
    /// <summary>
    /// Henter alle jagtområder.
    /// </summary>
    Task<IReadOnlyList<HuntingAreaListItemDto>> GetAllAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Opretter et nyt jagtområde.
    /// </summary>
    Task<Guid> CreateAsync(
        CreateHuntingAreaDto dto,
        CancellationToken cancellationToken = default);
}