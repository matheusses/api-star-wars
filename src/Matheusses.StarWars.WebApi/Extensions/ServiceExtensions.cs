using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Matheusses.StarWars.Domain.Application;
using Matheusses.StarWars.Domain.Interfaces.Application;
using Matheusses.StarWars.Domain.Interfaces.Repository;
using Matheusses.StarWars.Infrastructure.DataAccess.NoSql.MongoDb.Repository;

namespace Matheusses.StarWars.WebApi.Extensions
{
    public static class ServiceExtensions
    {
      public static void AddServices(this IServiceCollection services)
        {
            services.AddRepository();
            services.AddApplication();
        }


        private static void AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IPlanetRepository, PlanetRepository>();
        }
        private static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IPlanetApplication, PlanetApplication>();
        }
        
    }
}