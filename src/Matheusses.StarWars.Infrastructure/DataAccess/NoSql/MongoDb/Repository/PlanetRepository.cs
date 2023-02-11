using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Matheusses.StarWars.Domain.Interfaces.Repository;
using Matheusses.StarWars.Domain.Model;
using MongoDB.Driver;

namespace Matheusses.StarWars.Infrastructure.DataAccess.NoSql.MongoDb.Repository
{
    

    public class PlanetRepository : IPlanetRepository
    {
        private readonly IStarWarsContext _context;
        
        public PlanetRepository(IStarWarsContext context){
            _context = context;
        }

        public async Task AddAsync(Planet entity)
        {
           await _context.Planets.InsertOneAsync(entity);
        }

        public async Task<bool> DeletarAsync(Planet entity)
        {
            FilterDefinition<Planet> filter = Builders<Planet>.Filter.Eq(p => p.Id, entity.Id);
            DeleteResult deleteResult = await _context
                                                .Planets
                                                .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged
                            && deleteResult.DeletedCount > 0;
        }

        public async Task<IEnumerable<Planet>> GetAllAsync()
        {
            return await _context
                        .Planets
                        .Find(p => true)
                        .ToListAsync();
        }

        public async Task<Planet> GetByIdAsync(int id)
        {
            return await _context
                  .Planets
                  .Find(p => p.Id == id)
                  .FirstOrDefaultAsync();
        }

        public async Task<Planet> GetByNameAsync(string name)
        {
            return await _context
                  .Planets
                  .Find(p => p.Name.Equals(name))
                  .FirstOrDefaultAsync();
        }
    }
}