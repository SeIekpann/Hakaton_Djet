using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Oddjet.Data;
using ElectronNET.API;
using ElectronNET.API.Entities;


var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseElectron(args);
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddElectron();


var app = builder.Build();
var browserWindow = await Electron.WindowManager.CreateWindowAsync(new BrowserWindowOptions
         {
                Width = 1152,
                Height = 864,
                Show = false
            });

browserWindow.OnReadyToShow += () => browserWindow.Show();
browserWindow.SetTitle("Electron.NET API Demos");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

//pp.Run();
await app.StartAsync();
await Electron.WindowManager.CreateWindowAsync();

app.WaitForShutdown();
