using FrontAppBlazor.Components;
using FrontAppBlazor.Services;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddHttpClient();
builder.Services.AddScoped<ProtectedLocalStorage>();
builder.Services.AddScoped<AuthentificationService>();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}


app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
