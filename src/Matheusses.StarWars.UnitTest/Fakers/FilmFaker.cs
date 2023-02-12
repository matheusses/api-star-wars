using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Matheusses.StarWars.Domain.DTO;
using Matheusses.StarWars.Domain.Model;

namespace Matheusses.StarWars.UnitTest.Fakers
{
    public static class FilmFaker
    {
        public static Film Generate()
        {
            Faker faker = new Faker();

            return new Film{
                Director = faker.Person.FullName,
                ReleaseDate = faker.Date.ToString(),
                Title = faker.Lorem.Sentence(30)
            };            
        }

        public static FilmDto GenerateDto()
        {
            Faker faker = new Faker();

            return new FilmDto{
                Director = faker.Person.FullName,
                ReleaseDate = faker.Date.ToString(),
                Title = faker.Lorem.Sentence(30)
            }; 
        }
        
    }
}