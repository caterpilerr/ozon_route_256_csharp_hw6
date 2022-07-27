namespace MatchActors.Domain;

internal class ActorsMatcher : IActorsMatcher
{
    public Movie[] MatchByMovies(IEnumerable<Movie> moviesForFirstActor, IEnumerable<Movie> moviesForSecondActor,
        bool onlyActing)
    {
        if (onlyActing)
        {
            moviesForFirstActor = moviesForFirstActor.Where(m => m.Role is "Actress" or "Actor");
            moviesForSecondActor = moviesForSecondActor.Where(m => m.Role is "Actress" or "Actor");
        }

        var matchingTitles = moviesForFirstActor
            .Join(moviesForSecondActor, x => x.Id, y => y.Id, (x, _) => x)
            .ToArray();

        return matchingTitles;
    }
}