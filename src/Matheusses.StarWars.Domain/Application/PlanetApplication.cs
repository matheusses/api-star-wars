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

        public async Task<Result<List<Planet>>> GetAllPlanets()
        {
            var result = ResultBuilder<List<Planet>>.Create();
            try {
                var planets =  (List<Planet>)await _planetRepository.GetAllAsync();
                return result.WithSuccess(planets);
            }catch (Exception ex){
                switch (ex){
                    case InvalidOperationException:
                        result.WithError(ex.Message,ex , HttpStatusCode.NotFound);
                        break;
                    default:
                        result.WithException(errorMessage: ex.StackTrace ?? ex.Message);
                        break;
                }
                return result;
            }
        }

        public async Task<Result<Planet>> GetPlanetById(int id)
        {
            var result = ResultBuilder<Planet>.Create();
            try{
                var planet =  await _planetRepository.GetByIdAsync(id);
                if(planet == null)
                    throw new InvalidOperationException("Planet not exist");

                return result.WithSuccess(planet);
            }catch (Exception ex)
            {
                switch (ex){
                    case InvalidOperationException:
                        result.WithError(ex.Message,ex , HttpStatusCode.NotFound);
                        break;
                    default:
                        result.WithException(errorMessage: ex.StackTrace ?? ex.Message);
                        break;
                }
                return result;
            }
        }

        public async Task<Result<Planet>> GetPlanetByName(string name)
        {
            var result = ResultBuilder<Planet>.Create();
            try{
                var planet =  await _planetRepository.GetByNameAsync(name);
                if(planet == null)
                    throw new InvalidOperationException("Planet not exist");
                    
                return result.WithSuccess(planet);
            }catch (Exception ex)
            {
                switch (ex){
                    case InvalidOperationException:
                        result.WithError(ex.Message,ex , HttpStatusCode.NotFound);
                        break;
                    default:
                        result.WithException(errorMessage: ex.StackTrace ?? ex.Message);
                        break;
                }
                return result;
            }
        }

        public async Task<Result<Planet>> LoadPlanetByExternalApi(string id)
        {
            var result = ResultBuilder<Planet>.Create();
            try{
                int planetId = 0;
                if(!int.TryParse(id, out planetId))
                    throw new ArgumentException("Invalid input");
                
                Planet planet =  await _planetRepository.GetByIdAsync(planetId);
                if (planet != null)
                    return result.WithSuccess(planet);


                var planetDto = await _externalApiPlanet.GetAsync(id);

                if (planetDto is null)
                    throw new InvalidOperationException("Planet not found or External Api unavalaible");
                
                foreach(var url in planetDto.Films)
                {
                    var filmDto = await _externalApiFilm.GetByUrlAsync(url);
                    if (filmDto is null)
                       throw new InvalidOperationException("Planet not found or External Api unavalaible");
                    _films.Add(filmDto.ConverToFilm());
                }

                planet = planetDto.ConvertToPlanet(planetId, _films);
                await _planetRepository.AddAsync(planet);
            
                return result.WithSuccess(planet);

            }catch (Exception ex)
            {
                switch (ex){
                    case InvalidOperationException:
                        result.WithError(ex.Message,ex , HttpStatusCode.NotFound);
                        break;
                    case ArgumentException:
                        result.WithError(ex.Message,ex);
                        break;
                    default:
                        result.WithException(errorMessage: ex.StackTrace ?? ex.Message);
                        break;
                }
                return result;
            }

        }

        public async Task<Result<bool>> RemovePlanet(int id)
        {
            var result = ResultBuilder<bool>.Create();
            try{
                bool deleted = await _planetRepository.DeleteAsync(id);
                if(!deleted)
                    throw new InvalidOperationException("Planet not exist");
                return result.WithSuccess(deleted);
            }catch (Exception ex)
            {
                switch (ex){
                    case InvalidOperationException:
                        result.WithError(ex.Message,ex , HttpStatusCode.NotFound);
                        break;
                    default:
                        result.WithException(errorMessage: ex.StackTrace ?? ex.Message);
                        break;
                }
                return result;
            }
            
        }
    }
}