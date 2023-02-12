using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Matheusses.StarWars.Domain.Interfaces.Application;
using Microsoft.AspNetCore.Mvc;

namespace Matheusses.StarWars.WebApi.Controllers
{
    [ApiController]
    [Route("starwars/planets")]
    public class PlanetController : ControllerBase
    {
        private readonly IPlanetApplication _planetApplication;
        public PlanetController(
            IPlanetApplication planetApplication
        )
        {
            _planetApplication = planetApplication;
        }

        [HttpGet("loadexternal/{id}")]
        public async Task<IActionResult> GetExternalPlanet([FromRoute] string id)
        {
            var retorno = await _planetApplication.LoadPlanetByExternalApi(id);
            return Ok(retorno);
        }

        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            var retorno = await _planetApplication.GetAllPlanets();
            return Ok(retorno);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var retorno = await _planetApplication.GetPlanetById(id);
            return Ok(retorno);
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> Get([FromRoute] string name)
        {
            var retorno = await _planetApplication.GetPlanetByName(name);
            return Ok(retorno);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoverProduto([FromRoute] int id)
        {
            var retorno = await _planetApplication.RemovePlanet(id);
            return Ok(retorno);
        }
    }
}