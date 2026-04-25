using Microsoft.Playwright.NUnit;

namespace chapelhilldotnet.E2ETests;

public abstract class BlazorPageTest : PageTest
{
    protected const string BaseUrl = "http://localhost:5000";

    [SetUp]
    public void ConfigureTimeouts()
    {
        // Blazor WASM requires time to download the .NET runtime on first load.
        // 90 seconds covers cold-start scenarios; warm loads are typically under 3s.
        Page.SetDefaultTimeout(90_000);
        Page.SetDefaultNavigationTimeout(90_000);
    }
}
