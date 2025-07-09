using Microsoft.Playwright;
using NUnit.Framework;

namespace chapelhilldotnet.tests.playwright;

[TestFixture]
public class HomePageTests : BaseTest
{
    [Test]
    public async Task HomePage_ShouldLoadSuccessfully()
    {
        // Arrange & Act
        await Page.GotoAsync(BaseUrl);

        // Assert
        await Expect(Page).ToHaveTitleAsync("Chapel Hill .Net");
        await Expect(Page.Locator("h1")).ToContainTextAsync("Connect, Learn, and Grow with .NET & Azure");
    }

    [Test]
    public async Task HomePage_ShouldHaveNavigationMenu()
    {
        // Arrange & Act
        await Page.GotoAsync(BaseUrl);

        // Assert
        await Expect(Page.Locator("nav")).ToBeVisibleAsync();
        await Expect(Page.Locator("nav a[href='#about']")).ToContainTextAsync("About");
        await Expect(Page.Locator("nav a[href='#events']")).ToContainTextAsync("Events");
        await Expect(Page.Locator("nav a[href='#organizers']")).ToContainTextAsync("Organizers");
    }

    [Test]
    public async Task HomePage_ShouldHaveDarkModeToggle()
    {
        // Arrange & Act
        await Page.GotoAsync(BaseUrl);

        // Assert
        var darkModeToggle = Page.Locator("button[aria-label*='mode']");
        await Expect(darkModeToggle).ToBeVisibleAsync();
    }

    [Test]
    public async Task HomePage_ShouldDisplayUpcomingEvents()
    {
        // Arrange & Act
        await Page.GotoAsync(BaseUrl);

        // Assert
        await Expect(Page.Locator("#events")).ToBeVisibleAsync();
        await Expect(Page.Locator("#events h2")).ToContainTextAsync("Upcoming Events");
    }

    [Test]
    public async Task HomePage_ShouldDisplayOrganizers()
    {
        // Arrange & Act
        await Page.GotoAsync(BaseUrl);

        // Assert
        await Expect(Page.Locator("#organizers")).ToBeVisibleAsync();
        await Expect(Page.Locator("#organizers h2")).ToContainTextAsync("Meet Our Organizers");
    }
}