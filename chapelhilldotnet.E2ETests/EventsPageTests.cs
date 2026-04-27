using Microsoft.Playwright;
using System.Text.RegularExpressions;

namespace chapelhilldotnet.E2ETests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public partial class EventsPageTests : BlazorPageTest
{
    [Test]
    public async Task EventsPage_LoadsSuccessfully()
    {
        await Page.GotoAsync($"{BaseUrl}/events");

        // Check that the page loaded
        await Expect(Page).ToHaveURLAsync(EventsRegex());
    }

    [Test]
    public async Task EventsPage_DisplaysEventCards()
    {
        await Page.GotoAsync($"{BaseUrl}/events");

        // Wait for deterministic events-page content to render.
        ILocator sectionHeading = Page.GetByRole(AriaRole.Heading,
            new PageGetByRoleOptions { NameRegex = new Regex("coming up next", RegexOptions.IgnoreCase) });
        await Expect(sectionHeading).ToBeVisibleAsync();

        ILocator eventCards = Page.Locator(".card-grid--events .event-card");
        Assert.That(await eventCards.CountAsync(), Is.GreaterThan(0),
            "Events page should render at least one upcoming event card.");
    }

    [Test]
    public async Task EventsPage_HasNavigationBack()
    {
        await Page.GotoAsync($"{BaseUrl}/events");

        // Check for a link back to home
        ILocator homeLink = Page.GetByRole(AriaRole.Link, new PageGetByRoleOptions { NameRegex = HomeRegex() });

        if (await homeLink.CountAsync() > 0)
        {
            await homeLink.First.ClickAsync();
            await Expect(Page).ToHaveURLAsync(BaseUrl);
        }
        else
        {
            ILocator logoLink = Page.Locator("header a").First;
            await logoLink.ClickAsync();
            await Expect(Page).ToHaveURLAsync(BaseUrl);
        }
    }

    [Test]
    public async Task EventsPage_DisplaysDateInformation()
    {
        await Page.GotoAsync($"{BaseUrl}/events");

        // Wait for archived event cards and verify semantic date markup.
        ILocator archiveCards = Page.Locator(".archive-grid .archive-card");
        await Expect(archiveCards.First).ToBeVisibleAsync();

        ILocator dateElements = Page.Locator(".archive-grid time[datetime]");
        Assert.That(await dateElements.CountAsync(), Is.GreaterThan(0),
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
        string? bodyContent = await Page.TextContentAsync("body");
        Assert.That(bodyContent, Is.Not.Null.And.Not.Empty,
            "Events page should render on mobile viewport");

        // Test on desktop viewport
        await Page.SetViewportSizeAsync(1920, 1080);
        await Page.ReloadAsync();

        bodyContent = await Page.TextContentAsync("body");
        Assert.That(bodyContent, Is.Not.Null.And.Not.Empty,
            "Events page should render on desktop viewport");
    }

    [GeneratedRegex(".*events.*", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex EventsRegex();

    [GeneratedRegex("Home", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex HomeRegex();
}
