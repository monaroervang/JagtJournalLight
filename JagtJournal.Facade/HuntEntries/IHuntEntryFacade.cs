using JagtJournal.Application.Dtos.HuntEntries;

namespace JagtJournal.Facade.HuntEntries;

public interface IHuntEntryFacade
{
    Task<Guid> CreateAsync(
        CreateHuntEntryDto dto,
        CancellationToken cancellationToken = default);

    Task<HuntEntryDetailsDto?> GetDetailsAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<HuntEntryListItemDto>> GetLatestByUsernameAsync(
        string username,
        int maxCount = 10,
        CancellationToken cancellationToken = default);
}