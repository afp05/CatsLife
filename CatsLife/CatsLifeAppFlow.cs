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
                
                _logger.LogError(ex, "An HTTP error occurred while fetching the cat fact.");
                return null;
            }
            catch(JsonException ex)
            {
                _logger.LogError(ex, "A JSON error occurred while processing the cat fact.");
                return null;
            }
            catch(IOException ex)
            {
                _logger.LogError(ex, "An I/O error occurred while writing the cat fact to file.");
                return null;
            }
           catch(UnauthorizedAccessException ex)
            {
                _logger.LogError(ex, "Access denied while writing the cat fact to file.");
                return null;
            }
         
        }
    }
}

