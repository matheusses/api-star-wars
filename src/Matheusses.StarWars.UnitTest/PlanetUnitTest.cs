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
    private readonly PlanetApplication _planetApllication;

    private readonly IPlanetRepository _planetRepository =  Substitute.For<IPlanetRepository>();
    private readonly IExternalApiRest<PlanetDto> _externalApiPlanet = Substitute.For<IExternalApiRest<PlanetDto>>();
    private readonly IExternalApiRest<FilmDto> _externalApiFilm = Substitute.For<IExternalApiRest<FilmDto>>();

    public PlanetApplicationUnitTest()
    {
        _planetApllication = new PlanetApplication(
            _planetRepository,
            _externalApiPlanet,
            _externalApiFilm
        );
        
    }
}
