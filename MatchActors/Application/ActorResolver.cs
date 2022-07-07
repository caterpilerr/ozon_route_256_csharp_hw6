using MatchActors.Application.Exceptions;
using MatchActors.Domain;

namespace MatchActors.Application;

internal sealed class ActorResolver : IActorResolver
{
    private readonly IActorsRepository _actorsRepository;
    private readonly IImdbApiClient _imdbApiClient;

    public ActorResolver(IActorsRepository actorsRepository, IImdbApiClient imdbApiClient)
    {
        _actorsRepository = actorsRepository;
        _imdbApiClient = imdbApiClient;
    }

    public async Task<Actor> ResolveActor(string name, CancellationToken cancellationToken)
    {
        var actorFromDb = await _actorsRepository.Find(name);
        if (actorFromDb is not null)
        {
            return actorFromDb;
        }

        var actorFromApi = await _imdbApiClient.FindActor(name, cancellationToken);

        if (actorFromApi is null)
        {
            throw new ActorNotFoundException($"Actor with name {name} was not found");
        }

        await _actorsRepository.Add(actorFromApi);

        return actorFromApi;
    }
}