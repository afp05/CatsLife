using CatsLifeServices.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;


namespace CatsLife
{
    public class CatsLifeAppFlow
    {
      private readonly IGetFact _provider;
      private readonly IFileWriter _fileWriter;
      private readonly ILogger<CatsLifeAppFlow> _logger;
      private readonly IConfiguration _configuration;   
      
        public CatsLifeAppFlow(IGetFact provider, IFileWriter fileWriter, ILogger<CatsLifeAppFlow> logger, IConfiguration configuration)
        {
            _provider = provider;
            _fileWriter = fileWriter;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task RunAsync()
        {           
            
            int maxFacts = _configuration.GetValue<int>("TimeSettings:MaxFacts");
            int intervalMiliSeconds = _configuration.GetValue<int>("TimeSettings:IntervalMiliSeconds");

            
            for (int i = 0; i < maxFacts; i++)
            {
                
                try
                {
                    var fact = await _provider.GetFactAsync();
                    await _fileWriter.WriteAsync(fact);            
                    Console.WriteLine(fact.Fact);

                    if (i < maxFacts - 1)
                    {
                        await Task.Delay(intervalMiliSeconds);
                    }
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"An HTTP error occurred while fetching the cat fact. See log for details.");
                    _logger.LogError(ex, "An HTTP error occurred while fetching the cat fact. Details:");
                  
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"A JSON error occurred while processing the cat fact. See log for details.");
                    _logger.LogError(ex, "A JSON error occurred while processing the cat fact. Details:");
                  
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"An I/O error occurred while writing the cat fact to file. See log for details.");
                    _logger.LogError(ex, "An I/O error occurred while writing the cat fact to file. Details:");
                   
                }
                catch (UnauthorizedAccessException ex)
                {
                    Console.WriteLine($"Access denied while writing the cat fact to file. See log for details.");
                    _logger.LogError(ex, "Access denied while writing the cat fact to file. Details:");
                 
                }
            }
        
        }
    }
}

