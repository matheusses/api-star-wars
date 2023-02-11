using System.Reflection;
using AutoMapper;
using Matheusses.StarWars.Domain.Mappings;

namespace Matheusses.StarWars.WebApi.Extensions
{
    public static class AutoMapperExtensions
    {
        public static void AddAutoMapper(this IServiceCollection services, Assembly assembly)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperConfig());
            });
            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddAutoMapper(assembly);
        }
    }
}