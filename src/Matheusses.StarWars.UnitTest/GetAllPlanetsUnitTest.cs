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
    [Fact(DisplayName = "Test get all planets")]
    [Trait("Planet", "GetAllPlanets Success")]
    public async void GetAllPlanetsTest()
    {
        // arrange
          var planetFake = PlanetFaker.Generate();
          IEnumerable<Planet> planets = new List<Planet>(1){ planetFake };
        _planetRepository.GetAllAsync().Returns(Task<IEnumerable<Planet>>.FromResult(planets));
        // action
        var result = await _planetApllication.GetAllPlanets();
        // assert
        await _planetRepository.Received(1).GetAllAsync();            
        Assert.True(result.HasSuccess);
         Assert.NotNull(result.Data);
        Assert.Equal(result.HttpStatusCode, HttpStatusCode.OK);
    }

    [Fact(DisplayName = "Test get all planets return empty from database")]
    [Trait("Planet", "GetAllPlanets without planets")]
    public async void GetAllPlanetsEmptyTest()
    {
        // arrange
        _planetRepository.GetAllAsync().Returns(Task.FromResult((IEnumerable<Planet>)new List<Planet>()));
        // action
        var result = await _planetApllication.GetAllPlanets();
        // assert
        await _planetRepository.Received(1).GetAllAsync();            
        Assert.True(result.HasSuccess);
        Assert.NotNull(result.Data);
        Assert.Equal(result.HttpStatusCode, HttpStatusCode.OK);
    }

    [Fact(DisplayName = "Test unexpected error to get all planets")]
    [Trait("Planet", "GetAllPlanets with unexpected error")]
    public async void GetAllPlanetsUnexpectedErrorTest()
    {
        // arrange
        _planetRepository.GetAllAsync().ReturnsNull();
        // action
        var result = await _planetApllication.GetAllPlanets();
        // assert
        await _planetRepository.Received(1).GetAllAsync();            
        Assert.True(result.HasSuccess);
        Assert.Null(result.Data);
        Assert.Equal(result.HttpStatusCode, HttpStatusCode.OK);
    }
}
