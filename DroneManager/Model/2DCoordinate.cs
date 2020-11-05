using DroneManager.Enums;
using DroneManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DroneManager.Model
{
    public class CoordinateLetter2D : I2DPosition
    {


        public static readonly CoordinateLetter2D zero = new CoordinateLetter2D(Vector2dInt.Zero, 0);
        private Vector2dInt _position = Vector2dInt.Zero;
        public Vector2dInt Position { get { return _position; } }

        public double Angle { get; private set; }




        private readonly LeterCoordinates leter_coordinate;

        private CoordinateLetter2D()
        {

          
        }

        private CoordinateLetter2D(Vector2dInt position, double angle)
        {
            _position = position;
            this.Angle = angle;


        }

        

        public CoordinateLetter2D(LeterCoordinates letter, CoordinateLetter2D parameter = null)
        {

            //this is kind of ugly but I handle the scenario
            if (parameter == null)
            {
                parameter = zero;


            }

            this.leter_coordinate = letter;
            switch (this.leter_coordinate)
            {
                case LeterCoordinates.A:

                    Angle = parameter.Angle;
                    var direction = Vector2dInt.VectorFromYAngle(Angle);
                    _position = parameter.Position + direction;

                    break;
                case LeterCoordinates.I:
                    // rotar


                    Angle = (parameter.Angle - 90);

                    
                    if (Angle < 0)
                    {
                        Angle = 360 + Angle;
                    }
                    Angle = Angle % 360;



                    _position = parameter.Position;

                    break;
                case LeterCoordinates.D:
                    // rotar




                    Angle = (parameter.Angle + 90);
                    Angle = Angle % 360;


                    _position = parameter.Position;

                    break;

                default:
                    throw new Exception("Invalid Leter Coordinate");
                    break;
            }
        }

        private Vector2dInt VectorFromAngle(double angle)
        {
            switch (angle) {
                case 0:
                    return Vector2dInt.Up;
                case 90:
                    return Vector2dInt.Right;
                case 180:
                    return Vector2dInt.Down;
                case 270:
                    return Vector2dInt.Left;
                case 360:
                    return Vector2dInt.Up;
            }

            throw new ArgumentException($"Invalid Angle {angle}");
        }

        public string Position_String()
        {
            return this._position.ToString()+" "+Angle.ToString();
        }

        public string Position_Representation_String()
        {
            return leter_coordinate.ToString();
        }



    }
}
