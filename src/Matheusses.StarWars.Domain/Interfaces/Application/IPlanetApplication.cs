using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Matheusses.StarWars.Domain.Model;

namespace Matheusses.StarWars.Domain.Interfaces.Application
{
    public interface IPlanetApplication
    {
        Task<Planet> LoadPlanetByExternalApi(string id);
        Task<List<Planet>> GetAllPlanets();
        Task<Planet> GetPlanetById(int id);
        Task<Planet> GetPlanetByName(string name);
        Task<bool> RemovePlanet(int id);
    }
}