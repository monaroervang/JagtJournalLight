using System.Net.Http.Json;
using JagtJournal.Application.Dtos.HuntingAreas;

namespace JagtJournal.Web.Clients;

/// <summary>
/// Klient til kommunikation med HuntingArea API'et.
/// </summary>
public class HuntingAreaApiClient
{
    private readonly HttpClient _httpClient;

    public HuntingAreaApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Henter alle jagtområder.
    /// </summary>
    public async Task<IReadOnlyList<HuntingAreaListItemDto>> GetAllAsync()
    {
        var huntingAreas =
            await _httpClient.GetFromJsonAsync<List<HuntingAreaListItemDto>>(
                "api/huntingareas");

        return huntingAreas ?? [];
    }
}