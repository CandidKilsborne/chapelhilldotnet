// using Microsoft.Playwright;
// using NUnit.Framework;

// namespace chapelhilldotnet.tests.playwright;

// [TestFixture]
// public class ResponsiveTests : BaseTest
// {
//     [Test]
//     [Category("Responsive")]
//     public async Task HomePage_ShouldBeResponsiveOnMobile()
//     {
//         // Arrange
//         await Page.SetViewportSizeAsync(new() { Width = 375, Height = 667 }); // iPhone SE size
//         await Page.GotoAsync(BaseUrl);

//         // Assert
//         await Expect(Page.Locator("header")).ToBeVisibleAsync();
//         await Expect(Page.Locator("main")).ToBeVisibleAsync();

//         // Navigation should be hidden on mobile (if implemented)
//         var navigation = Page.Locator("nav.hidden.md\\:flex");
//         if (await navigation.CountAsync() > 0)
//         {
//             await Expect(navigation).Not.ToBeVisibleAsync();
//         }
//     }

//     [Test]
//     [Category("Responsive")]
//     public async Task HomePage_ShouldBeResponsiveOnTablet()
//     {
//         // Arrange
//         await Page.SetViewportSizeAsync(new() { Width = 768, Height = 1024 }); // iPad size
//         await Page.GotoAsync(BaseUrl);

//         // Assert
//         await Expect(Page.Locator("header")).ToBeVisibleAsync();
//         await Expect(Page.Locator("main")).ToBeVisibleAsync();
//         await Expect(Page.Locator("nav")).ToBeVisibleAsync();
//     }

//     [Test]
//     [Category("Responsive")]
//     public async Task HomePage_ShouldBeResponsiveOnDesktop()
//     {
//         // Arrange
//         await Page.SetViewportSizeAsync(new() { Width = 1920, Height = 1080 });
//         await Page.GotoAsync(BaseUrl);

//         // Assert
//         await Expect(Page.Locator("header")).ToBeVisibleAsync();
//         await Expect(Page.Locator("main")).ToBeVisibleAsync();
//         await Expect(Page.Locator("nav")).ToBeVisibleAsync();
//     }

//     [Test]
//     [TestCase(375, 667, TestName = "Mobile - iPhone SE")]
//     [TestCase(390, 844, TestName = "Mobile - iPhone 12")]
//     [TestCase(768, 1024, TestName = "Tablet - iPad")]
//     [TestCase(1024, 768, TestName = "Tablet - iPad Landscape")]
//     [TestCase(1920, 1080, TestName = "Desktop - Full HD")]
//     [Category("Responsive")]
//     public async Task HomePage_ShouldBeResponsiveAtDifferentViewports(int width, int height)
//     {
//         // Arrange
//         await Page.SetViewportSizeAsync(new() { Width = width, Height = height });
//         await Page.GotoAsync(BaseUrl);

//         // Assert - Basic layout should be functional at all sizes
//         await Expect(Page.Locator("header")).ToBeVisibleAsync();
//         await Expect(Page.Locator("main")).ToBeVisibleAsync();

//         // Check that content doesn't overflow
//         var body = Page.Locator("body");
//         var boundingBox = await body.BoundingBoxAsync();
//         Assert.That(boundingBox?.Width, Is.LessThanOrEqualTo(width + 20), "Content should not overflow viewport width");
//     }
// }