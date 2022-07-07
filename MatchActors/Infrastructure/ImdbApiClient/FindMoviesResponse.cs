using MatchActors.Domain;

namespace MatchActors.Infrastructure.ImdbApiClient;

internal sealed class FindMoviesResponse
{
    public Movie[] CastMovies { get; init; } = Array.Empty<Movie>();
}