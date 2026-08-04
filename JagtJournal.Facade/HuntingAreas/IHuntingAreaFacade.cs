using JagtJournal.Application.Dtos.HuntingAreas;

namespace JagtJournal.Facade.HuntingAreas;

/// <summary>
/// Facade for use cases vedrørende jagtområder.
///
/// Facaden er præsentationslagets indgang til Application-laget.
/// </summary>
public interface IHuntingAreaFacade
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