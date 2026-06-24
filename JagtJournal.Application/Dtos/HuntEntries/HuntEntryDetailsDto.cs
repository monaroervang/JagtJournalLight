namespace JagtJournal.Application.Dtos.HuntEntries;

public class HuntEntryDetailsDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid HuntingAreaId { get; set; }
    public DateOnly Date { get; set; }
    public int CartridgesUsed { get; set; }
}