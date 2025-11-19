using Bunit;
using chapelhilldotnet.web.Layout;
using Xunit;

namespace chapelhilldotnet.Tests.Accessibility;

/// <summary>
/// Accessibility tests for MainLayout component to ensure WCAG 2.1 AA compliance
/// </summary>
public class MainLayoutAccessibilityTests : TestContext
{
    [Fact]
    public void MainLayout_HasSkipNavigationLink()
    {
        // Act
        var cut = RenderComponent<MainLayout>();

        // Assert
        var skipLink = cut.Find("a.skip-link");
        Assert.NotNull(skipLink);
        Assert.Equal("Skip to main content", skipLink.TextContent);
    }

    [Fact]
    public void MainLayout_SkipLinkPointsToMainContent()
    {
        // Act
        var cut = RenderComponent<MainLayout>();

        // Assert
        var skipLink = cut.Find("a.skip-link");
        Assert.Equal("#main-content", skipLink.GetAttribute("href"));
    }

    [Fact]
    public void MainLayout_MainContentHasMatchingId()
    {
        // Act
        var cut = RenderComponent<MainLayout>();

        // Assert
        var main = cut.Find("#main-content");
        Assert.NotNull(main);
    }

    [Fact]
    public void MainLayout_MainHasRoleAttribute()
    {
        // Act
        var cut = RenderComponent<MainLayout>();

        // Assert
        var main = cut.Find("main");
        Assert.Equal("main", main.GetAttribute("role"));
    }

    [Fact]
    public void MainLayout_MainHasAriaLabel()
    {
        // Act
        var cut = RenderComponent<MainLayout>();

        // Assert
        var main = cut.Find("main");
        var ariaLabel = main.GetAttribute("aria-label");
        Assert.NotNull(ariaLabel);
        Assert.Contains("content", ariaLabel.ToLower());
    }

    [Fact]
    public void MainLayout_UsesSemanticMainElement()
    {
        // Act
        var cut = RenderComponent<MainLayout>();

        // Assert
        var main = cut.Find("main");
        Assert.NotNull(main);
        Assert.Equal("MAIN", main.TagName);
    }

    [Fact]
    public void MainLayout_UsesSemanticArticleElement()
    {
        // Act
        var cut = RenderComponent<MainLayout>();

        // Assert
        var article = cut.Find("article");
        Assert.NotNull(article);
    }

    [Fact]
    public void MainLayout_IncludesFooter()
    {
        // Act
        var cut = RenderComponent<MainLayout>();

        // Assert
        // Footer is rendered as a component, verify it's included
        var markup = cut.Markup;
        Assert.Contains("Footer", markup);
    }

    [Fact]
    public void MainLayout_SkipLinkIsFirstElement()
    {
        // Act
        var cut = RenderComponent<MainLayout>();

        // Assert
        var markup = cut.Markup;
        var skipLinkIndex = markup.IndexOf("skip-link", StringComparison.Ordinal);
        var mainContentIndex = markup.IndexOf("main-content", StringComparison.Ordinal);

        // Skip link should appear before main content
        Assert.True(skipLinkIndex < mainContentIndex);
    }

    [Fact]
    public void MainLayout_HasProperLandmarkStructure()
    {
        // Act
        var cut = RenderComponent<MainLayout>();

        // Assert
        var main = cut.Find("main[role='main']");
        Assert.NotNull(main);

        // Main element should contain the article
        var article = cut.Find("main article");
        Assert.NotNull(article);
    }
}
