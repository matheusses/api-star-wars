using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Matheusses.StarWars.Domain.DTO;
using Matheusses.StarWars.Domain.Model;

namespace Matheusses.StarWars.Domain.Mappings
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig(){
            CreateMap<PlanetDto, Planet>();
            CreateMap<FilmDto, Film>();
        }
    }
}