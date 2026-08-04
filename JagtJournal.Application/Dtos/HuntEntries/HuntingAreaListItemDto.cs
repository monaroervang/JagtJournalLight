namespace JagtJournal.Application.Dtos.HuntingAreas;

/// <summary>
/// Indeholder de oplysninger om et jagtområde,
/// som klienter som API og Blazor skal have adgang til.
/// </summary>
public class HuntingAreaListItemDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}