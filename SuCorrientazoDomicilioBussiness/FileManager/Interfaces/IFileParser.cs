using System;
using System.Collections.Generic;
using System.Text;

namespace SuCorrientazoDomicilioBussiness.FileManager.Interfaces
{
    public interface IFileParser<T>
    {
         T[] read(String line, int indexline);

    }
}
