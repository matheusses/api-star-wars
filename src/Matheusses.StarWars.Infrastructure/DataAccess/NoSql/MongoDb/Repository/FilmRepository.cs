using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Matheusses.StarWars.Domain.Interfaces.Repository;
using Matheusses.StarWars.Domain.Model;
using MongoDB.Driver;

namespace Matheusses.StarWars.Infrastructure.DataAccess.NoSql.MongoDb.Repository
{
    

    public class FilmRepository : IFilmRepository
    {
        private readonly IStarWarsContext _context;
        
        public FilmRepository(IStarWarsContext context){
            _context = context;
        }

        public async Task AddAsync(Film entity)
        {
           await _context.Films.InsertOneAsync(entity);
        }

        public async Task<bool> DeletarAsync(Film entity)
        {
            FilterDefinition<Film> filter = Builders<Film>.Filter.Eq(p => p.Id, entity.Id);
            DeleteResult deleteResult = await _context
                                                .Films
                                                .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged
                            && deleteResult.DeletedCount > 0;
        }

        public async Task<bool> DeleteByIdPlanetAsync(int idPlanet)
        {
            FilterDefinition<Film> filter = Builders<Film>.Filter.Eq(p => p.PlanetId, idPlanet);
            DeleteResult deleteResult = await _context
                                                .Films
                                                .DeleteManyAsync(filter);
            return deleteResult.IsAcknowledged
                            && deleteResult.DeletedCount > 0;
        }

        public async Task<IEnumerable<Film>> GetAllAsync()
        {
            return await _context
                        .Films
                        .Find(p => true)
                        .ToListAsync();
        }

        public async Task<IEnumerable<Film>> GetAllByIdPlanetAsync(int idPlanet)
        {
             return await _context
                        .Films
                        .Find(p => p.PlanetId == idPlanet)
                        .ToListAsync();
        }

        public async Task<Film> GetByIdAsync(int id)
        {
            return await _context
                  .Films
                  .Find(p => p.Id == id)
                  .FirstOrDefaultAsync();
        }
    }
}