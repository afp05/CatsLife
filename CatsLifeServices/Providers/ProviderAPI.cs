using CatsLifeServices.Interfaces;
using CatsLifeServices.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using static System.Net.WebRequestMethods;
using Microsoft.Extensions.Configuration;


namespace CatsLifeServices.Providers
{
    public class ProviderAPI : IGetFact
    {

        private readonly HttpClient _client;

        private readonly IConfiguration _configuration;

        public ProviderAPI(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _configuration = configuration;
        }
        public async Task<CatFact> GetFactAsync()
        {
            string URI = _configuration["CatFactApi:Url"] ?? throw new InvalidOperationException("API URL is missing in configuration");

            var result = await _client.GetAsync(URI);

            result.EnsureSuccessStatusCode();

            string json = await result.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            CatFact fact = JsonSerializer.Deserialize<CatFact>(json, options) ?? throw new JsonException("API returned null JSON");

            return fact;

        }

        
    }


}
