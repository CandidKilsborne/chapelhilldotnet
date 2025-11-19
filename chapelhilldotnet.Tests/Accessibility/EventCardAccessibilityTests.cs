using Bunit;
using chapelhilldotnet.web.Components;
using chapelhilldotnet.web.Models;
using Xunit;

namespace chapelhilldotnet.Tests.Accessibility;

/// <summary>
/// Accessibility tests for EventCard component to ensure WCAG 2.1 AA compliance
/// </summary>
public class EventCardAccessibilityTests : TestContext
{
    [Fact]
    public void EventCard_UsesSemanticArticleElement()
    {
        // Arrange
        var testEvent = new Event
        {
            Id = 1,
            Title = "Test Event",
            Description = "Test Description",
            Date = new DateTime(2024, 12, 25),
            Location = "Test Location"
        };

        // Act
        var cut = RenderComponent<EventCard>(parameters => parameters
            .Add(p => p.Event, testEvent));

        // Assert
        var article = cut.Find("article");
        Assert.NotNull(article);
    }

    [Fact]
    public void EventCard_DateHasSemanticTimeElement()
    {
        // Arrange
        var testEvent = new Event
        {
            Id = 1,
            Title = "Test Event",
            Description = "Test Description",
            Date = new DateTime(2024, 12, 25),
            Location = "Test Location"
        };

        // Act
        var cut = RenderComponent<EventCard>(parameters => parameters
            .Add(p => p.Event, testEvent));

        // Assert
        var timeElement = cut.Find("time");
        Assert.NotNull(timeElement);
        Assert.Equal("2024-12-25", timeElement.GetAttribute("datetime"));
        Assert.Equal("December 25, 2024", timeElement.TextContent);
    }

    [Fact]
    public void EventCard_IconsHaveAriaHidden()
    {
        // Arrange
        var testEvent = new Event
        {
            Id = 1,
            Title = "Test Event",
            Description = "Test Description",
            Date = new DateTime(2024, 12, 25),
            Location = "Test Location"
        };

        // Act
        var cut = RenderComponent<EventCard>(parameters => parameters
            .Add(p => p.Event, testEvent));

        // Assert - Both calendar and map icons should have aria-hidden
        var markup = cut.Markup;
        Assert.Contains("aria-hidden=\"true\"", markup);
    }

    [Fact]
    public void EventCard_HasProperHeadingHierarchy()
    {
        // Arrange
        var testEvent = new Event
        {
            Id = 1,
            Title = "Test Event",
            Description = "Test Description",
            Date = new DateTime(2024, 12, 25),
            Location = "Test Location"
        };

        // Act
        var cut = RenderComponent<EventCard>(parameters => parameters
            .Add(p => p.Event, testEvent));

        // Assert
        var heading = cut.Find("h3");
        Assert.NotNull(heading);
        Assert.Equal("Test Event", heading.TextContent);
    }

    [Theory]
    [InlineData(2025, 1, 15, "2025-01-15")]
    [InlineData(2024, 12, 31, "2024-12-31")]
    [InlineData(2025, 6, 1, "2025-06-01")]
    public void EventCard_DatetimeAttributeFormattedCorrectly(int year, int month, int day, string expected)
    {
        // Arrange
        var testEvent = new Event
        {
            Id = 1,
            Title = "Test Event",
            Description = "Test Description",
            Date = new DateTime(year, month, day),
            Location = "Test Location"
        };

        // Act
        var cut = RenderComponent<EventCard>(parameters => parameters
            .Add(p => p.Event, testEvent));

        // Assert
        var timeElement = cut.Find("time");
        Assert.Equal(expected, timeElement.GetAttribute("datetime"));
    }
}
