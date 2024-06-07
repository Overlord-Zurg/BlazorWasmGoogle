using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorGoogle;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddOidcAuthentication(options =>
{
    // Configure your authentication provider options here.
    // For more information, see https://aka.ms/blazor-standalone-auth
    builder.Configuration.Bind("Local", options.ProviderOptions);

    options.ProviderOptions.Authority = "https://accounts.google.com/";
    options.ProviderOptions.RedirectUri = "https://localhost:7167/authentication/login-callback";
    options.ProviderOptions.PostLogoutRedirectUri = "https://localhost:7167/authentication/logout-callback";
    options.ProviderOptions.ResponseType = "id_token";

    options.ProviderOptions.ClientId = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_ID");
});

await builder.Build().RunAsync();
