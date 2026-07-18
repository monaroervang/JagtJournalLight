using JagtJournal.Application.Dtos.HuntEntries;

namespace JagtJournal.Application.Services;

public interface IHuntEntryService
{
    // Opretter en ny jagtjournal-post
    Task<Guid> CreateAsync(CreateHuntEntryDto dto, CancellationToken cancellationToken = default);

    // Henter detaljer for én jagtjournal-post
    Task<HuntEntryDetailsDto?> GetDetailsAsync(Guid id, CancellationToken cancellationToken = default);

    // Henter de seneste jagtjournal-poster for et brugernavn
    Task<IReadOnlyList<HuntEntryListItemDto>> GetLatestByUsernameAsync(
        string username,
        int maxCount = 10,
        CancellationToken cancellationToken = default);
}