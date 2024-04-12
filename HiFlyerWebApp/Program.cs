using Blazored.LocalStorage;
using HiFlyerWebApp.Authentication;
using HiFlyerClassLibrary.Endpoints;
using HiFlyerWebApp;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using HiFlyerWebApp.Caches;
using HiFlyerWebApp.Stores;
using HiFlyerClassLibrary.Models.Profiles;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddOptions();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

builder.Services.AddAutoMapper(typeof(AddressProfile));
builder.Services.AddAutoMapper(typeof(CustomerProfile));

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IShopifyEndpoint, ShopifyEndpoint>();
builder.Services.AddScoped<ICartCache, CartCache>();
builder.Services.AddScoped<ITokenCache, TokenCache>();
builder.Services.AddScoped<ErrorListStore>();

await builder.Build().RunAsync();
