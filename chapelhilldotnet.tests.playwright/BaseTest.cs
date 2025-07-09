using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace chapelhilldotnet.tests.playwright;

public class BaseTest : PageTest
{
    protected string BaseUrl = "https://localhost:7154"; // Adjust port as needed

    [SetUp]
    public async Task TestSetup()
    {
        // Set default timeout
        Page.SetDefaultTimeout(30000);

        // Navigate to the application
        await Page.GotoAsync(BaseUrl);
    }
}