using JagtJournal.Application.Interfaces;
using JagtJournal.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace JagtJournal.Application.DependencyInjection;

/// <summary>
/// Registrerer de services, der hører til Application-laget.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Tilføjer Application-lagets services til Dependency Injection-containeren.
    /// </summary>
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IHuntEntryService, HuntEntryService>();

        return services;
    }
}