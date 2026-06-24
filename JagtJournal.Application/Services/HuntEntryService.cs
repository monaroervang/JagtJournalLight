using JagtJournal.Application.Dtos.HuntEntries;
using JagtJournal.Application.Interfaces;
using JagtJournal.Domain.Entities;

namespace JagtJournal.Application.Services;

public class HuntEntryService : IHuntEntryService
{
    private readonly IHuntEntryRepository _huntEntryRepository;

    // Application Service får repository gennem dependency injection
    public HuntEntryService(IHuntEntryRepository huntEntryRepository)
    {
        _huntEntryRepository = huntEntryRepository;
    }

    // Opretter en ny HuntEntry i domænet og beder repository om at gemme den
    public async Task<Guid> CreateAsync(
        CreateHuntEntryDto dto,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var huntEntry = new HuntEntry(
            dto.UserId,
            dto.HuntingAreaId,
            dto.Date,
            dto.CartridgesUsed
        );

        await _huntEntryRepository.AddAsync(huntEntry, cancellationToken);

        return huntEntry.Id;
    }

    // Henter én HuntEntry og mapper den til en DTO
    public async Task<HuntEntryDetailsDto?> GetDetailsAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var huntEntry = await _huntEntryRepository.GetByIdAsync(id, cancellationToken);

        if (huntEntry is null)
            return null;

        return new HuntEntryDetailsDto
        {
            Id = huntEntry.Id,
            UserId = huntEntry.UserId,
            HuntingAreaId = huntEntry.HuntingAreaId,
            Date = huntEntry.Date,
            CartridgesUsed = huntEntry.CartridgesUsed
        };
    }

    // Henter en brugers jagtjournal-poster og mapper dem til liste-DTO'er
    public async Task<IReadOnlyList<HuntEntryListItemDto>> GetByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        var huntEntries = await _huntEntryRepository.GetByUserIdAsync(userId, cancellationToken);

        return huntEntries
            .Select(huntEntry => new HuntEntryListItemDto
            {
                Id = huntEntry.Id,
                Date = huntEntry.Date,
                CartridgesUsed = huntEntry.CartridgesUsed
            })
            .ToList();
    }
}