using JagtJournal.Application.Interfaces;
using JagtJournal.Domain.Entities;
using JagtJournal.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace JagtJournal.Infrastructure.Repositories;

public class EfUserRepository : IUserRepository
{
    private readonly IDbContextFactory<JagtJournalDbContext> _contextFactory;

    // Repository får en DbContextFactory, så hver operation får sin egen DbContext
    public EfUserRepository(IDbContextFactory<JagtJournalDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    // Henter en bruger ud fra brugernavn
    public async Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);

        return await context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Username == username.Trim(), cancellationToken);
    }

    // Gemmer en ny bruger i databasen
    public async Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);

        await context.Users.AddAsync(user, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}