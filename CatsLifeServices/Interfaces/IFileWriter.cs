using CatsLifeServices.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatsLifeServices.Interfaces
{
    internal interface IFileWriter
    {
        Task WriteAsync(CatFact fact);
    }
}
