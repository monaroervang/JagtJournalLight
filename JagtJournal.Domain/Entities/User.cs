namespace JagtJournal.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Username { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;

    // Bruges af EF Core
    private User()
    {
    }

    // Bruges når vi selv opretter en bruger
    public User(string username, string? name = null, string? email = null)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Brugernavn skal udfyldes.");

        Id = Guid.NewGuid();
        Username = username.Trim();
        Name = string.IsNullOrWhiteSpace(name) ? Username : name.Trim();
        Email = string.IsNullOrWhiteSpace(email) ? string.Empty : email.Trim();
    }
}