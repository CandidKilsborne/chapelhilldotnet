// using Microsoft.Playwright;
// using NUnit.Framework;

// namespace chapelhilldotnet.tests.playwright;

// [TestFixture]
// public class DarkModeTests : BaseTest
// {
//     [Test]
//     public async Task DarkModeToggle_ShouldSwitchThemes()
//     {
//         // Arrange
//         await Page.GotoAsync(BaseUrl);

//         // Get initial theme state
//         var htmlElement = Page.Locator("html");
//         var initialHasDarkClass = await htmlElement.GetAttributeAsync("class");

//         // Act - Click dark mode toggle
//         var darkModeToggle = Page.Locator("button[aria-label*='mode']");
//         await darkModeToggle.ClickAsync();

//         // Assert - Theme should have changed
//         await Page.WaitForTimeoutAsync(500); // Give time for the toggle to complete
//         var newHasDarkClass = await htmlElement.GetAttributeAsync("class");

//         Assert.That(initialHasDarkClass?.Contains("dark"), Is.Not.EqualTo(newHasDarkClass?.Contains("dark")));
//     }

//     [Test]
//     public async Task DarkMode_ShouldPersistAcrossPageReloads()
//     {
//         // Arrange
//         await Page.GotoAsync(BaseUrl);

//         // Act - Toggle to dark mode
//         var darkModeToggle = Page.Locator("button[aria-label*='mode']");
//         await darkModeToggle.ClickAsync();
//         await Page.WaitForTimeoutAsync(500);

//         // Check if dark mode is active
//         var htmlElement = Page.Locator("html");
//         var isDarkMode = await htmlElement.GetAttributeAsync("class");

//         // Reload page
//         await Page.ReloadAsync();

//         // Assert - Dark mode should persist
//         var persistedTheme = await htmlElement.GetAttributeAsync("class");
//         Assert.That(isDarkMode?.Contains("dark"), Is.EqualTo(persistedTheme?.Contains("dark")));
//     }

//     [Test]
//     [Category("Theme")]
//     public async Task DarkModeToggle_ShouldBeAccessible()
//     {
//         // Arrange & Act
//         await Page.GotoAsync(BaseUrl);

//         // Assert
//         var darkModeToggle = Page.Locator("button[aria-label*='mode']");
//         await Expect(darkModeToggle).ToBeVisibleAsync();
//         await Expect(darkModeToggle).ToHaveAttributeAsync("aria-label", new Regex(".*mode.*", RegexOptions.IgnoreCase));
//     }
// }