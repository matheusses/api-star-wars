using AutoMapper;
using Matheusses.StarWars.Domain.DTO;
using Matheusses.StarWars.Domain.Interfaces.Repository;
using Matheusses.StarWars.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Matheusses.StarWars.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly IHttpClientFactory _clientFactory;
     private readonly IConfiguration _configuration;
     private readonly IPlanetRepository _planetRepository;
     private readonly IFilmRepository _filmRepository;
    private readonly IMapper _mapper;

    public WeatherForecastController(
        IHttpClientFactory clientFactory,
        IConfiguration configuration,
        IPlanetRepository planetRepository,
        IFilmRepository filmRepository
        // IMapper mapper
    )
    {
        _clientFactory = clientFactory;
        _configuration = configuration;
        _planetRepository = planetRepository;
        _filmRepository = filmRepository;
        // _mapper = mapper;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        try{
                    var httpClient = _clientFactory.CreateClient("planet-api");
        var urlPlanet = _configuration.GetValue<string>("ExternalStarWarsApi:PlanetRoute");
        var teste = await httpClient.GetFromJsonAsync<PlanetDto>($"{urlPlanet}/1");
        //  var planet = _mapper.Map<Planet>(teste);
        Planet planet = new Planet{
            Climate = teste.Climate,
            Name = teste.Name,
            Id = 1,
            Terrain = teste.Terrain
        };
        await _planetRepository.AddAsync(planet);
        if(teste != null)
        {
            string urlFilm  = teste.Films.FirstOrDefault();
            var teste1 =  await httpClient.GetFromJsonAsync<FilmDto>(urlFilm);
            //  var film = _mapper.Map<Film>(teste1);
            // await _filmRepository.AddAsync(film);
            Log.Information("teste");
        }

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();

        }
        catch(Exception ex)
        {
            string teste = ex.Message;
            return null;
        }

    }
}
