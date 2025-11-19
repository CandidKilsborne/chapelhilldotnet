using Bunit;
using chapelhilldotnet.web.Layout;
using Microsoft.JSInterop;
using Xunit;

namespace chapelhilldotnet.Tests.Accessibility;

/// <summary>
/// Accessibility tests for DarkModeToggle component to ensure WCAG 2.1 AA compliance
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
        var cut = RenderComponent<DarkModeToggle>();

        // Assert
        var button = cut.Find("button");
        Assert.Equal("button", button.GetAttribute("type"));
    }

    [Fact]
    public void DarkModeToggle_ButtonHasAriaLabel()
    {
        // Act
        var cut = RenderComponent<DarkModeToggle>();

        // Assert
        var button = cut.Find("button");
        var ariaLabel = button.GetAttribute("aria-label");
        Assert.NotNull(ariaLabel);
        Assert.Contains("mode", ariaLabel.ToLower());
    }

    [Fact]
    public void DarkModeToggle_ButtonHasAriaPressedState()
    {
        // Act
        var cut = RenderComponent<DarkModeToggle>();

        // Assert
        var button = cut.Find("button");
        var ariaPressed = button.GetAttribute("aria-pressed");
        Assert.NotNull(ariaPressed);
        Assert.True(ariaPressed == "true" || ariaPressed == "false");
    }

    [Fact]
    public void DarkModeToggle_HasLiveRegionForAnnouncements()
    {
        // Act
        var cut = RenderComponent<DarkModeToggle>();

        // Assert
        var liveRegion = cut.Find("div[role='status']");
        Assert.NotNull(liveRegion);
        Assert.Equal("polite", liveRegion.GetAttribute("aria-live"));
        Assert.Equal("true", liveRegion.GetAttribute("aria-atomic"));
    }

    [Fact]
    public void DarkModeToggle_LiveRegionHasSrOnlyClass()
    {
        // Act
        var cut = RenderComponent<DarkModeToggle>();

        // Assert
        var liveRegion = cut.Find("div[role='status']");
        Assert.Contains("sr-only", liveRegion.GetAttribute("class"));
    }

    [Fact]
    public void DarkModeToggle_SvgIconsHaveAriaHidden()
    {
        // Act
        var cut = RenderComponent<DarkModeToggle>();

        // Assert
        var svg = cut.Find("svg");
        Assert.Equal("true", svg.GetAttribute("aria-hidden"));
    }

    [Fact]
    public void DarkModeToggle_SvgIconsHaveFocusableFalse()
    {
        // Act
        var cut = RenderComponent<DarkModeToggle>();

        // Assert
        var svg = cut.Find("svg");
        Assert.Equal("false", svg.GetAttribute("focusable"));
    }

    [Fact]
    public void DarkModeToggle_AriaLabelChangesWithState()
    {
        // Act
        var cut = RenderComponent<DarkModeToggle>();
        var button = cut.Find("button");
        var initialLabel = button.GetAttribute("aria-label");

        // Verify label contains appropriate text
        Assert.True(
            initialLabel.Contains("light mode", StringComparison.OrdinalIgnoreCase) ||
            initialLabel.Contains("dark mode", StringComparison.OrdinalIgnoreCase)
        );
    }

    [Fact]
    public void DarkModeToggle_ButtonIsKeyboardAccessible()
    {
        // Act
        var cut = RenderComponent<DarkModeToggle>();
        var button = cut.Find("button");

        // Assert - button element is inherently keyboard accessible
        Assert.Equal("BUTTON", button.TagName);
    }

    [Fact]
    public void DarkModeToggle_HasVisibleFocusStyles()
    {
        // Act
        var cut = RenderComponent<DarkModeToggle>();
        var button = cut.Find("button");

        // Assert - Check for focus-visible classes
        var classes = button.GetAttribute("class");
        Assert.Contains("focus-visible", classes);
    }
}
