using MatchActors.Domain;

namespace MatchActors.Infrastructure.ImdbApiClient;

internal sealed class FindActorResponse
{
    public Actor[] Results { get; init; } = Array.Empty<Actor>();
}