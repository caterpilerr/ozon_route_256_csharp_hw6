using MatchActors.Domain;

namespace MatchActors.Application;

internal sealed class ActorsMatchService : IActorsMatchService
{
    private readonly IActorResolver _actorResolver;
    private readonly IImdbApiClient _imdbApiClient;
    private readonly IActorsMatcher _actorsMatcher;

    public ActorsMatchService(
        IImdbApiClient imdbApiClient,
        IActorResolver actorResolver,
        IActorsMatcher actorsMatcher)
    {
        _imdbApiClient = imdbApiClient;
        _actorResolver = actorResolver;
        _actorsMatcher = actorsMatcher;
    }

    public async Task<IEnumerable<string>> FindMovies(MatchActorsCommand command, CancellationToken cancellationToken)
    {
        var firstActor = await _actorResolver.ResolveActor(command.FirstActor, cancellationToken);
        var secondActor = await _actorResolver.ResolveActor(command.SecondActor, cancellationToken);
        
        var moviesForFirstActor = await _imdbApiClient.FindMoviesForActor(firstActor.Id, cancellationToken);
        var moviesForSecondActor = await _imdbApiClient.FindMoviesForActor(secondActor.Id, cancellationToken);
        
        var matchingMovies = _actorsMatcher.MatchByMovies(moviesForFirstActor, moviesForSecondActor, command.ActingOnly);
        
        return matchingMovies.Select(x => x.Title);
    }
}