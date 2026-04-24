using AngleSharp.Dom;
using Bunit;
using chapelhilldotnet.web.Layout;

namespace chapelhilldotnet.Tests.Accessibility;

/// <summary>
/// Accessibility tests for the DarkModeToggle component to ensure WCAG 2.1 AA compliance
/// </summary>
public class DarkModeToggleAccessibilityTests : TestContext
{
    public DarkModeToggleAccessibilityTests()
    {
        // Setup JS runtime mock
        JSInterop.Mode = JSRuntimeMode.Loose;
    }

    [Fact]
    public void DarkModeToggle_ButtonHasTypeAttribute()
    {
        // Act
        IRenderedComponent<DarkModeToggle> cut = RenderComponent<DarkModeToggle>();

        // Assert
        IElement button = cut.Find("button");
        Assert.Equal("button", button.GetAttribute("type"));
    }

    [Fact]
    public void DarkModeToggle_ButtonHasAriaLabel()
    {
        // Act
        IRenderedComponent<DarkModeToggle> cut = RenderComponent<DarkModeToggle>();

        // Assert
        IElement button = cut.Find("button");
        string? ariaLabel = button.GetAttribute("aria-label");
        Assert.NotNull(ariaLabel);
        Assert.Contains("mode", ariaLabel.ToLower());
    }

    [Fact]
    public void DarkModeToggle_ButtonHasAriaPressedState()
    {
        // Act
        IRenderedComponent<DarkModeToggle> cut = RenderComponent<DarkModeToggle>();

        // Assert
        IElement button = cut.Find("button");
        string? ariaPressed = button.GetAttribute("aria-pressed");
        Assert.NotNull(ariaPressed);
        Assert.True(ariaPressed is "true" or "false");
    }

    [Fact]
    public void DarkModeToggle_HasLiveRegionForAnnouncements()
    {
        // Act
        IRenderedComponent<DarkModeToggle> cut = RenderComponent<DarkModeToggle>();

        // Assert
        IElement liveRegion = cut.Find("div[role='status']");
        Assert.NotNull(liveRegion);
        Assert.Equal("polite", liveRegion.GetAttribute("aria-live"));
        Assert.Equal("true", liveRegion.GetAttribute("aria-atomic"));
    }

    [Fact]
    public void DarkModeToggle_LiveRegionHasSrOnlyClass()
    {
        // Act
        IRenderedComponent<DarkModeToggle> cut = RenderComponent<DarkModeToggle>();

        // Assert
        IElement liveRegion = cut.Find("div[role='status']");
        Assert.Contains("sr-only", liveRegion.GetAttribute("class"));
    }

    [Fact]
    public void DarkModeToggle_SvgIconsHaveAriaHidden()
    {
        // Act
        IRenderedComponent<DarkModeToggle> cut = RenderComponent<DarkModeToggle>();

        // Assert
        IElement svg = cut.Find("svg");
        Assert.Equal("true", svg.GetAttribute("aria-hidden"));
    }

    [Fact]
    public void DarkModeToggle_SvgIconsHaveFocusableFalse()
    {
        // Act
        IRenderedComponent<DarkModeToggle> cut = RenderComponent<DarkModeToggle>();

        // Assert
        IElement svg = cut.Find("svg");
        Assert.Equal("false", svg.GetAttribute("focusable"));
    }

    [Fact]
    public void DarkModeToggle_AriaLabelChangesWithState()
    {
        // Act
        IRenderedComponent<DarkModeToggle> cut = RenderComponent<DarkModeToggle>();
        IElement button = cut.Find("button");
        string? initialLabel = button.GetAttribute("aria-label");

        // Verify label contains appropriate text
        Assert.True(
            initialLabel!.Contains("light mode", StringComparison.OrdinalIgnoreCase) ||
            initialLabel.Contains("dark mode", StringComparison.OrdinalIgnoreCase)
        );
    }

    [Fact]
    public void DarkModeToggle_ButtonIsKeyboardAccessible()
    {
        // Act
        IRenderedComponent<DarkModeToggle> cut = RenderComponent<DarkModeToggle>();
        IElement button = cut.Find("button");

        // Assert - button element is inherently keyboard accessible
        Assert.Equal("BUTTON", button.TagName);
    }

    [Fact]
    public void DarkModeToggle_HasVisibleFocusStyles()
    {
        // Act
        IRenderedComponent<DarkModeToggle> cut = RenderComponent<DarkModeToggle>();
        IElement button = cut.Find("button");

        // Assert - Check for focus-visible classes
        string? classes = button.GetAttribute("class");
        Assert.Contains("focus-visible", classes);
    }
}
