using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Matheusses.StarWars.Domain.DTO;
using Matheusses.StarWars.Domain.Interfaces.Application;
using Matheusses.StarWars.Domain.Interfaces.ExternalApi;
using Matheusses.StarWars.Domain.Interfaces.Repository;
using Matheusses.StarWars.Domain.Model;

namespace Matheusses.StarWars.Domain.Application
{
    public sealed class PlanetApplication : IPlanetApplication
    {
        private readonly IPlanetRepository _planetRepository;
        private readonly IFilmRepository _filmRepository;
        private readonly IExternalApiRest<PlanetDto> _externalApiPlanet;
        private readonly IExternalApiRest<FilmDto> _externalApiFilm;
        private List<Film> _films;
        public PlanetApplication(
            IPlanetRepository planetRepository,
            IFilmRepository filmRepository,
            IExternalApiRest<PlanetDto> externalApiPlanet,
            IExternalApiRest<FilmDto> externalApiFilm
        )
        {
            _planetRepository = planetRepository;
            _filmRepository = filmRepository;
            _externalApiPlanet = externalApiPlanet;
            _externalApiFilm = externalApiFilm;
            _films = new List<Film>(4);

        }

        public async Task<List<Planet>> GetAllPlanets()
        {
            return (List<Planet>)await _planetRepository.GetAllAsync();
        }

        public async Task<Planet> GetPlanetById(int id)
        {
            return await _planetRepository.GetByIdAsync(id);
        }

        public async Task<Planet> GetPlanetByName(string name)
        {
            return await _planetRepository.GetByNameAsync(name);
        }

        public async Task<Planet> LoadPlanetByExternalApi(string id)
        {
            var planetDto = await _externalApiPlanet.GetAsync(id);
            
            foreach(var url in planetDto.Films)
            {
                var filmDto = await _externalApiFilm.GetByUrlAsync(url);
                _films.Add(filmDto.ConverToFilm());
            }
            var planet = planetDto.ConvertToPlanet(int.Parse(id), _films);
            await _planetRepository.AddAsync(planet);

            return planet;
        }

        public async Task<bool> RemovePlanet(int id)
        {
          return await _planetRepository.DeleteAsync(id);
        }
    }
}