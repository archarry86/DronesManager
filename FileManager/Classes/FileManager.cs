using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileManager.Classes
{
    public abstract class FileManager
    {


        public const String MyDirectoryFiles = @"D:\Files\Read\";


        public const String MyDirectoryFilesResult = @"D:\Files\Result\";

        protected String stringPath { get; private set; }


        protected FileManager(String _Path)
        {
            stringPath = _Path;

            if (string.IsNullOrEmpty(stringPath))
                throw new ArgumentException("Invalid path.");

            DirectoryInfo directoryInfo = new DirectoryInfo(stringPath);

            if (!directoryInfo.Exists)
            {
                throw new ArgumentException("The directory does not exists.");
            }
        }


        protected FileManager(String _Path, bool createdirectory)
        {
            stringPath = _Path;

            if (string.IsNullOrEmpty(stringPath))
                throw new ArgumentException("Invalid path.");

            DirectoryInfo directoryInfo = new DirectoryInfo(stringPath);

            if (!directoryInfo.Exists )
            {
                if(createdirectory)
                    directoryInfo.Create();
                else
                    throw new ArgumentException("The directory does not exists.");
            }
            
        }
    }

}
