using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Matheusses.StarWars.Domain.Application;
using Matheusses.StarWars.Domain.DTO;
using Matheusses.StarWars.Domain.Interfaces.ExternalApi;
using Matheusses.StarWars.Domain.Interfaces.Repository;
using Matheusses.StarWars.UnitTest.Fakers;
using Serilog;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Matheusses.StarWars.Domain.Model;

namespace Matheusses.StarWars.UnitTest;

[Trait("Planet", "PlanetApplication")]
public class PlanetApplicationUnitTest
{
    private readonly PlanetApplication _planetApllication;

    private readonly IPlanetRepository _planetRepository =  Substitute.For<IPlanetRepository>();
    private readonly IFilmRepository _filmRepository =  Substitute.For<IFilmRepository>();
    private readonly IExternalApiRest<PlanetDto> _externalApiPlanet = Substitute.For<IExternalApiRest<PlanetDto>>();
    private readonly IExternalApiRest<FilmDto> _externalApiFilm = Substitute.For<IExternalApiRest<FilmDto>>();

    public PlanetApplicationUnitTest()
    {
        _planetApllication = new PlanetApplication(
            _planetRepository,
            _filmRepository,
            _externalApiPlanet,
            _externalApiFilm
        );
        
    }

    [Fact(DisplayName = "Test load planet from self database")]
    [Trait("Planet", "LoadPlanetFromSelfDatabaseTest Success")]
    public async void LoadPlanetFromSelfDatabaseTest()
    {
        // arrange
        var planetFake = PlanetFaker.Generate();
        _planetRepository.GetByIdAsync(planetFake.Id).Returns(Task.FromResult(planetFake));
        // action
        var result = await _planetApllication.LoadPlanetByExternalApi(planetFake.Id.ToString());
        // assert
        await _planetRepository.Received(1).GetByIdAsync(planetFake.Id);            
        Assert.True(result.HasSuccess);
    }

    [Fact(DisplayName = "Test load planet from external api")]
    [Trait("Planet", "LoadPlanetFromExterApi Success")]
    public async void LoadPlanetFromExternalApiTest()
    {
        // arrange
        var planetDtoFake = PlanetFaker.GenerateDto();
        
        var urlFilm = planetDtoFake.Films.First();
        var filmDtoFake = FilmFaker.GenerateDto();
        List<Film> filmsFake = new List<Film>{ filmDtoFake.ConverToFilm()};
        

        var planetFake = planetDtoFake.ConvertToPlanet(planetDtoFake.Id, filmsFake);

        _planetRepository.GetByIdAsync(planetDtoFake.Id).ReturnsNull();
        _externalApiPlanet.GetAsync(planetDtoFake.Id.ToString()).Returns(Task.FromResult(planetDtoFake));
        _externalApiFilm.GetByUrlAsync(urlFilm).Returns(Task.FromResult(filmDtoFake));
        _planetRepository.AddAsync(planetFake).Returns(Task.CompletedTask);

        // action
        var result = await _planetApllication.LoadPlanetByExternalApi(planetDtoFake.Id.ToString());
        // assert
        await _planetRepository.Received(1).GetByIdAsync(planetDtoFake.Id);
        await _externalApiPlanet.Received(1).GetAsync(planetDtoFake.Id.ToString());
        await _externalApiFilm.Received(1).GetByUrlAsync(urlFilm); 
        await _planetRepository.Received(1).AddAsync(Arg.Any<Planet>()); 
        Assert.True(result.HasSuccess);
        Assert.NotNull(result.Data);
    }

    [Fact(DisplayName = "Test load planet with invalid input")]
    [Trait("Planet", "LoadPlanetFromExterApi with invalid input")]
    public async void LoadPlanetInvalidInputTest()
    {
        // arrange
        string invalidInput = "asd324asd";
        // action
        var result = await _planetApllication.LoadPlanetByExternalApi(invalidInput);
        // assert
        Assert.False(result.HasSuccess);
        Assert.Null(result.Data);
    }
}
