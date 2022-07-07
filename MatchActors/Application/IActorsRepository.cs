using MatchActors.Domain;

namespace MatchActors.Application;

internal interface IActorsRepository
{
    public Task<Actor?> Find(string name);

    public Task Add(Actor actor);
}