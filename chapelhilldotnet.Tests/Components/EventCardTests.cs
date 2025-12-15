using Bunit;
using chapelhilldotnet.web.Components;
using chapelhilldotnet.web.Models;
using Xunit;

namespace chapelhilldotnet.Tests.Components;

public class EventCardTests : TestContext
{
    [Fact]
    public void EventCard_RendersEventTitle()
    {
        // Arrange
        var testEvent = new Event
        {
            Id = 1,
            Title = "Test Event",
            Description = "Test Description",
            Date = new DateTime(2024, 12, 25),
            Location = "Test Location",
            Time = "6:00 PM",
            Attendees = 50
        };

        // Act
        var cut = RenderComponent<EventCard>(parameters => parameters
            .Add(p => p.Event, testEvent));

        // Assert
        var title = cut.Find("h3.event-title");
        Assert.Equal("Test Event", title.TextContent);
        
        var description = cut.Find("p.text-gray-700");
        Assert.Equal("Test Description", description.TextContent);
    }

    [Fact]
    public void EventCard_DisplaysCorrectDate()
    {
        // Arrange
        var testEvent = new Event
        {
            Id = 2,
            Title = "New Year Event",
            Description = "Celebrate the new year",
            Date = new DateTime(2025, 1, 1),
            Location = "Downtown",
            Time = "11:00 PM",
            Attendees = 100
        };

        // Act
        var cut = RenderComponent<EventCard>(parameters => parameters
            .Add(p => p.Event, testEvent));

        // Assert
        var dateSpan = cut.Find("div.flex.items-center.space-x-2.text-gray-600.mb-2 time");
        Assert.Equal("January 1, 2025", dateSpan.TextContent);
    }

    [Fact]
    public void EventCard_DisplaysLocation()
    {
        // Arrange
        var testEvent = new Event
        {
            Id = 3,
            Title = "Tech Meetup",
            Description = "Monthly tech gathering",
            Date = new DateTime(2025, 2, 15),
            Location = "Chapel Hill Library",
            Time = "7:00 PM",
            Attendees = 30
        };

        // Act
        var cut = RenderComponent<EventCard>(parameters => parameters
            .Add(p => p.Event, testEvent));

        // Assert
        var locationSpan = cut.Find("div.flex.items-center.space-x-2.text-gray-600 span");
        Assert.Equal("Chapel Hill Library", locationSpan.TextContent);
    }
}
