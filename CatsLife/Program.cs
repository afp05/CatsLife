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

var switchMapping = new Dictionary<string, string>
{
    { "-log", "Files:LogFilePath" },
    { "-fact", "Files:FactFilePath" },
    { "-url", "CatFactApi:Url" },
    { "-interval", "TimeSettings:IntervalMiliSeconds" },
    { "-count", "TimeSettings:MaxFacts" }
};
Directory.CreateDirectory("c:\\CatsLife");
IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddCommandLine(args, switchMapping)
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

