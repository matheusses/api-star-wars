using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Matheusses.StarWars.Domain.Model;

namespace Matheusses.StarWars.Domain.DTO
{
    public record FilmDto
    {
        public String? Title {get; init;}
        public String? Director {get; init;}
        [JsonPropertyName("release_date")]
        public string? ReleaseDate {get; init;}


    }
}