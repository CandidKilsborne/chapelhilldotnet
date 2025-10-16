using Microsoft.Playwright.NUnit;
using Microsoft.Playwright;
using NUnit.Framework;

namespace chapelhilldotnet.E2ETests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class FooterTests : PageTest
{
    private const string BaseUrl = "http://localhost:5000";

    [Test]
    public async Task Footer_IsVisible()
    {
        await Page.GotoAsync(BaseUrl);
        
        // Find the footer element
        var footer = Page.Locator("footer#contact");
        await Expect(footer).ToBeVisibleAsync();
    }

    [Test]
    public async Task Footer_DisplaysCopyrightWithCurrentYear()
    {
        await Page.GotoAsync(BaseUrl);
        
        var currentYear = DateTime.Now.Year.ToString();
        var footer = Page.Locator("footer#contact");
        
        // Check for copyright symbol and year
        await Expect(footer).ToContainTextAsync("©");
        await Expect(footer).ToContainTextAsync(currentYear);
        await Expect(footer).ToContainTextAsync("Chapel Hill .NET");
        await Expect(footer).ToContainTextAsync("All rights reserved");
    }

    [Test]
    public async Task Footer_DisplaysMadeWithLoveMessage()
    {
        await Page.GotoAsync(BaseUrl);
        
        var footer = Page.Locator("footer#contact");
        
        // Check for the "Made with ❤️ in Chapel Hill, NC" message
        await Expect(footer).ToContainTextAsync("Made with");
        await Expect(footer).ToContainTextAsync("in Chapel Hill, NC");
    }

    [Test]
    public async Task Footer_ContainsAboutUsSection()
    {
        await Page.GotoAsync(BaseUrl);
        
        var footer = Page.Locator("footer#contact");
        var aboutUsHeading = footer.Locator("h3").Filter(new() { HasText = "About Us" });
        
        await Expect(aboutUsHeading).ToBeVisibleAsync();
        await Expect(footer).ToContainTextAsync("community of .NET and Azure enthusiasts");
    }

    [Test]
    public async Task Footer_HasDarkModeSupport()
    {
        await Page.GotoAsync(BaseUrl);
        
        var footer = Page.Locator("footer#contact");
        
        // Check if footer has dark mode classes
        var footerClasses = await footer.GetAttributeAsync("class");
        Assert.That(footerClasses, Does.Contain("dark:bg-gray-900"), 
            "Footer should have dark mode background class");
    }

    [Test]
    public async Task Footer_CopyrightAndLoveMessageAreInSeparateParagraphs()
    {
        await Page.GotoAsync(BaseUrl);
        
        var footer = Page.Locator("footer#contact");
        var copyrightParagraph = footer.Locator("p").Filter(new() { HasText = "©" });
        var loveParagraph = footer.Locator("p").Filter(new() { HasText = "Made with" });
        
        await Expect(copyrightParagraph).ToBeVisibleAsync();
        await Expect(loveParagraph).ToBeVisibleAsync();
        
        // Verify they are separate elements
        var copyrightText = await copyrightParagraph.TextContentAsync();
        Assert.That(copyrightText, Does.Not.Contain("Made with"), 
            "Copyright and love message should be in separate paragraphs");
    }
}
