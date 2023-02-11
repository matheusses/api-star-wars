using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Matheusses.StarWars.Domain.DTO
{
    public class PlanetDto
    {
        public int? Id {get; init;}
        public String Name { get; init; }
        public String Climate { get; init; }
        public String Terrain { get; init; }
        public DateTime? Created {get; init;}
        public List<string> Films { get; init; }
        
    }
}