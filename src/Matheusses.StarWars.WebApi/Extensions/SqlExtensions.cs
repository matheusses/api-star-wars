
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Matheusses.StarWars.Infrastructure.DataAccess.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace Matheusses.StarWars.WebApi.Extensions
{
    public static class SqlExtensions
    {
        public static void AddSqlDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContextPool<StarWarsDBContext>(options =>
              options.UseMySql(connString,
                  ServerVersion.AutoDetect(connString)));
 

        }
    }
}