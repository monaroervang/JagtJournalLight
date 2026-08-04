using JagtJournal.Application.Interfaces;
using JagtJournal.Domain.Entities;
using JagtJournal.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace JagtJournal.Infrastructure.Repositories;

/// <summary>
/// EF Core-implementation af repository til jagtområder.
/// </summary>
public class EfHuntingAreaRepository : IHuntingAreaRepository
{
    private readonly IDbContextFactory<JagtJournalDbContext> _dbContextFactory;

    public EfHuntingAreaRepository(
        IDbContextFactory<JagtJournalDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    /// <summary>
    /// Henter alle jagtområder fra databasen og sorterer dem efter navn.
    /// </summary>
    public async Task<IReadOnlyList<HuntingArea>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        await using var dbContext =
            await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        return await dbContext.HuntingAreas
            .AsNoTracking()
            .OrderBy(huntingArea => huntingArea.Name)
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Gemmer et nyt jagtområde i databasen.
    /// </summary>
    public async Task AddAsync(
        HuntingArea huntingArea,
        CancellationToken cancellationToken = default)
    {
        await using var dbContext =
            await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        dbContext.HuntingAreas.Add(huntingArea);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
