namespace chapelhilldotnet.web.Models;

public class Event
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    public required string Location { get; set; }
}