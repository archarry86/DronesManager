using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager.Interfaces
{
    public interface IFileWritter<T>
    {

        T objecttoserialize { get; }

        /// <summary>
        /// Returns true if wrote
        /// </summary>
        /// <returns></returns>
         String nextNextLine();

        /// <summary>
        /// Returns true if the information was already written
        /// </summary>
        bool isDone { get; }

    }
}
