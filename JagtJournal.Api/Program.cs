using JagtJournal.Application.DependencyInjection;
using JagtJournal.Facade.DependencyInjection;
using JagtJournal.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

var connectionString =
    builder.Configuration.GetConnectionString("JagtJournalDb");

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException(
        "Connection string 'JagtJournalDb' mangler.");
}

// Registrerer services fra de enkelte lag.
builder.Services.AddApplication();
builder.Services.AddInfrastructure(connectionString);
builder.Services.AddFacade();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Et simpelt GET-endpoint, der kan bruges til at kontrollere,
// om JagtJournal API'et kører og svarer.
app.MapGet("/api/status", () =>
{
    return Results.Ok(new
    {
        Message = "JagtJournal API kører"
    });
})
.WithName("GetApiStatus");

app.Run();
