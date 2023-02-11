using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Matheusses.StarWars.Domain.Interfaces.Repository;
using Matheusses.StarWars.Infrastructure.DataAccess.NoSql.MongoDb.Repository;

namespace Matheusses.StarWars.WebApi.Extensions
{
    public static class RepositoryExtensions
    {
        public static void AddRepository(this IServiceCollection servicos)
        {
            servicos.AddScoped<IPlanetRepository, PlanetRepository>();
            servicos.AddScoped<IFilmRepository, FilmRepository>();
        }
        
    }
}