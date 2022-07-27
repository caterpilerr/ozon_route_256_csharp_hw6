using MatchActors.Domain;

namespace MatchActors.Application;

internal interface IActorResolver
{
   public Task<Actor> ResolveActor(string name, CancellationToken cancellationToken);
}