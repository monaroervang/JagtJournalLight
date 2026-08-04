using JagtJournal.Facade.HuntEntries;
using JagtJournal.Facade.HuntingAreas;
using Microsoft.Extensions.DependencyInjection;

namespace JagtJournal.Facade.DependencyInjection;

/// <summary>
/// Registrerer Facade-lagets services.
///
/// Facade fungerer som indgangen til Application-laget for
/// præsentationslag som Blazor og API.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registrerer alle facades.
    /// </summary>
    public static IServiceCollection AddFacade(
        this IServiceCollection services)
    {
        services.AddScoped<IHuntEntryFacade, HuntEntryFacade>();
        services.AddScoped<IHuntingAreaFacade, HuntingAreaFacade>();

        return services;
    }
}