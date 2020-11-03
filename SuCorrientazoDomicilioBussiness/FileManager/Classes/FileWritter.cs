using SuCorrientazoDomicilioBussiness.FileManager.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SuCorrientazoDomicilioBussiness.FileManager.Classes
{
    public class FileWritter<T> : FileManager
    {
        public FileWritter(String directorypath, IFileWritter<T> _writter) : base(directorypath, true)
        {
            this.writter = _writter;

            if (this.writter == null) {
                throw new ArgumentNullException("writter parameter can not be null");
            }
        }

        public FileWritter(IFileWritter<T> _writter) : this(SuCorrientazoDomicilioBussiness.FileManager.Classes.FileManager.MyDirectoryFilesResult, _writter)
        {
          
        }



        public IFileWritter<T> writter { get; protected set; }



        /// <summary>
        /// Writes the information on a new file
        /// 
        /// pre: the file does no exits 
        /// </summary>
        /// <param name="filename"></param>
        public void WriteInformation(String filename)
        {

            string fullpath = Path.Combine(this.stringPath, filename);

            FileInfo info = new FileInfo(fullpath);


            if (info.Exists)
            {
                File.Delete(fullpath);
            }


            StreamWriter outputFile = info.CreateText();

            //using is like a try (objecttoclose){ objecttoclose.invokeanymethod() }catch(Exception e){}
            using (outputFile )
            {
               while(!writter.isDone){

                    outputFile.WriteLine(writter.nextNextLine());
                }
            }


        }
    }
}
