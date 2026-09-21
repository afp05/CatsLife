using CatsLifeServices.Interfaces;
using CatsLifeServices.Models;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace CatsLife
{
    public class CatsLifeAppFlow
    {
      private readonly IGetFact _provider;
      private readonly IFileWriter _fileWriter;
        public CatsLifeAppFlow(IGetFact provider, IFileWriter fileWriter)
        {
            _provider = provider;
            _fileWriter = fileWriter;
        }

        public async Task<CatFact> RunAsync()
        {
            var fact = await _provider.GetFactAsync();
            await _fileWriter.WriteAsync(fact);

            return fact;
        }
    }
}
