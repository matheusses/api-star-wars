using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Matheusses.StarWars.Domain.DTO
{
    public class FilmDto
    {
        public String Title {get; init;}
        public String Director {get; init;}
        public DateTime? ReleaseDate {get; init;}
    }
}