using JagtJournal.Application.DependencyInjection;
using JagtJournal.Facade.DependencyInjection;
using JagtJournal.Infrastructure.DependencyInjection;
using JagtJournal.Web.Components;
using JagtJournal.Web.Clients;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var connectionString = builder.Configuration.GetConnectionString("JagtJournalDb");

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("Connection string 'JagtJournalDb' mangler.");
}

// Hvert lag registrerer selv sine egne services.
builder.Services.AddApplication();
builder.Services.AddInfrastructure(connectionString);
builder.Services.AddFacade();

// Registrerer en typed HttpClient til kommunikation med JagtJournal.Api.
// BaseAddress er API'ets grundadresse, som de relative endpoint-adresser
// i HuntingAreaApiClient bliver sat sammen med.
builder.Services.AddHttpClient<HuntingAreaApiClient>(httpClient =>
{
    httpClient.BaseAddress = new Uri("https://localhost:7117");
});

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
