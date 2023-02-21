using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        private readonly IExternalApiRest<PlanetDto> _externalApiPlanet;
        private readonly IExternalApiRest<FilmDto> _externalApiFilm;
        private List<Film> _films;
        public PlanetApplication(
            IPlanetRepository planetRepository,
            IExternalApiRest<PlanetDto> externalApiPlanet,
            IExternalApiRest<FilmDto> externalApiFilm
        )
        {
            _planetRepository = planetRepository;
            _externalApiPlanet = externalApiPlanet;
            _externalApiFilm = externalApiFilm;
            _films = new List<Film>(4);

        }

        public async Task<Result<List<Planet>>> GetAllPlanets()
        {
            var result = Result<List<Planet>>.Create();
            var planets =  (List<Planet>)await _planetRepository.GetAllAsync();
            return result.WithSuccess(planets);
        }

        public async Task<Result<Planet>> GetPlanetById(int id)
        {
            var result = Result<Planet>.Create();
            var planet =  await _planetRepository.GetByIdAsync(id);
            if(planet == null)
                return result.WithError(
                "Planet not exist", 
                HttpStatusCode.NotFound
            );

            return result.WithSuccess(planet);
        }

        public async Task<Result<Planet>> GetPlanetByName(string name)
        {
            var result = Result<Planet>.Create();
            var planet =  await _planetRepository.GetByNameAsync(name);
            if(planet == null)
                  return result.WithError(
                    "Planet not exist", 
                    HttpStatusCode.NotFound
                );
                
            return result.WithSuccess(planet);
        }

        public async Task<Result<Planet>> LoadPlanetByExternalApi(string id)
        {
            var result = Result<Planet>.Create();
            try{

                int planetId = 0;

                if (!int.TryParse(id, out planetId))
                    return result.WithError("Invalid input");

                Planet planet = await _planetRepository.GetByIdAsync(planetId);
                if (planet != null)
                    return result.WithSuccess(planet);

                var planetDto = await _externalApiPlanet.GetAsync(id);
                if (planetDto is null)
                    return result.WithError(
                        "Planet not found or External Api unavailable", 
                        HttpStatusCode.NotFound
                    );

                var films = await LoadFilms(planetDto.Films);
                if (films == null)
                    return result.WithError(
                        "Planet not found or External Api unavailable", 
                        HttpStatusCode.NotFound
                    );

                planet = planetDto.ConvertToPlanet(planetId, films);
                await _planetRepository.AddAsync(planet);
                return result.WithSuccess(planet);
            }
            catch(Exception ex){
               return result.WithException(ex.ToString());
            }
        }

        private async Task<List<Film>> LoadFilms(List<string> filmUrls)
        {
            var films = new List<Film>();
            foreach (var url in filmUrls)
            {
                var filmDto = await _externalApiFilm.GetByUrlAsync(url);
                if (filmDto is null)
                    return null;

                Film film = filmDto;
                films.Add(film);
            }
            return films;
        }

        public async Task<Result<bool>> RemovePlanet(int id)
        {
            var result = Result<bool>.Create();
            bool deleted = await _planetRepository.DeleteAsync(id);
            if(!deleted)
                  return result.WithError(
                    "Planet not exist", 
                    HttpStatusCode.NotFound
                );
            return result.WithSuccess(deleted);
        }
    }
}