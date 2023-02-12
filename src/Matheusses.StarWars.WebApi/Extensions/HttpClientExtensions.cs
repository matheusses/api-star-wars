using Matheusses.StarWars.Domain.DTO;
using Matheusses.StarWars.Domain.Interfaces.ExternalApi;
using Matheusses.StarWars.Domain.Model;
using Polly;
using Refit;
using Serilog;


namespace Matheusses.StarWars.WebApi.Extensions
{
    public static class HttpClientExtensions
    {
        public static void AddExternalApi(this IServiceCollection services, IConfiguration configuration)
        {
            var retryPolicy =  Policy
                .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                .RetryAsync(3, onRetry: (message, retryCount) =>
                {
                    string msg = $"Retentativa: {retryCount}";
                    Console.Out.WriteLineAsync(msg);
                    Log.Information(msg);
                });

                var externalUrlPlanet = configuration.GetValue<string>("ExternalStarWarsApi:PlanetRoute");
                services.AddHttpClient("HttpClient").AddPolicyHandler(retryPolicy);
                services.AddTransient<IExternalApiRest<PlanetDto>,ExternalApiRest<PlanetDto>>(
                    provider => new ExternalApiRest<PlanetDto>(provider.GetService<IHttpClientFactory>(), externalUrlPlanet)
                );
                
                var externalUrlFilm = configuration.GetValue<string>("ExternalStarWarsApi:FilmRoute");
                services.AddTransient<IExternalApiRest<FilmDto>,ExternalApiRest<FilmDto>>(
                    provider => new ExternalApiRest<FilmDto>(provider.GetService<IHttpClientFactory>(), externalUrlFilm)
                );

        }
    }
}