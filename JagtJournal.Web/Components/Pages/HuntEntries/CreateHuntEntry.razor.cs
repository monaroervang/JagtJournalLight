using JagtJournal.Application.Dtos.HuntingAreas;
using JagtJournal.Application.Dtos.HuntEntries;
using JagtJournal.Facade.HuntEntries;
using JagtJournal.Web.Clients;
using Microsoft.AspNetCore.Components;

namespace JagtJournal.Web.Components.Pages.HuntEntries;

/// <summary>
/// Indeholder C#-logikken til siden, hvor en jagtpost oprettes.
///
/// Klassen er partial, fordi den bliver kombineret med den klasse,
/// som Blazor automatisk genererer ud fra CreateHuntEntry.razor.
/// </summary>
public partial class CreateHuntEntry
{
    // API-klienten bruges til at hente jagtområder.
    [Inject]
    private HuntingAreaApiClient HuntingAreaApiClient { get; set; } = default!;

    // Facaden bruges til at oprette selve jagtposten.
    [Inject]
    private IHuntEntryFacade HuntEntryFacade { get; set; } = default!;

    // DTO'en fungerer som formularens model.
    // Blazor bruger Data Annotations på DTO'en til validering.
    private readonly CreateHuntEntryDto _model = new()
    {
        Date = DateOnly.FromDateTime(DateTime.Today)
    };

    // Indeholder de jagtområder, som senere skal vises i en dropdown.
    private IReadOnlyList<HuntingAreaListItemDto> _huntingAreas = [];

    // Midlertidig løsning, indtil tekstfeltet erstattes af en dropdown.
    private string _huntingAreaIdText = Guid.NewGuid().ToString();

    // Viser fejl- eller succesbeskeder i UI'et.
    private string? _message;

    /// <summary>
    /// Kaldes automatisk, når komponenten bliver initialiseret.
    /// Her hentes jagtområderne fra API'et.
    /// </summary>
    protected override async Task OnInitializedAsync()
    {
        _huntingAreas = await HuntingAreaApiClient.GetAllAsync();
    }

    /// <summary>
    /// Kaldes, når formularen er gyldig og sendes.
    /// </summary>
    private async Task CreateAsync()
    {
        if (!Guid.TryParse(_huntingAreaIdText, out var huntingAreaId))
        {
            _message = "Vælg eller indtast et gyldigt jagtområde.";
            return;
        }

        _model.HuntingAreaId = huntingAreaId;

        var huntEntryId =
            await HuntEntryFacade.CreateAsync(_model);

        _message = $"Jagtpost oprettet med id: {huntEntryId}";
    }
}