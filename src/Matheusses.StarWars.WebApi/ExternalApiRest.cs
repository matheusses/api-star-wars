using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Matheusses.StarWars.Domain.Interfaces.ExternalApi;

namespace Matheusses.StarWars.WebApi
{
    public class ExternalApiRest<TYourType> : IExternalApiRest<TYourType> where TYourType : new()
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string  _url;

        public ExternalApiRest(IHttpClientFactory clientFactory, string url)
        {
            _clientFactory = clientFactory;
            _url = url;
        }

        public async Task<TYourType> GetAsync(string key)
        {
            var httpClient = _clientFactory.CreateClient();
            var result = await httpClient.GetFromJsonAsync<TYourType>($"{_url}/{key}");
            return result;
        }

        public async Task<TYourType> GetByUrlAsync(string url)
        {
            var httpClient = _clientFactory.CreateClient();
            var result = await httpClient.GetFromJsonAsync<TYourType>($"{url}");
            return result;
        }
    }
}