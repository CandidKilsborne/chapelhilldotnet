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
        var footer = cut.Find("footer");
        Assert.NotNull(footer);
        Assert.Equal("contentinfo", footer.GetAttribute("role"));
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
