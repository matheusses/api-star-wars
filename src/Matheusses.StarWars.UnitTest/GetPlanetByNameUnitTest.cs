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
    [Fact(DisplayName = "Test get planet by name")]
    [Trait("Planet", "GetPlanetByName Success")]
    public async void GetPlanetByNameTest()
    {
        // arrange
          var planetFake = PlanetFaker.Generate();
        _planetRepository.GetByNameAsync(planetFake.Name).Returns(Task.FromResult(planetFake));
        // action
        var result = await _planetApllication.GetPlanetByName(planetFake.Name);
        // assert
        await _planetRepository.Received(1).GetByNameAsync(planetFake.Name);            
        Assert.True(result.Success);
        Assert.Equal(result.HttpStatusCode, HttpStatusCode.OK);
    }

    [Fact(DisplayName = "Test get by name inexistent planet")]
    [Trait("Planet", "GetPlanetByName with inexist planet")]
    public async void GetPlanetByNameInexistentTest()
    {
        // arrange
          var planetFake = PlanetFaker.Generate();
        _planetRepository.GetByNameAsync(planetFake.Name).ReturnsNull();
        // action
        var result = await _planetApllication.GetPlanetByName(planetFake.Name);
        // assert
        await _planetRepository.Received(1).GetByNameAsync(planetFake.Name);            
        Assert.False(result.Success);
        Assert.Equal(result.HttpStatusCode, HttpStatusCode.NotFound);
    }
}
