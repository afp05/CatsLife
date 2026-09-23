using CatsLifeServices.Interfaces;
using CatsLifeServices.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;


namespace CatsLife
{
    public class CatsLifeAppFlow
    {
      private readonly IGetFact _provider;
      private readonly IFileWriter _fileWriter;
      private readonly ILogger<CatsLifeAppFlow> _logger;
        public CatsLifeAppFlow(IGetFact provider, IFileWriter fileWriter, ILogger<CatsLifeAppFlow> logger)
        {
            _provider = provider;
            _fileWriter = fileWriter;
            _logger = logger;
        }

        public async Task<CatFact?> RunAsync()
        {
            try
            {
                var fact = await _provider.GetFactAsync();
                await _fileWriter.WriteAsync(fact);
                return fact;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"An HTTP error occurred while fetching the cat fact. See log for details.");
                _logger.LogError(ex, "An HTTP error occurred while fetching the cat fact. Details:");
                return null;
            }
            catch(JsonException ex)
            {
                Console.WriteLine($"A JSON error occurred while processing the cat fact. See log for details.");        
                _logger.LogError(ex, "A JSON error occurred while processing the cat fact. Details:");
                return null;
            }
            catch(IOException ex)
            {
                Console.WriteLine($"An I/O error occurred while writing the cat fact to file. See log for details.");
                _logger.LogError(ex, "An I/O error occurred while writing the cat fact to file. Details:");
                return null;
            }
           catch(UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Access denied while writing the cat fact to file. See log for details.");
                _logger.LogError(ex, "Access denied while writing the cat fact to file. Details:");
                return null;
            }
         
        }
    }
}

