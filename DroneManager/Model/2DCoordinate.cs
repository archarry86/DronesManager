using DroneManager.Enums;
using DroneManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DroneManager.Model
{
    public class CoordinateLetter2D : I2DPosition
    {


        public static readonly CoordinateLetter2D zero = new CoordinateLetter2D(Vector2dInt.zero, 90);
        private Vector2dInt _position = Vector2dInt.zero;
        public Vector2dInt Position { get { return _position; } }

        public double Angle { get; private set; }




        private readonly LeterCoordinates leter_coordinate;

        private CoordinateLetter2D()
        {

            throw new InvalidOperationException();
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
                    var direction = Vector2dInt.VectorFromAngle(parameter.Angle);
                    _position = parameter.Position + direction;

                    break;
                case LeterCoordinates.I:
                    // rotar


                    Angle = (parameter.Angle + 90);

                    Angle = Angle % 360;




                    _position = parameter.Position;

                    break;
                case LeterCoordinates.D:
                    // rotar




                    Angle = (parameter.Angle - 90);
                    if (Angle < 0)
                    {
                        Angle = 360 + Angle;
                    }
                    Angle = Angle % 360;


                    _position = parameter.Position;

                    break;

                default:
                    throw new Exception("Invalid Leter Coordinate");
                    break;
            }
        }

        public string Position_String()
        {
            return this._position.ToString();
        }

        public string Position_Representation_String()
        {
            return leter_coordinate.ToString();
        }



    }
}
