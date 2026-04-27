using Microsoft.Playwright.NUnit;

namespace chapelhilldotnet.E2ETests;

public abstract class BlazorPageTest : PageTest
{
    protected const string BaseUrl = "https://localhost:7105";

    [SetUp]
    public void ConfigureTimeouts()
    {
        // Blazor WASM requires time to download the .NET runtime on the first load.
        // 90 seconds covers cold-start scenarios; warm loads are typically under 3s.
        Page.SetDefaultTimeout(90_000);
        Page.SetDefaultNavigationTimeout(90_000);
    }
}
