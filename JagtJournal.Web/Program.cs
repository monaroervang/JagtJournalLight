using JagtJournal.Application.Interfaces;
using JagtJournal.Application.Services;
using JagtJournal.Facade.HuntEntries;
using JagtJournal.Infrastructure.Data;
using JagtJournal.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using JagtJournal.Web.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var connectionString = builder.Configuration.GetConnectionString("JagtJournalDb");

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("Connection string 'JagtJournalDb' mangler.");
}

// Registrerer EF Core DbContext med SQL Server
builder.Services.AddDbContext<JagtJournalDbContext>(options =>
    options.UseSqlServer(connectionString));

// Registrerer repository-implementationen fra Infrastructure
builder.Services.AddScoped<IHuntEntryRepository, EfHuntEntryRepository>();

// Registrerer Application Service
builder.Services.AddScoped<IHuntEntryService, HuntEntryService>();

// Registrerer Facade-laget, som Blazor senere skal bruge
builder.Services.AddScoped<IHuntEntryFacade, HuntEntryFacade>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
