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
    [Fact(DisplayName = "Test get planet by id")]
    [Trait("Planet", "GetPlanetById Success")]
    public async void GetPlanetByIdTest()
    {
        // arrange
          var planetFake = PlanetFaker.Generate();
        _planetRepository.GetByIdAsync(planetFake.Id).Returns(Task.FromResult(planetFake));
        // action
        var result = await _planetApllication.GetPlanetById(planetFake.Id);
        // assert
        await _planetRepository.Received(1).GetByIdAsync(planetFake.Id);            
        Assert.True(result.HasSuccess);
        Assert.Equal(result.HttpStatusCode, HttpStatusCode.OK);
    }

    [Fact(DisplayName = "Test get by id inexistent planet")]
    [Trait("Planet", "GetPlanetById with inexist planet")]
    public async void GetPlanetByIdInexistentTest()
    {
        // arrange
          var planetFake = PlanetFaker.Generate();
        _planetRepository.GetByIdAsync(planetFake.Id).ReturnsNull();
        // action
        var result = await _planetApllication.GetPlanetById(planetFake.Id);
        // assert
        await _planetRepository.Received(1).GetByIdAsync(planetFake.Id);            
        Assert.False(result.HasSuccess);
        Assert.Equal(result.HttpStatusCode, HttpStatusCode.NotFound);
    }
}
