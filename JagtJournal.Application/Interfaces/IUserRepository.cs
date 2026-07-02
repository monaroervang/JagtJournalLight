using JagtJournal.Domain.Entities;

namespace JagtJournal.Application.Interfaces;

public interface IUserRepository
{
    // Henter en bruger ud fra brugernavn
    Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);

    // Gemmer en ny bruger
    Task AddAsync(User user, CancellationToken cancellationToken = default);
}