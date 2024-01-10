using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using WeatherInterface.Data;
using Marten;
using Weasel.Core;
using Refit;
using WeatherInterface.Api;
using MudBlazor.Services;
using Microsoft.AspNetCore;
using System.Net;
using WeatherApp;

WebHost.CreateDefaultBuilder(args)
    .UseKestrel(options =>
    {
        options.Listen(IPAddress.Any, 5001); // Lauscht auf allen verfügbaren Schnittstellen
    })
    .UseStartup<Startup>();

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json");
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var gitHubApi = RestService.For<IWeatherApi>("https://api.weatherapi.com/");
builder.Services.AddSingleton(gitHubApi);

builder.Services.AddMudServices();

// Marten-Konfiguration
// This is the absolute, simplest way to integrate Marten into your
// .NET application with Marten's default configuration
builder.Services.AddMarten(options =>
{
    // Establish the connection string to your Marten database
    options.Connection(builder.Configuration.GetConnectionString("Marten")!);

    // If we're running in development mode, let Marten just take care
    // of all necessary schema building and patching behind the scenes
    if (builder.Environment.IsDevelopment())
    {
        options.AutoCreateSchemaObjects = AutoCreate.All;
    }
});

builder.Services.AddSingleton<WeatherService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
