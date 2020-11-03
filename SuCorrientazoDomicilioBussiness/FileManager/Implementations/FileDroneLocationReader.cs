﻿using DroneManager.Model;
using SuCorrientazoDomicilioBussiness.FileManager.Classes;
using SuCorrientazoDomicilioBussiness.FileManager.Implementations;
using SuCorrientazoDomicilioBussiness.FileManager.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace SuCorrientazoDomicilioBussiness.FileManager.Implementations
{

   
  

    public class FileDroneLocationReader : FileReader<CoordinateLetter2D>
    {

     

        public FileDroneLocationReader(String directorypath) : base(directorypath, new CoordinateLetter2DReader ())
        {

        }



        public string DroneSerial(string filename) {


            string pattern = @"[0-9]+";
           
            Match m = Regex.Match(filename, pattern, RegexOptions.IgnoreCase);

            if (m.Success)
              return  m.Value;

            throw new ArgumentException($"File Name {filename} does not contains Drone information.");
        }

    }


}
