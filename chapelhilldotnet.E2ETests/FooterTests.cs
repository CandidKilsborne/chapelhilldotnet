using Microsoft.Playwright;

namespace chapelhilldotnet.E2ETests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class FooterTests : BlazorPageTest
{
    [Test]
    public async Task Footer_IsVisible()
    {
        await Page.GotoAsync(BaseUrl);

        ILocator footer = Page.Locator("footer#contact");
        await Expect(footer).ToBeVisibleAsync();
    }

    [Test]
    public async Task Footer_DisplaysCopyrightWithCurrentYear()
    {
        await Page.GotoAsync(BaseUrl);

        string currentYear = DateTime.Now.Year.ToString();
        ILocator footer = Page.Locator("footer#contact");

        await Expect(footer).ToContainTextAsync("©");
        await Expect(footer).ToContainTextAsync(currentYear);
        await Expect(footer).ToContainTextAsync("Chapel Hill .NET");
        await Expect(footer).ToContainTextAsync("All rights reserved");
    }

    [Test]
    public async Task Footer_DisplaysTagline()
    {
        await Page.GotoAsync(BaseUrl);

        ILocator footer = Page.Locator("footer#contact");
        await Expect(footer).ToContainTextAsync("Meetups, talks");
        await Expect(footer).ToContainTextAsync("Triangle");
    }

    [Test]
    public async Task Footer_ContainsExploreSection()
    {
        await Page.GotoAsync(BaseUrl);

        ILocator footer = Page.Locator("footer#contact");
        ILocator exploreHeading = footer.Locator("h3").Filter(new LocatorFilterOptions { HasText = "Explore" });

        await Expect(exploreHeading).ToBeVisibleAsync();
        await Expect(footer).ToContainTextAsync(".NET");
    }

    [Test]
    public async Task Footer_HasSiteFooterClass()
    {
        await Page.GotoAsync(BaseUrl);

        ILocator footer = Page.Locator("footer#contact");
        string? footerClasses = await footer.GetAttributeAsync("class");
        Assert.That(footerClasses, Does.Contain("site-footer"),
            "Footer should have the site-footer CSS class");
    }

    [Test]
    public async Task Footer_CopyrightAndTaglineAreInSeparateParagraphs()
    {
        await Page.GotoAsync(BaseUrl);

        ILocator footer = Page.Locator("footer#contact");
        ILocator copyrightParagraph = footer.Locator("p").Filter(new LocatorFilterOptions { HasText = "©" });
        ILocator taglineParagraph = footer.Locator("p").Filter(new LocatorFilterOptions { HasText = "Meetups, talks" });

        await Expect(copyrightParagraph).ToBeVisibleAsync();
        await Expect(taglineParagraph).ToBeVisibleAsync();

        string? copyrightText = await copyrightParagraph.TextContentAsync();
        Assert.That(copyrightText, Does.Not.Contain("Meetups"),
            "Copyright and tagline should be in separate paragraphs");
    }
}
