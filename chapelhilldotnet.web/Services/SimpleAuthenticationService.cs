using Microsoft.JSInterop;

namespace chapelhilldotnet.web.Services;

public class SimpleAuthenticationService : IAuthenticationService
{
    private const string AuthTokenKey = "authToken";
    private const string UsernameKey = "currentUser";
    
    // In a real application, these would be stored securely in a backend service
    // For Azure Static Web Apps with WASM, consider using Azure AD B2C or Static Web Apps Authentication
    private const string AdminUsername = "admin";
    private const string AdminPassword = "Chapel2024!";
    
    private readonly IJSRuntime _jsRuntime;

    public SimpleAuthenticationService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<bool> LoginAsync(string username, string password)
    {
        // Simple validation - in production, use proper authentication
        if (username == AdminUsername && password == AdminPassword)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", AuthTokenKey, "authenticated");
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", UsernameKey, username);
            return true;
        }
        return false;
    }

    public async Task LogoutAsync()
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", AuthTokenKey);
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", UsernameKey);
    }

    public async Task<bool> IsAuthenticatedAsync()
    {
        try
        {
            var token = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", AuthTokenKey);
            return !string.IsNullOrEmpty(token);
        }
        catch
        {
            return false;
        }
    }

    public async Task<string?> GetCurrentUserAsync()
    {
        try
        {
            return await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", UsernameKey);
        }
        catch
        {
            return null;
        }
    }
}
