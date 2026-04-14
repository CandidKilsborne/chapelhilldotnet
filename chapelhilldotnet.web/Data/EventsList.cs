using chapelhilldotnet.web.Models;

namespace chapelhilldotnet.web.Data;

public static class EventsList
{
    public static List<Event> Events { get; } =
    [
        new()
        {
            Id = 1,
            Title = "Coffee and Coworking for .NET Builders",
            Description = "A lower-pressure morning session for swapping notes, getting unstuck, and meeting other local developers.",
            Date = DateTime.Today.AddDays(9),
            Location = "Perennial Cafe, Chapel Hill",
            Time = "8:00 AM - 10:00 AM",
            Attendees = 16
        },

        new()
        {
            Id = 2,
            Title = "Building Blazor Interfaces That Feel Fast",
            Description = "A practical evening session on component structure, perceived performance, and interface polish in Blazor apps.",
            Date = DateTime.Today.AddDays(18),
            Location = "Launch Chapel Hill",
            Time = "6:00 PM - 8:00 PM",
            Attendees = 30
        },

        new()
        {
            Id = 3,
            Title = "Azure Operations Office Hours",
            Description = "Bring your deployment, diagnostics, or platform questions and work through them with other engineers.",
            Date = DateTime.Today.AddDays(32),
            Location = "Online and Chapel Hill hybrid session",
            Time = "6:30 PM - 8:00 PM",
            Attendees = 24
        }
    ];
}
