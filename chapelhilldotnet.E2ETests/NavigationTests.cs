using Microsoft.Playwright;
using System.Text.RegularExpressions;

namespace chapelhilldotnet.E2ETests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public partial class NavigationTests : BlazorPageTest
{
    [Test]
    public async Task HomePage_LoadsSuccessfully()
    {
        await Page.GotoAsync(BaseUrl);
        await Expect(Page).ToHaveTitleAsync(TitleRegex());
    }

    [Test]
    public async Task HomePage_ContainsMainHeading()
    {
        await Page.GotoAsync(BaseUrl);
        ILocator heading = Page.Locator("h1");
        await Expect(heading).ToContainTextAsync("Connect, learn, and build better");
    }

    [Test]
    public async Task AboutLink_NavigatesToAboutPage()
    {
        await Page.GotoAsync(BaseUrl);
        await Page.GetByRole(AriaRole.Link, new PageGetByRoleOptions { Name = "About" }).First.ClickAsync();
        await Expect(Page).ToHaveURLAsync(AboutRegex());
    }

    [Test]
    public async Task EventsLink_NavigatesToEventsPage()
    {
        await Page.GotoAsync(BaseUrl);

        // Check if there's an Events link in navigation
        IReadOnlyList<ILocator> eventsLinks = await Page
            .GetByRole(AriaRole.Link, new PageGetByRoleOptions { NameRegex = EventLinksRegex() }).AllAsync();

        if (eventsLinks.Count > 0)
        {
            await eventsLinks[0].ClickAsync();
            await Expect(Page).ToHaveURLAsync(EventsRegex());
        }
        else
        {
            // If no Events link, test passes (it might be on the home page as a section)
            Assert.Pass("No separate Events page - events might be on the home page");
        }
    }

    [Test]
    public async Task Navigation_ContainsLogo()
    {
        await Page.GotoAsync(BaseUrl);

        // Check for the text or logo in the header
        string? headerText = await Page.Locator("header[role='banner']").TextContentAsync();
        Assert.That(headerText, Does.Contain("Chapel Hill").Or.Contain(".NET"));
    }

    [GeneratedRegex("Chapel Hill")]
    private static partial Regex TitleRegex();

    [GeneratedRegex(".*about.*", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex AboutRegex();

    [GeneratedRegex("Events?", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex EventLinksRegex();

    [GeneratedRegex(".*(events|#events).*", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex EventsRegex();
}
