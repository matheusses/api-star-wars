
using System.Text.Json.Serialization;
using Matheusses.StarWars.Domain.DTO;

namespace Matheusses.StarWars.Domain.Model
{
    public class Film
    {
        public String? Title {get; init;}
        public String? Director {get; init;}
        public String? ReleaseDate {get; init;}

        public static implicit operator Film(FilmDto dto)
        {
            return new Film{
                Title = dto.Title,
                Director = dto.Director,
                ReleaseDate = dto.ReleaseDate,
            };
        }
    }
}