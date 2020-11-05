using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager.Interfaces
{
    public interface IFileParser<T>
    {
         T[] read(String line, int indexline);

    }
}
