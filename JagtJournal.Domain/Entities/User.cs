namespace JagtJournal.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }

    // Bruges af EF Core
    private User()
    {
        Name = null!;
        Email = null!;
    }

    // Bruges når vi selv opretter en ny bruger i domænet
    public User(string name, string email)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Navn skal udfyldes.");

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email skal udfyldes.");

        Id = Guid.NewGuid();
        Name = name;
        Email = email;
    }
}