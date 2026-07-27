using JagtJournal.Application.Interfaces;
using JagtJournal.Infrastructure.Data;
using JagtJournal.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JagtJournal.Infrastructure.DependencyInjection;

/// <summary>
/// Registrerer Infrastructure-lagets services.
/// Infrastructure ejer selv EF Core og repositories.
/// </summary>
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string connectionString)
    {
        // Registrerer EF Core DbContext.
        //
        // DbContext er en del af Infrastructure, fordi det er en teknisk detalje
        // om, hvordan data gemmes i databasen. De øvrige lag skal ikke kende til
        // EF Core eller SQL Server, men kun arbejde gennem interfaces.
        //
        // Derfor skjules konfigurationen bag AddInfrastructure(), så både Blazor,
        // API og andre klienter kan genbruge den samme registrering.
        services.AddDbContextFactory<JagtJournalDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IHuntEntryRepository, EfHuntEntryRepository>();
        services.AddScoped<IUserRepository, EfUserRepository>();

        return services;
    }
}