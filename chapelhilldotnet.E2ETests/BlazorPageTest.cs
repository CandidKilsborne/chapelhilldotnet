using Microsoft.Playwright.NUnit;
using System.Diagnostics;
using System.Net;

namespace chapelhilldotnet.E2ETests;

[SetUpFixture]
public sealed class E2EWebHostFixture
{
    private static readonly Uri BaseUri = new(BlazorPageTest.BaseUrl);
    private Process? _webHostProcess;

    [OneTimeSetUp]
    public async Task StartWebHostAsync()
    {
        if (await IsWebHostReachableAsync())
        {
            return;
        }

        ProcessStartInfo startInfo = new("dotnet", "run --project ..\\chapelhilldotnet.Web\\chapelhilldotnet.Web.csproj --configuration Debug")
        {
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        _webHostProcess = Process.Start(startInfo)
            ?? throw new InvalidOperationException("Failed to start web host process for E2E tests.");

        await WaitForWebHostAsync();
    }

    [OneTimeTearDown]
    public void StopWebHost()
    {
        if (_webHostProcess is null || _webHostProcess.HasExited)
        {
            return;
        }

        try
        {
            _webHostProcess.Kill(entireProcessTree: true);
            _webHostProcess.WaitForExit(5_000);
        }
        catch
        {
            // Best-effort cleanup.
        }
        finally
        {
            _webHostProcess.Dispose();
            _webHostProcess = null;
        }
    }

    private static async Task WaitForWebHostAsync()
    {
        TimeSpan timeout = TimeSpan.FromSeconds(60);
        Stopwatch stopwatch = Stopwatch.StartNew();

        while (stopwatch.Elapsed < timeout)
        {
            if (await IsWebHostReachableAsync())
            {
                return;
            }

            await Task.Delay(500);
        }

        throw new TimeoutException($"Web host did not become reachable at '{BaseUri}' within {timeout.TotalSeconds:0} seconds.");
    }

    private static async Task<bool> IsWebHostReachableAsync()
    {
        try
        {
            using HttpClient client = new()
            {
                Timeout = TimeSpan.FromSeconds(2)
            };

            using HttpResponseMessage response = await client.GetAsync(BaseUri);
            return response.StatusCode != HttpStatusCode.NotFound;
        }
        catch
        {
            return false;
        }
    }
}

public abstract class BlazorPageTest : PageTest
{
    protected internal const string BaseUrl = "https://localhost:7105";

    [SetUp]
    public void ConfigureTimeouts()
    {
        // Blazor WASM requires time to download the .NET runtime on the first load.
        // 90 seconds covers cold-start scenarios; warm loads are typically under 3s.
        Page.SetDefaultTimeout(90_000);
        Page.SetDefaultNavigationTimeout(90_000);
    }
}
