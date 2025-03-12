using chapelhilldotnet.web.Models;

namespace chapelhilldotnet.web.Data;

public static class EventsList
{
    public static List<Event> Events { get; } =
    [
        new()
        {
            Title = "Coffee & Coworking",
            Description = "Meet other developers and learn about the latest .NET and Azure technologies.",
            Date = DateTime.Now.AddDays(7),
            Location = "TBD"
        },

        new()
        {
            Title = "Coffee & Coworking",
            Description = "Join us for an exciting discussion on the latest .NET and Azure technologies.",
            Date = DateTime.Now.AddDays(14),
            Location = "TBD"
        },

        new()
        {
            Title = "Coffee & Coworking",
            Description = "Join us for an exciting discussion on the latest .NET and Azure technologies.",
            Date = DateTime.Now.AddDays(21),
            Location = "TBD"
        }
    ];
}