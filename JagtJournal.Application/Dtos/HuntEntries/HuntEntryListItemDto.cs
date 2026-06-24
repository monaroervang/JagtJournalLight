namespace JagtJournal.Application.Dtos.HuntEntries;

public class HuntEntryListItemDto
{
    public Guid Id { get; set; }
    public DateOnly Date { get; set; }
    public int CartridgesUsed { get; set; }
}