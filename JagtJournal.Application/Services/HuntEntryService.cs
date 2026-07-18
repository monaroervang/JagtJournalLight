using JagtJournal.Application.Dtos.HuntEntries;
using JagtJournal.Application.Interfaces;
using JagtJournal.Domain.Entities;

namespace JagtJournal.Application.Services;

public class HuntEntryService : IHuntEntryService
{
    private readonly IHuntEntryRepository _huntEntryRepository;
    private readonly IUserRepository _userRepository;

    // Application Service får repositories gennem dependency injection
    public HuntEntryService(
        IHuntEntryRepository huntEntryRepository,
        IUserRepository userRepository)
    {
        _huntEntryRepository = huntEntryRepository;
        _userRepository = userRepository;
    }

    // Opretter en ny jagtjournal-post for et brugernavn
    public async Task<Guid> CreateAsync(CreateHuntEntryDto dto, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(dto.Username))
            throw new ArgumentException("Brugernavn skal udfyldes.");

        var username = dto.Username.Trim();

        var user = await _userRepository.GetByUsernameAsync(username, cancellationToken);

        if (user is null)
        {
            user = new User(username);
            await _userRepository.AddAsync(user, cancellationToken);
        }

        var huntEntry = new HuntEntry(
            user.Id,
            dto.HuntingAreaId,
            dto.Date,
            dto.CartridgesUsed);

        await _huntEntryRepository.AddAsync(huntEntry, cancellationToken);

        return huntEntry.Id;
    }

    // Henter detaljer for én jagtjournal-post
    public async Task<HuntEntryDetailsDto?> GetDetailsAsync(Guid id, CancellationToken cancellationToken = default)
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

    // Henter de seneste jagtjournal-poster for et brugernavn
    public async Task<IReadOnlyList<HuntEntryListItemDto>> GetLatestByUsernameAsync(
        string username,
        int maxCount = 10,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(username))
            return [];

        var user = await _userRepository.GetByUsernameAsync(username.Trim(), cancellationToken);

        if (user is null)
            return [];

        var huntEntries = await _huntEntryRepository.GetByUserIdAsync(user.Id, cancellationToken);

        return huntEntries
            .Take(maxCount)
            .Select(huntEntry => new HuntEntryListItemDto
            {
                Id = huntEntry.Id,
                Date = huntEntry.Date,
                CartridgesUsed = huntEntry.CartridgesUsed
            })
            .ToList();
    }
}