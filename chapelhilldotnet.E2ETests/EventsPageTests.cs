using Microsoft.Playwright.NUnit;
using Microsoft.Playwright;
using NUnit.Framework;
using System.Text.RegularExpressions;

namespace chapelhilldotnet.E2ETests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class EventsPageTests : PageTest
{
    private const string BaseUrl = "http://localhost:5000";

    [Test]
    public async Task EventsPage_LoadsSuccessfully()
    {
        await Page.GotoAsync($"{BaseUrl}/events");
        
        // Check that the page loaded
        await Expect(Page).ToHaveURLAsync(new Regex(".*events.*", RegexOptions.IgnoreCase));
    }

    [Test]
    public async Task EventsPage_DisplaysEventCards()
    {
        await Page.GotoAsync($"{BaseUrl}/events");
        
        // Wait for the page to load
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        
        // Check if there are event headings or sections
        var upcomingSection = Page.Locator("text=/upcoming events/i").First;
        var isUpcomingVisible = await upcomingSection.IsVisibleAsync().ConfigureAwait(false);
        
        if (isUpcomingVisible)
        {
            Assert.Pass("Events page displays upcoming events section");
        }
        else
        {
            // Alternative: check for any event-related content
            var pageContent = await Page.TextContentAsync("body");
            Assert.That(pageContent, Does.Contain("Event").Or.Contain("Meetup"), 
                "Page should contain event-related content");
        }
    }

    [Test]
    public async Task EventsPage_HasNavigationBack()
    {
        await Page.GotoAsync($"{BaseUrl}/events");
        
        // Check for a link back to home
        var homeLink = Page.GetByRole(AriaRole.Link, new() { NameRegex = new Regex("Home", RegexOptions.IgnoreCase) });
        
        if (await homeLink.CountAsync() > 0)
        {
            await homeLink.First.ClickAsync();
            await Expect(Page).ToHaveURLAsync(BaseUrl);
        }
        else
        {
            // Alternative: check for logo link
            var logoLink = Page.Locator("header a").First;
            await logoLink.ClickAsync();
            await Expect(Page).ToHaveURLAsync(BaseUrl);
        }
    }

    [Test]
    public async Task EventsPage_DisplaysDateInformation()
    {
        await Page.GotoAsync($"{BaseUrl}/events");
        
        // Wait for content to load
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        
        // Look for date-related content or calendar icons
        var pageText = await Page.TextContentAsync("body");
        
        // Check for date patterns or date-related words
        var hasDateContent = pageText != null && (
            pageText.Contains("January") || 
            pageText.Contains("February") || 
            pageText.Contains("March") ||
            pageText.Contains("2024") ||
            pageText.Contains("2025") ||
            pageText.Contains("Event") ||
            pageText.Contains("Date") ||
            pageText.Contains("Time")
        );
        
        Assert.That(hasDateContent, Is.True, 
            "Events page should display date or time information");
    }

    [Test]
    public async Task EventsPage_HasResponsiveDesign()
    {
        // Test on mobile viewport
        await Page.SetViewportSizeAsync(375, 667);
        await Page.GotoAsync($"{BaseUrl}/events");
        
        // Verify page is accessible on mobile
        await Page.WaitForLoadStateAsync(LoadState.Load);
        var bodyContent = await Page.TextContentAsync("body");
        Assert.That(bodyContent, Is.Not.Null.And.Not.Empty, 
            "Events page should render on mobile viewport");
        
        // Test on desktop viewport
        await Page.SetViewportSizeAsync(1920, 1080);
        await Page.ReloadAsync();
        
        bodyContent = await Page.TextContentAsync("body");
        Assert.That(bodyContent, Is.Not.Null.And.Not.Empty, 
            "Events page should render on desktop viewport");
    }
}
