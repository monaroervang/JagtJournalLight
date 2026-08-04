using JagtJournal.Application.Dtos.HuntingAreas;
using JagtJournal.Application.Interfaces;

namespace JagtJournal.Facade.HuntingAreas;

/// <summary>
/// Facade for jagtområder.
///
/// Facaden videresender kald fra præsentationslaget
/// til Application Service.
/// </summary>
public class HuntingAreaFacade : IHuntingAreaFacade
{
    private readonly IHuntingAreaService _huntingAreaService;

    // Application Service injiceres via Dependency Injection.
    public HuntingAreaFacade(
        IHuntingAreaService huntingAreaService)
    {
        _huntingAreaService = huntingAreaService;
    }

    /// <summary>
    /// Henter alle jagtområder.
    /// </summary>
    public async Task<IReadOnlyList<HuntingAreaListItemDto>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _huntingAreaService.GetAllAsync(cancellationToken);
    }

    /// <summary>
    /// Opretter et nyt jagtområde.
    /// </summary>
    public async Task<Guid> CreateAsync(
        CreateHuntingAreaDto dto,
        CancellationToken cancellationToken = default)
    {
        return await _huntingAreaService.CreateAsync(
            dto,
            cancellationToken);
    }
}