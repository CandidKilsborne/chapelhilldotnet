using Bunit;
using chapelhilldotnet.web.Layout;

namespace chapelhilldotnet.Tests.Accessibility;

/// <summary>
/// Accessibility tests for the NavMenu component to ensure WCAG 2.1 AA compliance
/// </summary>
public class NavMenuAccessibilityTests : TestContext
{
    public NavMenuAccessibilityTests()
    {
        JSInterop.Mode = JSRuntimeMode.Loose;
    }

    [Fact]
    public void NavMenu_UsesButtonInsteadOfCheckbox()
    {
        // Act
        var cut = RenderComponent<NavMenu>();

        // Assert
        var button = cut.Find("button.site-nav-toggle");
        Assert.NotNull(button);

        // Ensure no checkbox exists
        Assert.Throws<ElementNotFoundException>(() => cut.Find("input[type='checkbox']"));
    }

    [Fact]
    public void NavMenu_ToggleButtonHasTypeAttribute()
    {
        // Act
        var cut = RenderComponent<NavMenu>();

        // Assert
        var button = cut.Find("button.site-nav-toggle");
        Assert.Equal("button", button.GetAttribute("type"));
    }

    [Fact]
    public void NavMenu_ToggleButtonHasAriaLabel()
    {
        // Act
        var cut = RenderComponent<NavMenu>();

        // Assert
        var button = cut.Find("button.site-nav-toggle");
        var ariaLabel = button.GetAttribute("aria-label");
        Assert.NotNull(ariaLabel);
        Assert.Contains("navigation", ariaLabel.ToLower());
    }

    [Fact]
    public void NavMenu_ToggleButtonHasAriaExpanded()
    {
        // Act
        var cut = RenderComponent<NavMenu>();

        // Assert
        var button = cut.Find("button.site-nav-toggle");
        var ariaExpanded = button.GetAttribute("aria-expanded");
        Assert.NotNull(ariaExpanded);
        Assert.True(ariaExpanded == "true" || ariaExpanded == "false");
    }

    [Fact]
    public void NavMenu_AriaExpandedReflectsMenuState()
    {
        // Act
        var cut = RenderComponent<NavMenu>();
        var button = cut.Find("button.site-nav-toggle");

        // Assert - Initially collapsed, so aria-expanded should be "false"
        Assert.Equal("false", button.GetAttribute("aria-expanded"));

        // Click to expand
        button.Click();

        // Assert - Now expanded, so aria-expanded should be "true"
        Assert.Equal("true", button.GetAttribute("aria-expanded"));

        // Click to collapse again
        button.Click();

        // Assert - Back to collapsed, aria-expanded should be "false"
        Assert.Equal("false", button.GetAttribute("aria-expanded"));
    }

    [Fact]
    public void NavMenu_ToggleButtonHasAriaControls()
    {
        // Act
        var cut = RenderComponent<NavMenu>();

        // Assert
        var button = cut.Find("button.site-nav-toggle");
        var ariaControls = button.GetAttribute("aria-controls");
        Assert.Equal("main-navigation", ariaControls);
    }

    [Fact]
    public void NavMenu_NavigationHasMatchingId()
    {
        // Act
        var cut = RenderComponent<NavMenu>();

        // Assert
        var nav = cut.Find("#main-navigation");
        Assert.NotNull(nav);
    }

    [Fact]
    public void NavMenu_NavigationHasAriaLabel()
    {
        // Act
        var cut = RenderComponent<NavMenu>();

        // Assert
        var nav = cut.Find("nav");
        var ariaLabel = nav.GetAttribute("aria-label");
        Assert.NotNull(ariaLabel);
        Assert.Contains("navigation", ariaLabel.ToLower());
    }

    [Fact]
    public void NavMenu_TogglingChangesAriaExpandedState()
    {
        // Act
        var cut = RenderComponent<NavMenu>();
        var button = cut.Find("button.site-nav-toggle");

        // Get initial state
        var initialExpanded = button.GetAttribute("aria-expanded");

        // Click the button
        button.Click();

        // Get new state
        var newExpanded = button.GetAttribute("aria-expanded");

        // Assert - State should have changed
        Assert.NotEqual(initialExpanded, newExpanded);
    }

    [Fact]
    public void NavMenu_TogglingChangesCssClasses()
    {
        // Act
        var cut = RenderComponent<NavMenu>();
        var navContainer = cut.Find("#main-navigation");
        var button = cut.Find("button.site-nav-toggle");

        // Get initial classes
        var initialClasses = navContainer.GetAttribute("class");

        // Click the button
        button.Click();

        // Get new classes
        var newClasses = navContainer.GetAttribute("class");

        // Assert - Classes should have changed
        Assert.NotEqual(initialClasses, newClasses);
    }

    [Fact]
    public void NavMenu_IconsHaveAriaHidden()
    {
        // Act
        var cut = RenderComponent<NavMenu>();

        // Assert - Theme toggle SVG icons inside the nav should have aria-hidden
        var markup = cut.Markup;
        Assert.Contains("aria-hidden=\"true\"", markup);
    }

    [Fact]
    public void NavMenu_AllNavLinksAreKeyboardAccessible()
    {
        // Act
        var cut = RenderComponent<NavMenu>();

        // Assert
        var navLinks = cut.FindAll(".site-nav__link");
        Assert.NotEmpty(navLinks);

        // All NavLink components render as anchor tags, which are keyboard accessible
        foreach (var link in navLinks)
        {
            Assert.Equal("A", link.TagName);
        }
    }

    [Fact]
    public void NavMenu_StartsInCollapsedState()
    {
        // Act
        var cut = RenderComponent<NavMenu>();
        var button = cut.Find("button.site-nav-toggle");

        // Assert - Menu starts collapsed, aria-expanded should be "false"
        var ariaExpanded = button.GetAttribute("aria-expanded");
        Assert.Equal("false", ariaExpanded);
    }

    [Fact]
    public void NavMenu_ContainsTogglerBars()
    {
        // Act
        var cut = RenderComponent<NavMenu>();

        // Assert
        var togglerBars = cut.FindAll(".site-nav-toggle__bar");
        Assert.NotEmpty(togglerBars);
    }
}
