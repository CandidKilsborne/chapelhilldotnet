namespace chapelhilldotnet.web.Models;

public class Organizer
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public required string Bio { get; init; }
    public required string ImageUrl { get; init; }
    public required string TwitterUrl { get; init; }
    public required string LinkedInUrl { get; init; }
    public required string GitHubUrl { get; init; }
}