using CatsLifeServices.Interfaces;
using CatsLifeServices.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatsLifeServices.Writers
{
    public class FileWriterTXT : IFileWriter
    {
        public async Task WriteAsync(CatFact fact)
        {
            string text = $"\"fact\": \"{fact.Fact}\", \"length\": {fact.Length}";

            await File.AppendAllTextAsync("cats.txt",text + Environment.NewLine);
        }
    }
}
