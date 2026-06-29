using JagtJournal.Application.Dtos.HuntEntries;

namespace JagtJournal.Facade.HuntEntries;

public interface IHuntEntryFacade
{
    // Opretter en ny jagtjournal-post fra UI-laget
    Task<Guid> CreateAsync(CreateHuntEntryDto dto, CancellationToken cancellationToken = default);

    // Henter detaljer til visning i UI-laget
    Task<HuntEntryDetailsDto?> GetDetailsAsync(Guid id, CancellationToken cancellationToken = default);

    // Henter en brugers jagtjournal-poster til listevisning
    Task<IReadOnlyList<HuntEntryListItemDto>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}