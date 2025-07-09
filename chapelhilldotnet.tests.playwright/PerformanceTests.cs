// using Microsoft.Playwright;
// using NUnit.Framework;

// namespace chapelhilldotnet.tests.playwright;

// [TestFixture]
// [Category("Performance")]
// public class PerformanceTests : BaseTest
// {
//     [Test]
//     public async Task HomePage_ShouldLoadWithinReasonableTime()
//     {
//         // Arrange
//         var stopwatch = System.Diagnostics.Stopwatch.StartNew();

//         // Act
//         await Page.GotoAsync(BaseUrl);
//         await Expect(Page.Locator("h1")).ToBeVisibleAsync();

//         stopwatch.Stop();

//         // Assert - Page should load within 5 seconds
//         Assert.That(stopwatch.ElapsedMilliseconds, Is.LessThan(5000),
//             $"Page took {stopwatch.ElapsedMilliseconds}ms to load, expected less than 5000ms");
//     }

//     [Test]
//     public async Task HomePage_ShouldHaveReasonablePageSize()
//     {
//         // Arrange & Act
//         var response = await Page.GotoAsync(BaseUrl);

//         // Assert
//         Assert.That(response, Is.Not.Null);
//         Assert.That(response!.Status, Is.EqualTo(200));

//         // Check that the page doesn't have excessive resources
//         var allResponses = new List<IResponse>();
//         Page.Response += (_, response) => allResponses.Add(response);

//         await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

//         var totalSize = allResponses.Sum(r => r.Body().Result.Length);
//         Assert.That(totalSize, Is.LessThan(5 * 1024 * 1024), // 5MB
//             $"Total page size is {totalSize / 1024}KB, which exceeds the 5MB limit");
//     }
// }