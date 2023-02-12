
using System.Text.Json.Serialization;

namespace Matheusses.StarWars.Domain.Model
{
    public class Film
    {
        public String Title {get; init;}
        public String Director {get; init;}
        public String ReleaseDate {get; init;}
    }
}