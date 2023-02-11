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

        public Task AddManyAsync(List<Film> entities)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Film>> GetAllAsync()
        {
            return await _context
                        .Films
                        .Find(p => true)
                        .ToListAsync();
        }

    }
}