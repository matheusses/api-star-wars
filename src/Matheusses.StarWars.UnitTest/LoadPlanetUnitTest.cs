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
using System.Net;

namespace Matheusses.StarWars.UnitTest;

[Trait("Planet", "PlanetApplication")]
public partial class PlanetApplicationUnitTest
{

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
        Assert.Equal(result.HttpStatusCode, HttpStatusCode.OK);
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
        Assert.Equal(result.HttpStatusCode, HttpStatusCode.OK);
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
        Assert.Equal(result.HttpStatusCode, HttpStatusCode.BadRequest);
        Assert.Equal("Invalid input", result.Message);
    }

    [Fact(DisplayName = "Test load planet not found from external planet api")]
    [Trait("Planet", "LoadPlanetFromExternalApi not found from external planet api")]
    public async void LoadPlanetNotFoundFromExternalPlanetApiTest()
    {
        // arrange
        int notFoundInput = 1;
        _planetRepository.GetByIdAsync(notFoundInput).ReturnsNull();
        _externalApiPlanet.GetAsync(notFoundInput.ToString()).ReturnsNull();
        // action
        var result = await _planetApllication.LoadPlanetByExternalApi(notFoundInput.ToString());
        // assert
        Assert.False(result.HasSuccess);
        Assert.Null(result.Data);
        Assert.Equal(result.HttpStatusCode, HttpStatusCode.NotFound);
        Assert.Equal("Planet not found or External Api unavalaible", result.Message);
    }

    [Fact(DisplayName = "Test not found from external film api to load planet ")]
    [Trait("Planet", "LoadPlanetFromExternalApi not found from external film api")]
    public async void LoadPlanetNotFoundFromExternalFilmApiTest()
    {
        // arrange
        var planetDtoFake = PlanetFaker.GenerateDto();        
        var urlFilm = planetDtoFake.Films.First();

        _planetRepository.GetByIdAsync(planetDtoFake.Id).ReturnsNull();
        _externalApiPlanet.GetAsync(planetDtoFake.Id.ToString()).Returns(Task.FromResult(planetDtoFake));
        _externalApiFilm.GetByUrlAsync(urlFilm).ReturnsNull();

        // action
        var result = await _planetApllication.LoadPlanetByExternalApi(planetDtoFake.Id.ToString());
        // assert
        Assert.False(result.HasSuccess);
        Assert.Null(result.Data);
        Assert.Equal(result.HttpStatusCode, HttpStatusCode.NotFound);
        Assert.Equal("Planet not found or External Api unavalaible", result.Message);
    }
}