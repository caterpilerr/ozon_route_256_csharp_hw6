using MatchActors.Domain;

namespace MatchActors.Application;

internal interface IImdbApiClient
{
    public Task<Actor?> FindActor(string name, CancellationToken cancellationToken);
    public Task<Movie[]> FindMoviesForActor(string actorId, CancellationToken cancellationToken);
}