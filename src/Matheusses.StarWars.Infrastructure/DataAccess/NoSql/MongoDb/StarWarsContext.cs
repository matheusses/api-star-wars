using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Matheusses.StarWars.Domain.Model;
using MongoDB.Driver;

namespace Matheusses.StarWars.Infrastructure.DataAccess.NoSql.MongoDb
{
    public class StarWarsContext : IStarWarsContext
    {
        public StarWarsContext(IMongoDatabase db) {
            Planets = db.GetCollection<Planet>(typeof(Planet).Name);
            Films = db.GetCollection<Film>(typeof(Film).Name);
        }
        public IMongoCollection<Planet> Planets { get; set; }
        public IMongoCollection<Film> Films { get; set; }
    }
}