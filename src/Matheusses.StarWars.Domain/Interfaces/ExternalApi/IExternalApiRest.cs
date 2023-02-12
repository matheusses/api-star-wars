using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Matheusses.StarWars.Domain.Interfaces.ExternalApi
{
    public interface IExternalApiRest<TYourType> where TYourType : new()
    {
        Task<TYourType> GetAsync(string key);  
        Task<TYourType> GetByUrlAsync(string url);      
    }
}