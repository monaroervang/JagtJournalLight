using JagtJournal.Application.Dtos.HuntEntries;
using JagtJournal.Application.Services;

namespace JagtJournal.Facade.HuntEntries;

public class HuntEntryFacade : IHuntEntryFacade
{
    private readonly IHuntEntryService _huntEntryService;

    // Facaden får Application Service gennem dependency injection
    public HuntEntryFacade(IHuntEntryService huntEntryService)
    {
        _huntEntryService = huntEntryService;
    }

    // Sender oprettelse videre til Application-laget
    public async Task<Guid> CreateAsync(
        CreateHuntEntryDto dto,
        CancellationToken cancellationToken = default)
    {
        return await _huntEntryService.CreateAsync(dto, cancellationToken);
    }

    // Sender detaljeforespørgsel videre til Application-laget
    public async Task<HuntEntryDetailsDto?> GetDetailsAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _huntEntryService.GetDetailsAsync(id, cancellationToken);
    }

    // Sender listeforespørgsel videre til Application-laget
    public async Task<IReadOnlyList<HuntEntryListItemDto>> GetByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await _huntEntryService.GetByUserIdAsync(userId, cancellationToken);
    }
}