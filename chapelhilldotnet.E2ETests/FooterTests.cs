namespace chapelhilldotnet.E2ETests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class FooterTests : BlazorPageTest
{
    [Test]
    public async Task Footer_IsVisible()
    {
        await Page.GotoAsync(BaseUrl);

        var footer = Page.Locator("footer#contact");
        await Expect(footer).ToBeVisibleAsync();
    }

    [Test]
    public async Task Footer_DisplaysCopyrightWithCurrentYear()
    {
        await Page.GotoAsync(BaseUrl);

        var currentYear = DateTime.Now.Year.ToString();
        var footer = Page.Locator("footer#contact");

        await Expect(footer).ToContainTextAsync("©");
        await Expect(footer).ToContainTextAsync(currentYear);
        await Expect(footer).ToContainTextAsync("Chapel Hill .NET");
        await Expect(footer).ToContainTextAsync("All rights reserved");
    }

    [Test]
    public async Task Footer_DisplaysTagline()
    {
        await Page.GotoAsync(BaseUrl);

        var footer = Page.Locator("footer#contact");
        await Expect(footer).ToContainTextAsync("Meetups, talks");
        await Expect(footer).ToContainTextAsync("Triangle");
    }

    [Test]
    public async Task Footer_ContainsExploreSection()
    {
        await Page.GotoAsync(BaseUrl);

        var footer = Page.Locator("footer#contact");
        var exploreHeading = footer.Locator("h3").Filter(new() { HasText = "Explore" });

        await Expect(exploreHeading).ToBeVisibleAsync();
        await Expect(footer).ToContainTextAsync(".NET");
    }

    [Test]
    public async Task Footer_HasSiteFooterClass()
    {
        await Page.GotoAsync(BaseUrl);

        var footer = Page.Locator("footer#contact");
        var footerClasses = await footer.GetAttributeAsync("class");
        Assert.That(footerClasses, Does.Contain("site-footer"),
            "Footer should have the site-footer CSS class");
    }

    [Test]
    public async Task Footer_CopyrightAndTaglineAreInSeparateParagraphs()
    {
        await Page.GotoAsync(BaseUrl);

        var footer = Page.Locator("footer#contact");
        var copyrightParagraph = footer.Locator("p").Filter(new() { HasText = "©" });
        var taglineParagraph = footer.Locator("p").Filter(new() { HasText = "Meetups, talks" });

        await Expect(copyrightParagraph).ToBeVisibleAsync();
        await Expect(taglineParagraph).ToBeVisibleAsync();

        var copyrightText = await copyrightParagraph.TextContentAsync();
        Assert.That(copyrightText, Does.Not.Contain("Meetups"),
            "Copyright and tagline should be in separate paragraphs");
    }
}
