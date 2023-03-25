using Blazored.Modal;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetGame.Database;
using PetGame.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazoredModal();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<CurrentPlayer>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<CurrentPlayer>());

builder.Services.Configure<RazorPagesOptions>(options =>
{
    options.RootDirectory = "/Layout";
});

builder.Services.AddDbContextFactory<PetGameDatabase>(options =>
{
    var connectionString = "server=localhost;port=3306;database=petgame;user=root;password=YOURPASSWORDHERE;";
    
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

var app = builder.Build();

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
app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

using(var scope = app.Services.CreateScope())
{
    var database = scope.ServiceProvider.GetRequiredService<PetGameDatabase>();
    await database.Database.MigrateAsync();
}

app.Run();
