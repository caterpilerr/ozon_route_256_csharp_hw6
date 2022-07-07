using Dapper;
using MatchActors.Application;
using MatchActors.Domain;
using Npgsql;

namespace MatchActors.Infrastructure.Database;

internal sealed class ActorsRepository : IActorsRepository
{
    private readonly ILogger<ActorsRepository> _logger;
    private readonly MatchActorsDbConnectionString _connectionString;

    public ActorsRepository(MatchActorsDbConnectionString connectionString, ILogger<ActorsRepository> logger)
    {
        _connectionString = connectionString;
        _logger = logger;
    }

    public async Task<Actor?> Find(string name)
    {
        const string query = @"SELECT actor_id as id, name as title FROM actors WHERE name = @Name;";
        await using var connection = new NpgsqlConnection(_connectionString.Value);
        Actor? result = null;
        try
        {
            connection.Open();
            var parameters = new DynamicParameters(new { Name = name });
            result = await connection.QuerySingleOrDefaultAsync<Actor>(query, parameters);
        }
        catch (NpgsqlException e)
        {
            _logger.LogError(e, $"Error while trying to get actor {name} from the database");
        }

        return result;
    }

    public async Task Add(Actor actor)
    {
        const string query = @"INSERT INTO actors VALUES (@Id, @ActorId, @Name)";
        await using var connection = new NpgsqlConnection(_connectionString.Value);
        try
        {
            connection.Open();
            var parameters = new DynamicParameters(new { Id = Guid.NewGuid(), ActorId = actor.Id, Name = actor.Title });
            await connection.ExecuteAsync(query, parameters);
        }
        catch (NpgsqlException e)
        {
            _logger.LogError(e, $"Error while adding actor {actor.Title} to the database");
        }
    }
}