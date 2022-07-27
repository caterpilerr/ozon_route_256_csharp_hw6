using MatchActors.Application;
using MatchActors.Domain;
using Newtonsoft.Json;

namespace MatchActors.Infrastructure.ImdbApiClient;

internal sealed class ImdbApiClient : IImdbApiClient
{
    private readonly HttpClient _httpClient;
    private readonly ImdbApiOptions _imdbApiOptions;

    public ImdbApiClient(HttpClient httpClient, ImdbApiOptions imdbApiOptions)
    {
        _httpClient = httpClient;
        _imdbApiOptions = imdbApiOptions;
    }

    public async Task<Actor?> FindActor(string name, CancellationToken cancellationToken)
    {
        var responseMessage = await _httpClient.GetAsync(BuildFindActorQuery(name), cancellationToken);
        var content = await responseMessage.Content.ReadAsStringAsync(cancellationToken);
        var data = JsonConvert.DeserializeObject<FindActorResponse>(content);
        
        return data?.Results.FirstOrDefault(x => name == x.Title);
    }

    public async Task<Movie[]> FindMoviesForActor(string actorId, CancellationToken cancellationToken)
    {
        var responseMessage = await _httpClient.GetAsync(BuildFindMoviesQuery(actorId), cancellationToken);
        var content = await responseMessage.Content.ReadAsStringAsync(cancellationToken);
        
        return JsonConvert.DeserializeObject<FindMoviesResponse>(content)?.CastMovies ?? Array.Empty<Movie>();
    }

    private string BuildFindActorQuery(string actorName)
    {
        return "https://imdb-api.com/en/API/SearchName/" + _imdbApiOptions.Key + "/" + actorName;
    }

    private string BuildFindMoviesQuery(string actorId)
    {
        return "https://imdb-api.com/en/API/Name/" + _imdbApiOptions.Key + "/" + actorId;
    }
}