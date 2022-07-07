namespace MatchActors.Contracts;

public sealed class MatchActorsRequest
{
    public string FirstActor { get; init; } = string.Empty;
    public string SecondActor { get; init; } = string.Empty;
    public bool MoviesOnly { get; init; }
}