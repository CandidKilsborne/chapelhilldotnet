using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using chapelhilldotnet.web;
using chapelhilldotnet.web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Register custom services
builder.Services.AddScoped<IAuthenticationService, SimpleAuthenticationService>();
builder.Services.AddScoped<IEventService, EventService>();

await builder.Build().RunAsync();