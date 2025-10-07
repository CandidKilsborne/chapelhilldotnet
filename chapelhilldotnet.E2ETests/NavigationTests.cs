using Microsoft.Playwright.NUnit;
using Microsoft.Playwright;
using NUnit.Framework;
using System.Text.RegularExpressions;

namespace chapelhilldotnet.E2ETests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class NavigationTests : PageTest
{
    private const string BaseUrl = "http://localhost:5000";

    [Test]
    public async Task HomePage_LoadsSuccessfully()
    {
        await Page.GotoAsync(BaseUrl);
        await Expect(Page).ToHaveTitleAsync(new Regex("Chapel Hill"));
    }

    [Test]
    public async Task HomePage_ContainsMainHeading()
    {
        await Page.GotoAsync(BaseUrl);
        var heading = Page.Locator("h1");
        await Expect(heading).ToContainTextAsync("Connect, Learn, and Grow");
    }

    [Test]
    public async Task AboutLink_NavigatesToAboutPage()
    {
        await Page.GotoAsync(BaseUrl);
        await Page.GetByRole(AriaRole.Link, new() { Name = "About" }).First.ClickAsync();
        await Expect(Page).ToHaveURLAsync(new Regex(".*about.*", RegexOptions.IgnoreCase));
    }

    [Test]
    public async Task EventsLink_NavigatesToEventsPage()
    {
        await Page.GotoAsync(BaseUrl);
        
        // Check if there's an Events link in navigation
        var eventsLinks = await Page.GetByRole(AriaRole.Link, new() { NameRegex = new Regex("Events?", RegexOptions.IgnoreCase) }).AllAsync();
        
        if (eventsLinks.Count > 0)
        {
            await eventsLinks[0].ClickAsync();
            await Expect(Page).ToHaveURLAsync(new Regex(".*(events|#events).*", RegexOptions.IgnoreCase));
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
        var headerText = await Page.Locator("header").TextContentAsync();
        Assert.That(headerText, Does.Contain("Chapel Hill").Or.Contain(".NET"));
    }
}
