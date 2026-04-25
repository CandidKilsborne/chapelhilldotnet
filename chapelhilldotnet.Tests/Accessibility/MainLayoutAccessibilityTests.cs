using Bunit;
using chapelhilldotnet.web.Layout;

namespace chapelhilldotnet.Tests.Accessibility;

/// <summary>
/// Accessibility tests for MainLayout component to ensure WCAG 2.1 AA compliance
/// </summary>
public class MainLayoutAccessibilityTests : TestContext
{
    public MainLayoutAccessibilityTests()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
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
    public void MainLayout_UsesPageShellWrapper()
    {
        // Act
        var cut = RenderComponent<MainLayout>();

        // Assert - the layout uses a site-shell div as the top-level wrapper
        var shell = cut.Find(".site-shell");
        Assert.NotNull(shell);
    }

    [Fact]
    public void MainLayout_IncludesFooter()
    {
        // Act
        var cut = RenderComponent<MainLayout>();

        // Assert
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

        // Header landmark should exist from NavMenu
        var header = cut.Find("header[role='banner']");
        Assert.NotNull(header);
    }
}
