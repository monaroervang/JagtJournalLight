using JagtJournal.Domain.Entities;

namespace JagtJournal.Application.Interfaces;

/// <summary>
/// Beskriver de databaseoperationer, som Application-laget
/// har brug for i forbindelse med jagtområder.
/// </summary>
public interface IHuntingAreaRepository
{
    /// <summary>
    /// Henter alle jagtområder.
    /// </summary>
    Task<IReadOnlyList<HuntingArea>> GetAllAsync(
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Gemmer et nyt jagtområde i databasen.
    /// </summary>
    Task AddAsync(
        HuntingArea huntingArea,
        CancellationToken cancellationToken = default);
}