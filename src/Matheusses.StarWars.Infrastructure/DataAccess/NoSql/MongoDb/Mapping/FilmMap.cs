using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Matheusses.StarWars.Domain.Model;
using MongoDB.Bson.Serialization;

namespace Matheusses.StarWars.Infrastructure.DataAccess.NoSql.MongoDb.Mapping
{
    public class FilmMap
    {

        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<Film>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapCreator(p => new Film(){
                    Director = p.Director,
                    ReleaseDate = p.ReleaseDate,
                    Title = p.Title,
                });                             
            });
        }
        
    }
}