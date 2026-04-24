using System.Runtime.InteropServices;
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
            Date = new DateTime(2026, 4, 24),
            Location = "La Vita Dolce",
            Time = "8:30 AM - 10:30 AM",
            Attendees = 16
        },

        new()
        {
            Id = 2,
            Title = "Building Blazor Interfaces That Feel Fast",
            Description = "A practical evening session on component structure, perceived performance, and interface polish in Blazor apps.",
            Date = new DateTime(2026, 5, 12),
            Location = "Online session",
            Time = "4:00 PM - 5:00 PM",
            Attendees = 30
        },

        new()
        {
            Id = 3,
            Title = "Azure Operations Office Hours",
            Description = "Bring your deployment, diagnostics, or platform questions and work through them with other engineers.",
            Date = new DateTime(2026, 6, 5),
            Location = "Online session",
            Time = "7:30 PM - 8:30 PM",
            Attendees = 24
        }
    ];
}
