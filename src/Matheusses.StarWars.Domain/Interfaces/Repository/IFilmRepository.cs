using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Matheusses.StarWars.Domain.Model;

namespace Matheusses.StarWars.Domain.Interfaces.Repository
{
    public interface IFilmRepository: IRepository<Film>
    {
          Task<IEnumerable<Film>> GetAllByIdPlanetAsync(int idPlanet);
          Task<bool> DeleteByIdPlanetAsync(int idPlanet);
    }
}