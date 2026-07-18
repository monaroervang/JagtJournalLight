using JagtJournal.Application.Dtos.HuntEntries;
using JagtJournal.Application.Services;

namespace JagtJournal.Facade.HuntEntries;

public class HuntEntryFacade : IHuntEntryFacade
{
    private readonly IHuntEntryService _huntEntryService;

    public HuntEntryFacade(IHuntEntryService huntEntryService)
    {
        _huntEntryService = huntEntryService;
    }

    public Task<Guid> CreateAsync(
        CreateHuntEntryDto dto,
        CancellationToken cancellationToken = default)
    {
        return _huntEntryService.CreateAsync(dto, cancellationToken);
    }

    public Task<HuntEntryDetailsDto?> GetDetailsAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return _huntEntryService.GetDetailsAsync(id, cancellationToken);
    }

    public Task<IReadOnlyList<HuntEntryListItemDto>> GetLatestByUsernameAsync(
        string username,
        int maxCount = 10,
        CancellationToken cancellationToken = default)
    {
        return _huntEntryService.GetLatestByUsernameAsync(
            username,
            maxCount,
            cancellationToken);
    }
}