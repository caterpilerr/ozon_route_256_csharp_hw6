namespace MatchActors.Application;

public sealed class MatchActorsCommand
{
    public string FirstActor { get; init; } = string.Empty;
    public string SecondActor { get; init; } = string.Empty;
    public bool ActingOnly { get; init; }
}