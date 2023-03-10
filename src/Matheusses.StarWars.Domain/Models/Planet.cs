using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Matheusses.StarWars.Domain.DTO;

namespace Matheusses.StarWars.Domain.Model
{
    public class Planet
    {
        public int Id { get; init; }
        public String? Name { get; init; }
        public String? Climate { get; init; }
        public String? Terrain { get; init; }
        public List<Film>? Films {get; init;}
    }
}