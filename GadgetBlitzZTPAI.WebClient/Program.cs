using GadgetBlitzZTPAI.WebClient;
using GadgetBlitzZTPAI.WebClient.Services.Navigation;
using GadgetBlitzZTPAI.WebClient.Services.Smartphones;
using GadgetBlitzZTPAI.WebClient.ViewModels;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<INavigationService, NavigationService>();

builder.Services.AddScoped<ISmartphonesService, SmartphonesService>();
builder.Services.AddScoped<ISmartphonesViewModel, SmartphonesViewModel>();





//builder.Services.AddHttpClient<ISmartphonesService, SmartphonesService>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();

await builder.Build().RunAsync();
