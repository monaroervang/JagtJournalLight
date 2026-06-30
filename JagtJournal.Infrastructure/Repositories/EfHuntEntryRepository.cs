using JagtJournal.Application.Interfaces;
using JagtJournal.Domain.Entities;
using JagtJournal.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace JagtJournal.Infrastructure.Repositories;

public class EfHuntEntryRepository : IHuntEntryRepository
{
    private readonly IDbContextFactory<JagtJournalDbContext> _contextFactory;

    // Repository får en DbContextFactory, så hver operation kan få sin egen DbContext
    public EfHuntEntryRepository(IDbContextFactory<JagtJournalDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    // Gemmer en ny jagtjournal-post i databasen
    public async Task AddAsync(HuntEntry huntEntry, CancellationToken cancellationToken = default)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);

        await context.HuntEntries.AddAsync(huntEntry, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    // Henter én jagtjournal-post med tilhørende resultater, jagtkammerater og vejrdata
    public async Task<HuntEntry?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);

        return await context.HuntEntries
            .Include(huntEntry => huntEntry.Results)
            .Include(huntEntry => huntEntry.Companions)
            .Include(huntEntry => huntEntry.WeatherSnapshot)
            .FirstOrDefaultAsync(huntEntry => huntEntry.Id == id, cancellationToken);
    }

    // Henter alle jagtjournal-poster for én bruger
    public async Task<IReadOnlyList<HuntEntry>> GetByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);

        return await context.HuntEntries
            .AsNoTracking()
            .Where(huntEntry => huntEntry.UserId == userId)
            .OrderByDescending(huntEntry => huntEntry.Date)
            .ToListAsync(cancellationToken);
    }
}