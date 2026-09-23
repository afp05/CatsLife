using CatsLife;
using CatsLifeServices.Interfaces;
using CatsLifeServices.Models;
using CatsLifeServices.Providers;
using CatsLifeServices.Writers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;



ServiceCollection services = new ServiceCollection();
services.AddTransient<IFileWriter, FileWriterTXT>();
services.AddTransient<IGetFact, ProviderAPI>();
services.AddTransient<CatsLifeAppFlow>();
services.AddHttpClient();
services.AddLogging(builder => builder.AddConsole());

ServiceProvider serviceProvider = services.BuildServiceProvider();

CatsLifeAppFlow app = serviceProvider.GetRequiredService<CatsLifeAppFlow>();

CatFact? fact = await app.RunAsync();

if (fact != null)
{

    Console.WriteLine(fact.Fact);
}