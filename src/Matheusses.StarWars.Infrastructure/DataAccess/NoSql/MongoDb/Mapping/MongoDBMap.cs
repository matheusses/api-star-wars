using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Matheusses.StarWars.Infrastructure.DataAccess.NoSql.MongoDb.Mapping
{
    public static class MongoDBMap
    {
        public static void Configure()
        {
            PlanetMap.Configure();
            FilmMap.Configure();
        }
    }
}