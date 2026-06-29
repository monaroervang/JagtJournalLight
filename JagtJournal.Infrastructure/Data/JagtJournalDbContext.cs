using JagtJournal.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JagtJournal.Infrastructure.Data;

public class JagtJournalDbContext : DbContext
{
    // DbContext får sine options gennem dependency injection
    public JagtJournalDbContext(DbContextOptions<JagtJournalDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<HuntingArea> HuntingAreas => Set<HuntingArea>();
    public DbSet<HuntEntry> HuntEntries => Set<HuntEntry>();
    public DbSet<HuntResult> HuntResults => Set<HuntResult>();
    public DbSet<HuntCompanion> HuntCompanions => Set<HuntCompanion>();
    public DbSet<Species> Species => Set<Species>();
    public DbSet<HuntingSeason> HuntingSeasons => Set<HuntingSeason>();
    public DbSet<WeatherSnapshot> WeatherSnapshots => Set<WeatherSnapshot>();

    // Her indlæses alle Fluent API-konfigurationer fra Infrastructure-projektet
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(JagtJournalDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}