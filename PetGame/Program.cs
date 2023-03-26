using Blazored.Modal;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetGame.Database;
using PetGame.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazoredModal();
builder.Services.AddAuthorizationCore();

// if you add a new class in Services that you want to be able to [Inject], or use in other services,
// register it here:
builder.Services.AddScoped<CurrentPlayer>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<CurrentPlayer>());
// builder.Services.AddScoped<MyCustomService>();

builder.Services.Configure<RazorPagesOptions>(options =>
{
    options.RootDirectory = "/Layout";
});

builder.Services.AddDbContextFactory<PetGameDatabase>(options =>
{
    // TODO: it's up to you to set up and configure a real database!
    // Sqlite is convenient to use when working on the game, on just your computer, but when it's time to deploy the
    // game to the internet, you'll need to use something more powerful, like PostgreSQL, MariaDB, or MySQL.
    options.UseSqlite("Data Source=PetGame.db;Cache=Shared");
});

var app = builder.Build();

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
    var dbFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<PetGameDatabase>>();
    await using var database = await dbFactory.CreateDbContextAsync();
    await database.Database.MigrateAsync();
}

app.Run();
