using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Matheusses.StarWars.Domain.Model;
using MongoDB.Bson.Serialization;

namespace Matheusses.StarWars.Infrastructure.DataAccess.NoSql.MongoDb.Mapping
{
    public class PlanetMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<Planet>(map =>
            {
                map.AutoMap(); 
                map.SetIgnoreExtraElements(true);  
                map.MapIdField(p => p.Id);            
                map.MapCreator(p => new Planet(){
                    Climate = p.Climate,
                    Id = p.Id,
                    Terrain = p.Terrain,
                    Name = p.Name,
                    Films = p.Films,
                });
            });
        }
        
    }
}