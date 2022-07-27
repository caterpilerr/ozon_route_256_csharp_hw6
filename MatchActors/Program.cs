using MatchActors.Application;
using MatchActors.Domain;
using MatchActors.Infrastructure.Database;
using MatchActors.Infrastructure.ImdbApiClient;

namespace MatchActors
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            AddOptions(builder.Services, builder.Configuration);

            // Add services to the container.
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHttpClient();
            builder.Services.AddSingleton<IImdbApiClient, ImdbApiClient>();
            builder.Services.AddSingleton<IActorsRepository, ActorsRepository>();
            builder.Services.AddSingleton<IActorResolver, ActorResolver>();
            builder.Services.AddSingleton<IActorsMatchService, ActorsMatchService>();
            builder.Services.AddSingleton<IActorsMatcher, ActorsMatcher>();
            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseSwagger();
            
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
            
            app.UseSwaggerUI();

            app.Run();
        }

        private static void AddOptions(IServiceCollection services, IConfiguration configuration)
        {
            var imdbApiOptions = configuration.GetSection(nameof(ImdbApiOptions)).Get<ImdbApiOptions>();
            var matchActorsDbConnectionString = configuration.GetSection(nameof(MatchActorsDbConnectionString))
                .Get<MatchActorsDbConnectionString>();

            services.AddSingleton(imdbApiOptions);
            services.AddSingleton(matchActorsDbConnectionString);
        }
    }
}