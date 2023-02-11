using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Matheusses.StarWars.Domain.Model;
using MongoDB.Driver;

namespace Matheusses.StarWars.Infrastructure.DataAccess.NoSql.MongoDb
{
    public interface IStarWarsContext
    {
        public IMongoCollection<Planet> Planets { get; set; }
        public IMongoCollection<Film> Films { get; set; }
    }
}