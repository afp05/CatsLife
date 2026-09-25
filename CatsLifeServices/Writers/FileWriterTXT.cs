using CatsLifeServices.Interfaces;
using CatsLifeServices.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace CatsLifeServices.Writers
{
    public class FileWriterTXT : IFileWriter
    {
        private readonly IConfiguration _configuration;

        public FileWriterTXT(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task WriteAsync(CatFact fact)
        {
            string text = $"\"fact\": \"{fact.Fact}\", \"length\": {fact.Length}";
            string filePath = _configuration["Files:FactFilePath"] ?? throw new InvalidOperationException("FactFilePath is not configured.");
            string? directory = Path.GetDirectoryName(filePath);

            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }

            await File.AppendAllTextAsync(filePath, text + Environment.NewLine);
        }
    }
}
