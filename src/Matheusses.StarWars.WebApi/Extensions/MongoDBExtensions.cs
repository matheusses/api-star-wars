using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Matheusses.StarWars.Infrastructure.DataAccess.NoSql.MongoDb;
using Matheusses.StarWars.Infrastructure.DataAccess.NoSql.MongoDb.Mapping;
using MongoDB.Driver;

namespace Matheusses.StarWars.WebApi.Extensions
{
    public static class MongoDBExtensions
    {
        public static void AddMongoDB(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString =configuration.GetValue<string>("MongoDB:ConnectionStrings");
            var dataBase =configuration.GetValue<string>("MongoDB:DataBase");

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(dataBase);

            services.AddSingleton<IMongoClient>(client);
            services.AddSingleton<IMongoDatabase>(database);
            services.AddScoped<IStarWarsContext,StarWarsContext>();
            MongoDBMap.Configure();
        }
       
    }
}