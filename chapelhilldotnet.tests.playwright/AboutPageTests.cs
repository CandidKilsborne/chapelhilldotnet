using Microsoft.Playwright;
using NUnit.Framework;

namespace chapelhilldotnet.tests.playwright;

[TestFixture]
public class AboutPageTests : BaseTest
{
    [Test]
    public async Task AboutPage_ShouldLoadSuccessfully()
    {
        // Arrange & Act
        await Page.GotoAsync($"{BaseUrl}/about");

        // Assert
        await Expect(Page.Locator("h1")).ToContainTextAsync("About Our Meetup");
        await Expect(Page.Locator("h2")).ToContainTextAsync("Our Mission");
    }

    [Test]
    public async Task AboutPage_ShouldDisplayMissionSection()
    {
        // Arrange & Act
        await Page.GotoAsync($"{BaseUrl}/about");

        // Assert
        var missionSection = Page.Locator("text=Our Mission").Locator("..");
        await Expect(missionSection).ToContainTextAsync("Chapel Hill .NET and Azure Meetup");
    }

    [Test]
    public async Task AboutPage_ShouldDisplayWhatWeCoverSection()
    {
        // Arrange & Act
        await Page.GotoAsync($"{BaseUrl}/about");

        // Assert
        await Expect(Page.Locator("h2:has-text('What We Cover')")).ToBeVisibleAsync();
        await Expect(Page.Locator("h3:has-text('.NET Development')")).ToBeVisibleAsync();
        await Expect(Page.Locator("h3:has-text('Azure Cloud')")).ToBeVisibleAsync();
    }

    [Test]
    [Category("Content")]
    public async Task AboutPage_ShouldHaveCompleteContent()
    {
        // Arrange & Act
        await Page.GotoAsync($"{BaseUrl}/about");

        // Assert - Check for key content sections
        await Expect(Page.Locator("h1")).ToBeVisibleAsync();
        await Expect(Page.Locator("h2")).ToBeVisibleAsync();

        // Verify multiple sections exist
        var headings = await Page.Locator("h2, h3").AllAsync();
        Assert.That(headings.Count, Is.GreaterThan(2), "Page should have multiple content sections");
    }
}