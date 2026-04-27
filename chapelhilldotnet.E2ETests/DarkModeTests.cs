using Microsoft.Playwright;

namespace chapelhilldotnet.E2ETests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class DarkModeTests : BlazorPageTest
{
    [Test]
    public async Task DarkModeToggle_IsVisible()
    {
        await Page.GotoAsync(BaseUrl);

        // Find the dark mode toggle button by aria-label
        ILocator toggleButton = Page.Locator("button[aria-label*='dark mode' i], button[aria-label*='light mode' i]");
        await Expect(toggleButton).ToBeVisibleAsync();
    }

    [Test]
    public async Task DarkModeToggle_ChangesTheme()
    {
        await Page.GotoAsync(BaseUrl);

        // Find the dark mode toggle button
        ILocator toggleButton =
            Page.Locator("button[aria-label*='dark mode' i], button[aria-label*='light mode' i]").First;

        // Get the initial state of the html element's class
        ILocator htmlElement = Page.Locator("html");
        string? initialClasses = await htmlElement.GetAttributeAsync("class");
        bool initialIsDark = initialClasses?.Contains("dark") ?? false;

        // Click the toggle
        await toggleButton.ClickAsync();

        // Wait for theme to change
        await Page.WaitForTimeoutAsync(500);

        // Get the new state
        string? newClasses = await htmlElement.GetAttributeAsync("class");
        bool newIsDark = newClasses?.Contains("dark") ?? false;

        // Verify the theme changed
        Assert.That(newIsDark, Is.Not.EqualTo(initialIsDark), "Theme should have toggled");
    }

    [Test]
    public async Task DarkModeToggle_PersistsPreference()
    {
        await Page.GotoAsync(BaseUrl);

        // Find and click the dark mode toggle
        ILocator toggleButton =
            Page.Locator("button[aria-label*='dark mode' i], button[aria-label*='light mode' i]").First;
        await toggleButton.ClickAsync();

        // Wait for theme to change
        await Page.WaitForTimeoutAsync(500);

        // Get the current dark mode state
        ILocator htmlElement = Page.Locator("html");
        string? classesAfterToggle = await htmlElement.GetAttributeAsync("class");
        bool isDarkAfterToggle = classesAfterToggle?.Contains("dark") ?? false;

        // Reload the page
        await Page.ReloadAsync();

        // Check if the preference persisted
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        string? classesAfterReload = await htmlElement.GetAttributeAsync("class");
        bool isDarkAfterReload = classesAfterReload?.Contains("dark") ?? false;

        Assert.That(isDarkAfterReload, Is.EqualTo(isDarkAfterToggle),
            "Dark mode preference should persist after page reload");
    }

    [Test]
    public async Task DarkMode_AppliesCorrectStyling()
    {
        await Page.GotoAsync(BaseUrl);

        // Ensure we're in dark mode
        ILocator htmlElement = Page.Locator("html");
        string? initialClasses = await htmlElement.GetAttributeAsync("class");

        if (!(initialClasses?.Contains("dark") ?? false))
        {
            ILocator toggleButton = Page
                .Locator("button[aria-label*='dark mode' i], button[aria-label*='light mode' i]")
                .First;
            await toggleButton.ClickAsync();
            await Page.WaitForTimeoutAsync(500);
        }

        // Verify dark mode is active
        string? darkClasses = await htmlElement.GetAttributeAsync("class");
        Assert.That(darkClasses, Does.Contain("dark"), "HTML element should have 'dark' class");
    }
}
