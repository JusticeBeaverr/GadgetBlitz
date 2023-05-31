using Blazored.LocalStorage;
using GadgetBlitzZTPAI.WebClient;
using GadgetBlitzZTPAI.WebClient.Services.Auth;
using GadgetBlitzZTPAI.WebClient.Services.Navigation;
using GadgetBlitzZTPAI.WebClient.Services.Smartphones;
using GadgetBlitzZTPAI.WebClient.ViewModels;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<INavigationService, NavigationService>();

builder.Services.AddScoped<ISmartphonesService, SmartphonesService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ISmartphonesViewModel, SmartphonesViewModel>();
builder.Services.AddScoped<ISmartphoneDetailsViewModel, SmartphoneDetailsViewModel>();
builder.Services.AddScoped<IAccountViewModel, AccountViewModel>();
builder.Services.AddScoped<CustomAuthenticationStateProvider>();


builder.Services.AddBlazoredLocalStorage();

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

builder.Services.AddHttpClient<ISmartphonesService, SmartphonesService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:8080/gadgetblitz/api/v1/");
});
builder.Services.AddHttpClient<IAuthService, AuthService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:8080/gadgetblitz/api/v1/");
});

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();

await builder.Build().RunAsync();
