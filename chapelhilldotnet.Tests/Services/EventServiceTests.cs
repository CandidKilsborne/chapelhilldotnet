using chapelhilldotnet.web.Models;
using chapelhilldotnet.web.Services;
using Microsoft.JSInterop;
using Moq;
using Xunit;

namespace chapelhilldotnet.Tests.Services;

public class EventServiceTests
{
    [Fact]
    public async Task CreateEventAsync_AddsNewEvent()
    {
        // Arrange
        var mockJsRuntime = new Mock<IJSRuntime>();
        var service = new EventService(mockJsRuntime.Object);
        
        var newEvent = new Event
        {
            Title = "Test Event",
            Description = "Test Description",
            Date = DateTime.Now.AddDays(7),
            Location = "Test Location",
            Time = "6:00 PM",
            Attendees = 25
        };

        // Act
        var result = await service.CreateEventAsync(newEvent);

        // Assert
        Assert.NotEqual(0, result.Id);
        Assert.Equal("Test Event", result.Title);
    }

    [Fact]
    public async Task GetAllEventsAsync_ReturnsOrderedEvents()
    {
        // Arrange
        var mockJsRuntime = new Mock<IJSRuntime>();
        var service = new EventService(mockJsRuntime.Object);
        
        var event1 = new Event
        {
            Title = "Future Event",
            Date = DateTime.Now.AddDays(30),
            Location = "Location 1"
        };
        
        var event2 = new Event
        {
            Title = "Near Event",
            Date = DateTime.Now.AddDays(7),
            Location = "Location 2"
        };

        await service.CreateEventAsync(event1);
        await service.CreateEventAsync(event2);

        // Act
        var events = await service.GetAllEventsAsync();

        // Assert
        Assert.Equal(2, events.Count(e => e.Title == "Future Event" || e.Title == "Near Event"));
        // Events should be ordered by date
        var customEvents = events.Where(e => e.Title == "Future Event" || e.Title == "Near Event").ToList();
        Assert.True(customEvents[0].Date <= customEvents[1].Date);
    }

    [Fact]
    public async Task UpdateEventAsync_UpdatesExistingEvent()
    {
        // Arrange
        var mockJsRuntime = new Mock<IJSRuntime>();
        var service = new EventService(mockJsRuntime.Object);
        
        var originalEvent = new Event
        {
            Title = "Original Title",
            Date = DateTime.Now.AddDays(7),
            Location = "Original Location"
        };

        var created = await service.CreateEventAsync(originalEvent);

        // Act
        created.Title = "Updated Title";
        created.Location = "Updated Location";
        await service.UpdateEventAsync(created);

        var retrieved = await service.GetEventByIdAsync(created.Id);

        // Assert
        Assert.NotNull(retrieved);
        Assert.Equal("Updated Title", retrieved.Title);
        Assert.Equal("Updated Location", retrieved.Location);
    }

    [Fact]
    public async Task DeleteEventAsync_RemovesEvent()
    {
        // Arrange
        var mockJsRuntime = new Mock<IJSRuntime>();
        var service = new EventService(mockJsRuntime.Object);
        
        var eventToDelete = new Event
        {
            Title = "Event to Delete",
            Date = DateTime.Now.AddDays(7),
            Location = "Test Location"
        };

        var created = await service.CreateEventAsync(eventToDelete);

        // Act
        var deleted = await service.DeleteEventAsync(created.Id);
        var retrieved = await service.GetEventByIdAsync(created.Id);

        // Assert
        Assert.True(deleted);
        Assert.Null(retrieved);
    }

    [Fact]
    public async Task GetEventByIdAsync_ReturnsCorrectEvent()
    {
        // Arrange
        var mockJsRuntime = new Mock<IJSRuntime>();
        var service = new EventService(mockJsRuntime.Object);
        
        var event1 = new Event
        {
            Title = "Event 1",
            Date = DateTime.Now.AddDays(7),
            Location = "Location 1"
        };

        var created = await service.CreateEventAsync(event1);

        // Act
        var retrieved = await service.GetEventByIdAsync(created.Id);

        // Assert
        Assert.NotNull(retrieved);
        Assert.Equal("Event 1", retrieved.Title);
        Assert.Equal(created.Id, retrieved.Id);
    }

    [Fact]
    public async Task DeleteEventAsync_WithNonExistentId_ReturnsFalse()
    {
        // Arrange
        var mockJsRuntime = new Mock<IJSRuntime>();
        var service = new EventService(mockJsRuntime.Object);

        // Act
        var result = await service.DeleteEventAsync(999);

        // Assert
        Assert.False(result);
    }
}
