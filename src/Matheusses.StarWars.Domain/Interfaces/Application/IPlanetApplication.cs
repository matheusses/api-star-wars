using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Matheusses.StarWars.Domain.Model;
using Matheusses.StarWars.Domain.DTO;

namespace Matheusses.StarWars.Domain.Interfaces.Application
{
    public interface IPlanetApplication
    {
        Task<Result<Planet>> LoadPlanetByExternalApi(string id);
        Task<Result<List<Planet>>> GetAllPlanets();
        Task<Result<Planet>> GetPlanetById(int id);
        Task<Result<Planet>> GetPlanetByName(string name);
        Task<Result<bool>> RemovePlanet(int id);
    }
}