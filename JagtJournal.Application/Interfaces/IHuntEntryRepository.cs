using JagtJournal.Domain.Entities;

namespace JagtJournal.Application.Interfaces;

public interface IHuntEntryRepository
{
    // Gemmer en ny jagtjournal-post
    Task AddAsync(HuntEntry huntEntry, CancellationToken cancellationToken = default);

    // Henter én jagtjournal-post ud fra id
    Task<HuntEntry?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    // Henter alle jagtjournal-poster for én bruger
    Task<IReadOnlyList<HuntEntry>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
}