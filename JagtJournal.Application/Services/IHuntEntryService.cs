using JagtJournal.Application.Dtos.HuntEntries;

namespace JagtJournal.Application.Services;

public interface IHuntEntryService
{
    // Opretter en ny jagtjournal-post
    Task<Guid> CreateAsync(CreateHuntEntryDto dto, CancellationToken cancellationToken = default);

    // Henter detaljer for én jagtjournal-post
    Task<HuntEntryDetailsDto?> GetDetailsAsync(Guid id, CancellationToken cancellationToken = default);

    // Henter en simpel liste over en brugers jagtjournal-poster
    Task<IReadOnlyList<HuntEntryListItemDto>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}