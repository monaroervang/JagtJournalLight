using JagtJournal.Facade.HuntingAreas;
using JagtJournal.Application.Dtos.HuntingAreas;
using JagtJournal.Application.Dtos.HuntEntries;
using JagtJournal.Facade.HuntEntries;
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
// Registrerer Swagger, som genererer den interaktive dokumentation.
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    // OpenAPI-dokumentet i JSON-format.
    app.MapOpenApi();

    // Gør Swagger JSON tilgængelig.
    app.UseSwagger();

    // Viser den grafiske Swagger UI.
    app.UseSwaggerUI();
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

// Henter de seneste jagtposter for et brugernavn.
// Facaden bliver automatisk leveret gennem Dependency Injection.
app.MapGet(
    "/api/huntentries/latest/{username}",
    async (
        string username,
        IHuntEntryFacade huntEntryFacade,
        CancellationToken cancellationToken) =>
    {
        var huntEntries =
            await huntEntryFacade.GetLatestByUsernameAsync(
                username,
                cancellationToken: cancellationToken);

        return Results.Ok(huntEntries);
    })
    .WithName("GetLatestHuntEntriesByUsername");

// Opretter en ny jagtpost ud fra data i HTTP-requestens JSON-body.
// Facaden leveres automatisk gennem Dependency Injection.
app.MapPost(
    "/api/huntentries",
    async (
        CreateHuntEntryDto dto,
        IHuntEntryFacade huntEntryFacade,
        CancellationToken cancellationToken) =>
    {
        var huntEntryId = await huntEntryFacade.CreateAsync(
            dto,
            cancellationToken);

        // 201 Created fortæller klienten, at en ny ressource er oprettet.
        // Location-headeren peger på den nye jagtposts kommende GET-adresse.
        return Results.Created(
            $"/api/huntentries/{huntEntryId}",
            new { Id = huntEntryId });
    })
    .WithName("CreateHuntEntry");

// Henter alle jagtområder.
// Facaden leveres automatisk gennem Dependency Injection.
app.MapGet(
    "/api/huntingareas",
    async (
        IHuntingAreaFacade huntingAreaFacade,
        CancellationToken cancellationToken) =>
    {
        var huntingAreas =
            await huntingAreaFacade.GetAllAsync(cancellationToken);

        return Results.Ok(huntingAreas);
    })
    .WithName("GetHuntingAreas");

// Opretter et nyt jagtområde.
// DTO'en bindes automatisk fra requestens JSON-body.
app.MapPost(
    "/api/huntingareas",
    async (
        CreateHuntingAreaDto dto,
        IHuntingAreaFacade huntingAreaFacade,
        CancellationToken cancellationToken) =>
    {
        var huntingAreaId = await huntingAreaFacade.CreateAsync(
            dto,
            cancellationToken);

        return Results.Created(
            $"/api/huntingareas/{huntingAreaId}",
            new { Id = huntingAreaId });
    })
    .WithName("CreateHuntingArea");

app.Run();
