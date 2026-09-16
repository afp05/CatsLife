using CatsLifeServices.Interfaces;
using CatsLifeServices.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace CatsLifeServices.Providers
{
    public class ProviderAPI : IGetFact
    {

        private readonly HttpClient _client;

        public ProviderAPI(HttpClient client)
        {
            _client = client;   
        }
        public async Task<CatFact> GetFactAsync()
        {
            string URI = "https://catfact.ninja/fact";
                
            var result = await _client.GetAsync(URI);

            string json = await result.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            CatFact fact = JsonSerializer.Deserialize<CatFact>(json, options) ?? new CatFact();

            return fact;

        }

        
    }


}
