using AngleSharp.Dom;
using Bunit;
using chapelhilldotnet.web.Layout;
using chapelhilldotnet.web.Pages;

namespace chapelhilldotnet.Tests.Accessibility;

/// <summary>
/// General accessibility tests to ensure WCAG 2.1 AA compliance across pages
/// Tests common accessibility patterns and requirements
/// </summary>
public class GeneralAccessibilityTests : TestContext
{
    public GeneralAccessibilityTests()
    {
        // Set up JS runtime mock for components that need it
        JSInterop.Mode = JSRuntimeMode.Loose;
    }

    [Fact]
    public void NavMenu_HeaderHasRoleBanner()
    {
        // Act
        IRenderedComponent<NavMenu> cut = RenderComponent<NavMenu>();

        // Assert
        IElement header = cut.Find("header[role='banner']");
        Assert.NotNull(header);
    }

    [Fact]
    public void NavMenu_NavigationHasAriaLabel()
    {
        // Act
        IRenderedComponent<NavMenu> cut = RenderComponent<NavMenu>();

        // Assert
        IElement nav = cut.Find("nav");
        string? ariaLabel = nav.GetAttribute("aria-label");
        Assert.NotNull(ariaLabel);
        Assert.Contains("navigation", ariaLabel.ToLower());
    }

    [Fact]
    public void HomePage_NamedSectionsContainHeadings()
    {
        // Act
        IRenderedComponent<Home> cut = RenderComponent<Home>();

        // Assert - sections with IDs should contain heading elements
        IRefreshableElementCollection<IElement> namedSections = cut.FindAll("section[id]");
        Assert.NotEmpty(namedSections);

        foreach (IElement section in namedSections)
        {
            string inner = section.InnerHtml;
            Assert.True(
                inner.Contains("<h1", StringComparison.OrdinalIgnoreCase) ||
                inner.Contains("<h2", StringComparison.OrdinalIgnoreCase) ||
                inner.Contains("<h3", StringComparison.OrdinalIgnoreCase),
                $"Section #{section.GetAttribute("id")} should contain a heading element");
        }
    }

    [Fact]
    public void HomePage_DecorativeSvgsHaveAriaHidden()
    {
        // Act
        IRenderedComponent<Home> cut = RenderComponent<Home>();

        // Assert
        string markup = cut.Markup;
        int svgCount = CountOccurrences(markup, "<svg");

        // Decorative SVGs should have aria-hidden="true"
        if (svgCount > 0)
        {
            Assert.Contains("aria-hidden=\"true\"", markup);
        }
    }

    [Fact]
    public void HomePage_AllButtonsHaveTypeAttribute()
    {
        // Act
        IRenderedComponent<Home> cut = RenderComponent<Home>();

        // Assert
        IRefreshableElementCollection<IElement> buttons = cut.FindAll("button");
        foreach (IElement button in buttons)
        {
            string? type = button.GetAttribute("type");
            Assert.NotNull(type);
            Assert.True(type is "button" or "submit" or "reset");
        }
    }

    [Fact]
    public void HomePage_NoImproperTabindex()
    {
        // Act
        IRenderedComponent<Home> cut = RenderComponent<Home>();

        // Assert
        string markup = cut.Markup;

        // tabindex > 0 is considered an antipattern
        Assert.DoesNotContain("tabindex=\"1\"", markup);
        Assert.DoesNotContain("tabindex=\"2\"", markup);
        Assert.DoesNotContain("tabindex=\"3\"", markup);
    }

    [Fact]
    public void EventsPage_TimeElementsHaveDatetimeAttribute()
    {
        // Act
        IRenderedComponent<Events> cut = RenderComponent<Events>();

        // Assert
        IRefreshableElementCollection<IElement> timeElements = cut.FindAll("time");
        foreach (IElement time in timeElements)
        {
            string? datetime = time.GetAttribute("datetime");
            Assert.NotNull(datetime);

            // Verify format is YYYY-MM-DD
            Assert.Matches(@"^\d{4}-\d{2}-\d{2}$", datetime);
        }
    }

    [Fact]
    public void EventsPage_ExternalLinksHaveSecurityAttributes()
    {
        // Act
        IRenderedComponent<Events> cut = RenderComponent<Events>();

        // Assert
        IRefreshableElementCollection<IElement> externalLinks = cut.FindAll("a[target='_blank']");
        foreach (IElement link in externalLinks)
        {
            string? rel = link.GetAttribute("rel");
            Assert.Contains("noopener", rel);
            Assert.Contains("noreferrer", rel);
        }
    }

    [Fact]
    public void Footer_HasRoleContentInfo()
    {
        // Act
        IRenderedComponent<Footer> cut = RenderComponent<Footer>();

        // Assert
        IElement footer = cut.Find("footer[role='contentinfo']");
        Assert.NotNull(footer);
    }

    [Fact]
    public void ErrorPage_ErrorMessageHasRoleAlert()
    {
        // Act
        IRenderedComponent<Error> cut = RenderComponent<Error>();

        // Assert
        IElement alert = cut.Find("div[role='alert']");
        Assert.NotNull(alert);
    }

    [Fact]
    public void ErrorPage_HasAriaLive()
    {
        // Act
        IRenderedComponent<Error> cut = RenderComponent<Error>();

        // Assert
        IElement alert = cut.Find("div[aria-live='assertive']");
        Assert.NotNull(alert);
    }

    [Fact]
    public void AllPages_HeadingsFollowHierarchy()
    {
        // Test that h1 appears before h2, h2 before h3, etc.
        // This is a simplified test - in practice you'd check each page

        // Act
        IRenderedComponent<Home> cut = RenderComponent<Home>();

        // Assert
        string markup = cut.Markup;
        int h1Index = markup.IndexOf("<h1", StringComparison.Ordinal);
        int h2Index = markup.IndexOf("<h2", StringComparison.Ordinal);
        int h3Index = markup.IndexOf("<h3", StringComparison.Ordinal);

        // h1 should come before h2
        if (h1Index > -1 && h2Index > -1)
        {
            Assert.True(h1Index < h2Index, "h1 should appear before h2");
        }

        // h2 should come before h3
        if (h2Index > -1 && h3Index > -1)
        {
            Assert.True(h2Index < h3Index, "h2 should appear before h3");
        }
    }

    private static int CountOccurrences(string text, string pattern)
    {
        int count = 0;
        int index = 0;
        while ((index = text.IndexOf(pattern, index, StringComparison.Ordinal)) != -1)
        {
            count++;
            index += pattern.Length;
        }

        return count;
    }
}
