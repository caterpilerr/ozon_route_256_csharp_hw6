using MatchActors.Application;
using MatchActors.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace MatchActors.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActorsMatchController : ControllerBase
    {
        private readonly IActorsMatchService _actorsMatchService;

        public ActorsMatchController(IActorsMatchService actorsMatchService)
        {
            _actorsMatchService = actorsMatchService;
        }

        [HttpPost]
        public async Task<IEnumerable<string>> Post(MatchActorsRequest request, CancellationToken cancellationToken)
        {
            var command = new MatchActorsCommand
            {
                FirstActor = request.FirstActor,
                SecondActor = request.SecondActor,
                ActingOnly = request.MoviesOnly
            };
            
            return await _actorsMatchService.FindMovies(command, cancellationToken);
        }
    }
}
