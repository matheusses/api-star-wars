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
    [Fact(DisplayName = "Test remove planet")]
    [Trait("Planet", "RemovePlanet Success")]
    public async void RemovePlanetTest()
    {
        // arrange
        int input = 1;
        _planetRepository.DeleteAsync(input).Returns(Task.FromResult(true));
        // action
        var result = await _planetApllication.RemovePlanet(input);
        // assert
        await _planetRepository.Received(1).DeleteAsync(input);            
        Assert.True(result.Success);
        Assert.Equal(result.HttpStatusCode, HttpStatusCode.OK);
    }

    [Fact(DisplayName = "Test remove inexistent planet")]
    [Trait("Planet", "RemovePlanet with inexist planet")]
    public async void RemovePlanetInexistentTest()
    {
        // arrange
        int input = 1;
        _planetRepository.DeleteAsync(input).Returns(Task.FromResult(false));
        // action
        var result = await _planetApllication.RemovePlanet(input);
        // assert
        await _planetRepository.Received(1).DeleteAsync(input);            
        Assert.False(result.Success);
        Assert.Equal(result.HttpStatusCode, HttpStatusCode.NotFound);
    }
}
