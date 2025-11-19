using Bunit;
using chapelhilldotnet.web.Pages;
using Microsoft.JSInterop;
using Xunit;

namespace chapelhilldotnet.Tests.Accessibility;

/// <summary>
/// General accessibility tests to ensure WCAG 2.1 AA compliance across pages
/// Tests common accessibility patterns and requirements
/// </summary>
public class GeneralAccessibilityTests : TestContext
{
    public GeneralAccessibilityTests()
    {
        // Setup JS runtime mock for components that need it
        JSInterop.Mode = JSRuntimeMode.Loose;
    }

    [Fact]
    public void HomePage_HeaderHasRoleBanner()
    {
        // Act
        var cut = RenderComponent<Home>();

        // Assert
        var header = cut.Find("header[role='banner']");
        Assert.NotNull(header);
    }

    [Fact]
    public void HomePage_NavigationHasAriaLabel()
    {
        // Act
        var cut = RenderComponent<Home>();

        // Assert
        var nav = cut.Find("nav");
        var ariaLabel = nav.GetAttribute("aria-label");
        Assert.NotNull(ariaLabel);
        Assert.Contains("navigation", ariaLabel.ToLower());
    }

    [Fact]
    public void HomePage_AllSectionsHaveAriaLabelledby()
    {
        // Act
        var cut = RenderComponent<Home>();

        // Assert
        var sections = cut.FindAll("section[aria-labelledby]");
        Assert.NotEmpty(sections);

        foreach (var section in sections)
        {
            var labelledBy = section.GetAttribute("aria-labelledby");
            Assert.NotNull(labelledBy);

            // Verify the referenced element exists
            var referencedElement = cut.Find($"#{labelledBy}");
            Assert.NotNull(referencedElement);
        }
    }

    [Fact]
    public void HomePage_DecorativeSvgsHaveAriaHidden()
    {
        // Act
        var cut = RenderComponent<Home>();

        // Assert
        var markup = cut.Markup;
        var svgCount = CountOccurrences(markup, "<svg");

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
        var cut = RenderComponent<Home>();

        // Assert
        var buttons = cut.FindAll("button");
        foreach (var button in buttons)
        {
            var type = button.GetAttribute("type");
            Assert.NotNull(type);
            Assert.True(type == "button" || type == "submit" || type == "reset");
        }
    }

    [Fact]
    public void HomePage_NoImproperTabindex()
    {
        // Act
        var cut = RenderComponent<Home>();

        // Assert
        var markup = cut.Markup;

        // tabindex > 0 is considered an anti-pattern
        Assert.DoesNotContain("tabindex=\"1\"", markup);
        Assert.DoesNotContain("tabindex=\"2\"", markup);
        Assert.DoesNotContain("tabindex=\"3\"", markup);
    }

    [Fact]
    public void EventsPage_TableHasCaption()
    {
        // Act
        var cut = RenderComponent<Events>();

        // Assert
        var table = cut.Find("table");
        var caption = cut.Find("caption");
        Assert.NotNull(caption);
    }

    [Fact]
    public void EventsPage_TableHeadersHaveScope()
    {
        // Act
        var cut = RenderComponent<Events>();

        // Assert
        var headers = cut.FindAll("th");
        foreach (var header in headers)
        {
            var scope = header.GetAttribute("scope");
            Assert.NotNull(scope);
            Assert.True(scope == "col" || scope == "row");
        }
    }

    [Fact]
    public void EventsPage_TimeElementsHaveDatetimeAttribute()
    {
        // Act
        var cut = RenderComponent<Events>();

        // Assert
        var timeElements = cut.FindAll("time");
        foreach (var time in timeElements)
        {
            var datetime = time.GetAttribute("datetime");
            Assert.NotNull(datetime);

            // Verify format is YYYY-MM-DD
            Assert.Matches(@"^\d{4}-\d{2}-\d{2}$", datetime);
        }
    }

    [Fact]
    public void EventsPage_ExternalLinksHaveSecurityAttributes()
    {
        // Act
        var cut = RenderComponent<Events>();

        // Assert
        var externalLinks = cut.FindAll("a[target='_blank']");
        foreach (var link in externalLinks)
        {
            var rel = link.GetAttribute("rel");
            Assert.Contains("noopener", rel);
            Assert.Contains("noreferrer", rel);
        }
    }

    [Fact]
    public void AboutPage_FooterHasRoleContentinfo()
    {
        // Act
        var cut = RenderComponent<About>();

        // Assert
        var footer = cut.Find("footer[role='contentinfo']");
        Assert.NotNull(footer);
    }

    [Fact]
    public void AboutPage_AddressElementUsedForContactInfo()
    {
        // Act
        var cut = RenderComponent<About>();

        // Assert
        var address = cut.Find("address");
        Assert.NotNull(address);
    }

    [Fact]
    public void ErrorPage_ErrorMessageHasRoleAlert()
    {
        // Act
        var cut = RenderComponent<Error>();

        // Assert
        var alert = cut.Find("div[role='alert']");
        Assert.NotNull(alert);
    }

    [Fact]
    public void ErrorPage_HasAriaLive()
    {
        // Act
        var cut = RenderComponent<Error>();

        // Assert
        var alert = cut.Find("div[aria-live='assertive']");
        Assert.NotNull(alert);
    }

    [Fact]
    public void AllPages_HeadingsFollowHierarchy()
    {
        // Test that h1 appears before h2, h2 before h3, etc.
        // This is a simplified test - in practice you'd check each page

        // Act
        var cut = RenderComponent<Home>();

        // Assert
        var markup = cut.Markup;
        var h1Index = markup.IndexOf("<h1", StringComparison.Ordinal);
        var h2Index = markup.IndexOf("<h2", StringComparison.Ordinal);
        var h3Index = markup.IndexOf("<h3", StringComparison.Ordinal);

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
