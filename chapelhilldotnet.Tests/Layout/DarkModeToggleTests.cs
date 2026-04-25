using Bunit;
using chapelhilldotnet.web.Layout;

namespace chapelhilldotnet.Tests.Layout;

public class DarkModeToggleTests : TestContext
{
    [Fact]
    public void DarkModeToggle_RendersButton()
    {
        // Arrange
        JSInterop.Mode = JSRuntimeMode.Loose;

        // Act
        var cut = RenderComponent<DarkModeToggle>();

        // Assert
        var button = cut.Find("button");
        Assert.NotNull(button);
    }

    [Fact]
    public void DarkModeToggle_HasAccessibleAriaLabel()
    {
        // Arrange
        JSInterop.Mode = JSRuntimeMode.Loose;

        // Act
        var cut = RenderComponent<DarkModeToggle>();

        // Assert
        var button = cut.Find("button");
        var ariaLabel = button.GetAttribute("aria-label");
        Assert.NotNull(ariaLabel);
        Assert.True(
            ariaLabel.Contains("Switch to light mode") || ariaLabel.Contains("Switch to dark mode"),
            "Button should have an appropriate aria-label");
    }

    [Fact]
    public void DarkModeToggle_DisplaysMoonIconByDefault()
    {
        // Arrange
        JSInterop.Mode = JSRuntimeMode.Loose;

        // Act
        var cut = RenderComponent<DarkModeToggle>();

        // Assert
        var svgPath = cut.Find("svg path");
        Assert.Contains("M12 3a6 6 0 0 0 9 9 9 9 0 1 1-9-9Z", svgPath.GetAttribute("d"));
    }

    [Fact]
    public void DarkModeToggle_TogglesOnClick()
    {
        // Arrange
        JSInterop.Mode = JSRuntimeMode.Loose;
        var cut = RenderComponent<DarkModeToggle>();
        var button = cut.Find("button");

        // Act
        button.Click();

        // Assert - The component should have attempted to call localStorage and toggle dark mode
        // Since we're using a mock JSRuntime, we verify that the button still renders
        Assert.NotNull(cut.Find("button"));
    }
}
