using System.ComponentModel.DataAnnotations;

namespace JagtJournal.Application.Dtos.HuntingAreas;

/// <summary>
/// Indeholder de data, der kræves for at oprette et jagtområde.
/// </summary>
public class CreateHuntingAreaDto
{
    [Required(ErrorMessage = "Navn skal udfyldes.")]
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }
}