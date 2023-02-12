
using Bogus;
using Matheusses.StarWars.Domain.DTO;
using Matheusses.StarWars.Domain.Model;

namespace Matheusses.StarWars.UnitTest.Fakers
{
    public static class PlanetFaker
    {
        public static Planet Generate(int filmQuantity = 1)
        {
            Faker faker = new Faker();
            List<Film> films = new List<Film>(filmQuantity);
            for(int i=0; i < filmQuantity; i++){
                films.Add(FilmFaker.Generate());
            }

            return new Planet{
                Climate = faker.Name.FindName(),
                Id = faker.Random.Int(1,2147483647),
                Name = faker.Name.Locale,
                Terrain = faker.Name.FindName(),
                Films = films
            };            
        }

        public static PlanetDto GenerateDto(int filmQuantity = 1)
        {
            Faker faker = new Faker();

            List<String> films = new List<String>(filmQuantity);
            for(int i=0; i < filmQuantity; i++){
                films.Add(faker.Internet.Url());
            }

            return new PlanetDto{
                Climate = faker.Name.FindName(),
                Id = faker.Random.Int(1,2147483647),
                Name = faker.Name.Locale,
                Terrain = faker.Name.FindName(),
                Films = films
            }; 
        }
        
    }
}