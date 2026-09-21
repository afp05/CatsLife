using CatsLifeServices.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatsLifeServices.Interfaces
{
    public interface IFileWriter
    {
        Task WriteAsync(CatFact fact);
    }
}
