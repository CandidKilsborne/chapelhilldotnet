# End-to-End Tests

This directory contains E2E tests using Playwright for .NET.

## Prerequisites

Before running the tests, you need to:

1. Install Playwright browsers (required on first run only):

```bash
cd chapelhilldotnet.E2ETests
dotnet build
pwsh bin/Debug/net9.0/playwright.ps1 install
```

## Running the Tests

### Option 1: With the application running separately

1. Start the application in one terminal:
```bash
cd chapelhilldotnet.web
dotnet run
```

2. Run the E2E tests in another terminal:
```bash
cd chapelhilldotnet.E2ETests
dotnet test
```

### Option 2: Using the test script (recommended)

From the root directory:
```bash
# Start app and run tests
./scripts/run-e2e-tests.sh
```

## Test Structure

- **NavigationTests.cs** - Tests for page navigation and routing
- **DarkModeTests.cs** - Tests for dark mode toggle functionality
- **EventsPageTests.cs** - Tests for the Events page interactions

## Configuration

The tests are configured to run against `http://localhost:5000` by default. You can change this in each test file if your application runs on a different port.

## Debugging Tests

To debug Playwright tests with headed browser:
```bash
HEADED=1 dotnet test
```

## Troubleshooting

If tests fail with browser errors:
1. Ensure Playwright browsers are installed: `pwsh bin/Debug/net9.0/playwright.ps1 install`
2. Check that the application is running on the correct port
3. Verify no firewall is blocking localhost connections
