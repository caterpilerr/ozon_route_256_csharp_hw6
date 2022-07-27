namespace MatchActors.Application;

public interface IActorsMatchService
{
    public Task<IEnumerable<string>> FindMovies(MatchActorsCommand command, CancellationToken cancellationToken);
}