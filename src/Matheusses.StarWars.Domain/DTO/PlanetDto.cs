using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Matheusses.StarWars.Domain.Model;

namespace Matheusses.StarWars.Domain.DTO
{
    public class PlanetDto
    {
        public int Id {get; init;}
        public String Name { get; init; }
        public String Climate { get; init; }
        public String Terrain { get; init; }
        public List<string> Films { get; init; }

        public Planet ConvertToPlanet(int id, List<Film> films){
            return new Planet{
                Climate = this.Climate,
                Id = id,
                Name = this.Name,
                Terrain = this.Terrain,
                Films = films,
            };
        }
        
    }
}