using CatsLifeServices.Models;
using CatsLifeServices.Providers;
using CatsLifeServices.Writers;


HttpClient client = new HttpClient();
ProviderAPI provider = new ProviderAPI(client);

CatFact fact = await provider.GetFactAsync();
FileWriterTXT filewritertxt = new FileWriterTXT();
Console.WriteLine($"Length: {fact.Length}, Text: {fact.Fact}");
await filewritertxt.WriteAsync(fact);