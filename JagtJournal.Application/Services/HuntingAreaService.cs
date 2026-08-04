using JagtJournal.Application.Dtos.HuntingAreas;
using JagtJournal.Application.Interfaces;
using JagtJournal.Domain.Entities;

namespace JagtJournal.Application.Services;

/// <summary>
/// Application Service for jagtområder.
///
/// Servicen orkestrerer use cases og mapper domæneobjekter
/// til DTO'er. Den indeholder ikke databasekode.
/// </summary>
public class HuntingAreaService : IHuntingAreaService
{
    private readonly IHuntingAreaRepository _huntingAreaRepository;

    // Repository injiceres via Dependency Injection.
    public HuntingAreaService(
        IHuntingAreaRepository huntingAreaRepository)
    {
        _huntingAreaRepository = huntingAreaRepository;
    }

    /// <summary>
    /// Henter alle jagtområder og mapper dem til DTO'er.
    /// </summary>
    public async Task<IReadOnlyList<HuntingAreaListItemDto>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        var huntingAreas =
            await _huntingAreaRepository.GetAllAsync(cancellationToken);

        return huntingAreas
            .Select(huntingArea => new HuntingAreaListItemDto
            {
                Id = huntingArea.Id,
                Name = huntingArea.Name,
                Description = huntingArea.Description
            })
            .ToList();
    }

    /// <summary>
    /// Opretter et nyt jagtområde.
    /// </summary>
    public async Task<Guid> CreateAsync(
        CreateHuntingAreaDto dto,
        CancellationToken cancellationToken = default)
    {
        var huntingArea = new HuntingArea(
            dto.Name,
            dto.Description);

        await _huntingAreaRepository.AddAsync(
            huntingArea,
            cancellationToken);

        return huntingArea.Id;
    }
}