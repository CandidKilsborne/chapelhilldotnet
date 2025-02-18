namespace chapelhilldotnet.web.Models;

public class Event
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    public required string Location { get; set; }
    public string Time { get; set; } = string.Empty;
    public int Attendees { get; set; }
}