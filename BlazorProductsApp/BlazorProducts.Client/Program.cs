using BlazorProducts.Client;
using BlazorProducts.Client.AuthProviders;
using BlazorProducts.Client.Repository;
using BlazorProducts.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Register fake AuthService as Singleton
builder.Services.AddSingleton<AuthService>();

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("http://localhost:5071/api/")
});
builder.Services.AddScoped<IProductHttpRepository, ProductHttpRepository>();

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, TestAuthStateProvider>();
await builder.Build().RunAsync();
