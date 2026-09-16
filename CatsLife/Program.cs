using CatsLifeServices.Models;
using CatsLifeServices.Providers;


HttpClient client = new HttpClient();
ProviderAPI provider = new ProviderAPI(client);

CatFact fact = await provider.GetFactAsync();

Console.WriteLine($"Length: {fact.Length}, Text: {fact.Fact}");