
using FileManager.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileManager.Classes
{
    public abstract class FileReader<T> : FileManager
    {
        private string directorypath;
      
        public FileReader(String directorypath, IFileParser<T> parser) : base(directorypath)
        {
            this.parser = parser;

            if (this.parser == null) {
                throw new ArgumentNullException("The parser can not be null ");
            }
        }

        public FileReader(IFileParser<T> parser) : this(FileManager.MyDirectoryFiles, parser)
        {
           
        }


        public IFileParser<T> parser { get; protected set; }




        public T[][] ReadInformation(String filename)
        {

            T[][] result = new T[0][];

            string line;

            string fullpath = Path.Combine(this.stringPath, filename);

            FileInfo info = new FileInfo(fullpath);

            if (!info.Exists)
            {
                throw new ArgumentException("The file does not exits");
            }


            int lineindex = 0;

            List<T[]> matrix = new List<T[]>();
            // Read the file and display it line by line.  
            using (System.IO.StreamReader streamReader =
                 new System.IO.StreamReader(fullpath))
            {

                while ((line = streamReader.ReadLine()) != null)
                {

                    matrix.Add(parser.read(line, lineindex));
                    lineindex++;
                }

            }

            result = matrix.ToArray();

            return result;
        }
    }
}
