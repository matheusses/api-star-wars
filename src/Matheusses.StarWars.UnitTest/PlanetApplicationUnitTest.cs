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
}
