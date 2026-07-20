using System.ComponentModel.DataAnnotations;

namespace JagtJournal.Application.Dtos.HuntEntries;
// Data Annotations er attributter, der bruges til at beskrive regler for data.
// Når DTO'en anvendes i en Blazor EditForm, bliver reglerne automatisk
// anvendt til validering af brugerens input, inden formularen sendes.

public class CreateHuntEntryDto
{
    [Required(ErrorMessage = "Brugernavn skal udfyldes.")] //Required er Data Annotation, der sikrer, at et felt skal udfyldes
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Jagtområde skal vælges.")]
    public Guid HuntingAreaId { get; set; }

    [Required(ErrorMessage = "Dato skal udfyldes.")]
    public DateOnly Date { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Antallet af patroner må ikke være negativt.")] //Range er en Data Annotation, der sikrer, at en numerisk værdi ligger inden for for et bestemt interval
    public int CartridgesUsed { get; set; }
}