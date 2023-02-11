using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Matheusses.StarWars.Domain.Model
{
    public class Film
    {
        [BsonId]
        public ObjectId InternalId { get; set; }
        public int Id {get; set;}
        public int PlanetId  {get; set;}
        public String Title {get; set;}
        public String Director {get; set;}
        public DateTime? ReleaseDate {get; set;}
    }
}