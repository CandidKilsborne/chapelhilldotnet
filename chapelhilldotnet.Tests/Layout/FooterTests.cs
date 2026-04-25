using Bunit;
using chapelhilldotnet.web.Layout;

namespace chapelhilldotnet.Tests.Layout;

public class FooterTests : TestContext
{
    [Fact]
    public void Footer_RendersFooterElement()
    {
        // Act
        var cut = RenderComponent<Footer>();

        // Assert
        var footer = cut.Find("footer");
        Assert.NotNull(footer);
    }

    [Fact]
    public void Footer_DisplaysCopyright()
    {
        // Act
        var cut = RenderComponent<Footer>();

        // Assert
        var copyrightText = cut.Find("footer").TextContent;
        Assert.Contains("©", copyrightText);
        Assert.Contains("Chapel Hill .NET", copyrightText);
        Assert.Contains("All rights reserved", copyrightText);
    }

    [Fact]
    public void Footer_DisplaysCurrentYear()
    {
        // Act
        var cut = RenderComponent<Footer>();
        var currentYear = DateTime.Now.Year.ToString();

        // Assert
        var footerText = cut.Find("footer").TextContent;
        Assert.Contains(currentYear, footerText);
    }

    [Fact]
    public void Footer_DisplaysTagline()
    {
        // Act
        var cut = RenderComponent<Footer>();

        // Assert
        var footerText = cut.Find("footer").TextContent;
        Assert.Contains("Meetups, talks", footerText);
        Assert.Contains("Triangle", footerText);
    }

    [Fact]
    public void Footer_HasExploreSection()
    {
        // Act
        var cut = RenderComponent<Footer>();

        // Assert
        var exploreSection = cut.FindAll("h3").FirstOrDefault(h => h.TextContent.Contains("Explore"));
        Assert.NotNull(exploreSection);

        var footerText = cut.Find("footer").TextContent;
        Assert.Contains(".NET", footerText);
    }

    [Fact]
    public void Footer_HasCorrectId()
    {
        // Act
        var cut = RenderComponent<Footer>();

        // Assert
        var footer = cut.Find("footer");
        var id = footer.GetAttribute("id");
        Assert.Equal("contact", id);
    }
}
