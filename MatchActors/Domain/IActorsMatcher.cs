namespace MatchActors.Domain;

internal interface IActorsMatcher
{
    public Movie[] MatchByMovies(IEnumerable<Movie> moviesForFirstActor, IEnumerable<Movie> moviesForSecondActor, bool onlyActing);
}