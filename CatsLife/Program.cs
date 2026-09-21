using CatsLife;
using CatsLifeServices.Interfaces;
using CatsLifeServices.Models;
using CatsLifeServices.Providers;
using CatsLifeServices.Writers;
using Microsoft.Extensions.DependencyInjection;



ServiceCollection services = new ServiceCollection();
services.AddTransient<IFileWriter, FileWriterTXT>();
services.AddTransient<IGetFact, ProviderAPI>();
services.AddTransient<CatsLifeAppFlow>();
services.AddHttpClient();

ServiceProvider serviceProvider = services.BuildServiceProvider();

CatsLifeAppFlow app = serviceProvider.GetRequiredService<CatsLifeAppFlow>();
await app.RunAsync();
CatFact fact = await app.RunAsync();

Console.WriteLine(fact.Fact);


//IGetFact provider = serviceProvider.GetRequiredService<IGetFact>();
//IFileWriter fileWriter = serviceProvider.GetRequiredService<IFileWriter>();

//HttpClient client = new HttpClient();
//ProviderAPI provider = new ProviderAPI(client);

//CatFact fact = await provider.GetFactAsync();
//FileWriterTXT filewritertxt = new FileWriterTXT();
//Console.WriteLine($"Length: {fact.Length}, Text: {fact.Fact}");
//await fileWriter.WriteAsync(fact);