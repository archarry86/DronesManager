using DroneManager.Enums;
using DroneManager.Model;
using FileManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuCorrientazoDomicilioBussiness.File.DataAccess
{
    public class CoordinateLetter2DReader : IFileParser<CoordinateLetter2D>
    {


        private CoordinateLetter2D lastcoordinate = null;

        

        public CoordinateLetter2D[] read(string line, int indexline)
        {
            List<CoordinateLetter2D> readvalues = new List<CoordinateLetter2D>();

            if (line == null)
            {
                throw new ArgumentException("The line parameter can not be null");
            }

            if (line != String.Empty)
            {
              
                for (int index = 0; index < line.Length; index++)
                {

                    var letter = LeterCoordinates._none;
                   
                    if (Enum.TryParse<LeterCoordinates>(line[index].ToString(), out letter))
                    {

                        var newCordinate = new CoordinateLetter2D(letter, lastcoordinate);
                        readvalues.Add(newCordinate);
                        lastcoordinate = newCordinate;
                    }
                    else
                    {
                        throw new ArgumentException($"Invalid Character {index} at Line {indexline}.");
                    }
                }

            }

            return readvalues.ToArray();
        }



    }
}
