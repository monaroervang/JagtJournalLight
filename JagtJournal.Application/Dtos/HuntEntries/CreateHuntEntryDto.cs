namespace JagtJournal.Application.Dtos.HuntEntries;

public class CreateHuntEntryDto
{
    public string Username { get; set; } = string.Empty;
    public Guid HuntingAreaId { get; set; }
    public DateOnly Date { get; set; }
    public int CartridgesUsed { get; set; }
}