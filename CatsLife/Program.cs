using CatsLife;
using CatsLifeServices.Interfaces;
using CatsLifeServices.Models;
using CatsLifeServices.Providers;
using CatsLifeServices.Writers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Logging;
using Microsoft.Extensions.Configuration;

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

Log.Logger = new LoggerConfiguration()
    .WriteTo.File(configuration["Files:LogFilePath"] ?? throw new InvalidOperationException("LogFilePath is not configured."))
    .CreateLogger();  

ServiceCollection services = new ServiceCollection();

services.AddSingleton<IConfiguration>(configuration);
services.AddTransient<IFileWriter, FileWriterTXT>();
services.AddTransient<IGetFact, ProviderAPI>();
services.AddTransient<CatsLifeAppFlow>();
services.AddHttpClient();
services.AddLogging(builder => builder.AddSerilog(Log.Logger) );

ServiceProvider serviceProvider = services.BuildServiceProvider();

CatsLifeAppFlow app = serviceProvider.GetRequiredService<CatsLifeAppFlow>();

 await app.RunAsync();

