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
            ShowConfiguration();
            string? intervalValue = _configuration["TimeSettings:IntervalMilliSeconds"];
            string? maxFactsValue = _configuration["TimeSettings:MaxFacts"];


            if (!int.TryParse(maxFactsValue, out int maxFacts) || maxFacts <= 0)

            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid MaxFacts value. Program will use default value: 5");
                maxFacts = 5;
                Console.ResetColor();

            }

            if (!int.TryParse(intervalValue, out int intervalMilliSeconds) || intervalMilliSeconds < 0)

            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid IntervalMilliseconds value. Using default value: 1500 ms.");
                intervalMilliSeconds = 1500;
                Console.ResetColor();

            }

            for (int i = 0; i < maxFacts; i++)
            {

                try
                {
                    var fact = await _provider.GetFactAsync();
                    await _fileWriter.WriteAsync(fact);
                    Console.WriteLine(fact.Fact);

                    if (i < maxFacts - 1)
                    {
                        await Task.Delay(intervalMilliSeconds);
                    }
                }
                catch (HttpRequestException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"An HTTP error occurred while fetching the cat fact. See log for details.");
                    _logger.LogError(ex, "An HTTP error occurred while fetching the cat fact. Details:");
                    Console.ResetColor();

                }
                catch (JsonException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"A JSON error occurred while processing the cat fact. See log for details.");
                    _logger.LogError(ex, "A JSON error occurred while processing the cat fact. Details:");
                    Console.ResetColor();

                }
                catch (IOException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"An I/O error occurred while writing the cat fact to file. See log for details.");
                    _logger.LogError(ex, "An I/O error occurred while writing the cat fact to file. Details:");
                    Console.ResetColor();

                }
                catch (UnauthorizedAccessException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Access denied while writing the cat fact to file. See log for details.");
                    _logger.LogError(ex, "Access denied while writing the cat fact to file. Details:");
                    Console.ResetColor();
                }
            }

        }

        private void ShowConfiguration()
        {
            Console.ForegroundColor = ConsoleColor.Green;

            foreach (var section in _configuration.GetChildren())
            {
                Console.WriteLine(section.Key);

                foreach (var setting in section.GetChildren())
                {
                    Console.WriteLine($"  {setting.Key}: {setting.Value}");
                }
            }

            Console.WriteLine();
            Console.ResetColor();
        }
    }
}

