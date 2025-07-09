// using Microsoft.Playwright;
// using NUnit.Framework;

// namespace chapelhilldotnet.tests.playwright;

// [TestFixture]
// public class EventsPageTests : BaseTest
// {
//     [Test]
//     public async Task EventsPage_ShouldLoadSuccessfully()
//     {
//         // Arrange & Act
//         await Page.GotoAsync($"{BaseUrl}/events");

//         // Assert
//         await Expect(Page.Locator("h1")).ToContainTextAsync("CH .NET & Azure Events");
//         await Expect(Page.Locator("#upcoming h2")).ToContainTextAsync("Upcoming Events");
//         await Expect(Page.Locator("#past h2")).ToContainTextAsync("Past Events");
//     }

//     [Test]
//     public async Task EventsPage_ShouldDisplayUpcomingEvents()
//     {
//         // Arrange & Act
//         await Page.GotoAsync($"{BaseUrl}/events");

//         // Assert
//         var upcomingSection = Page.Locator("#upcoming");
//         await Expect(upcomingSection).ToBeVisibleAsync();

//         // Check for event cards
//         var eventCards = upcomingSection.Locator(".bg-white.dark\\:bg-gray-800");
//         await Expect(eventCards.First()).ToBeVisibleAsync();
//     }

//     [Test]
//     public async Task EventsPage_ShouldDisplayPastEventsTable()
//     {
//         // Arrange & Act
//         await Page.GotoAsync($"{BaseUrl}/events");

//         // Assert
//         var pastSection = Page.Locator("#past");
//         await Expect(pastSection).ToBeVisibleAsync();

//         var table = pastSection.Locator("table");
//         await Expect(table).ToBeVisibleAsync();

//         // Check table headers
//         await Expect(table.Locator("th")).ToContainTextAsync(["Event", "Date", "Location", "Resources"]);
//     }

//     [Test]
//     [Category("Smoke")]
//     public async Task EventsPage_SmokeTest()
//     {
//         // Arrange & Act
//         await Page.GotoAsync($"{BaseUrl}/events");

//         // Assert - Basic page load verification
//         await Expect(Page).ToHaveURLAsync(new Regex(".*/events"));
//         await Expect(Page.Locator("h1")).ToBeVisibleAsync();
//     }
// }