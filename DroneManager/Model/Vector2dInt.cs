using System;
using System.Collections.Generic;
using System.Text;

namespace DroneManager.Model
{
    public class Vector2dInt:IEquatable<Vector2dInt>
    {
        public static Vector2dInt Zero = new Vector2dInt(0, 0);
        public static Vector2dInt Up = new Vector2dInt(0, 1);
        public static Vector2dInt Down = new Vector2dInt(0, -1);
        public static Vector2dInt Left = new Vector2dInt(-1, 0);
        public static Vector2dInt Right = new Vector2dInt(1, 0);

        public int X { get; private set; }
        public int Y { get; private set; }

        public readonly double angle = 0;

        public Vector2dInt(int x, int y) {
            this.X = x;
            this.Y = y;
            angle = Angle();
        }


        public void  A(Vector2dInt N)
        {
            Vector2dInt AUX = this + N;
        }


        public static Vector2dInt operator +(Vector2dInt a, Vector2dInt b)
       => new Vector2dInt(a.X+ b.X, a.Y+b.Y);

        public static Vector2dInt operator -(Vector2dInt a, Vector2dInt b)
     => new Vector2dInt(a.X - b.X, a.Y - b.Y);


        public  Vector2dInt Rotate(  double degrees)
        {

            double rad = degrees* ( Math.PI / 180) % 360;

            return new Vector2dInt(
                (int)(this.X * Math.Cos(rad) - this.Y * (int) Math.Sin(rad)),
                (int)(this.X * Math.Sin(rad) + this.Y * (int)Math.Cos(rad))
            );
        }

        public double Angle()
        {
           double radians = Math.Atan2(Y, X);
           double angle = radians * (180 / Math.PI);


            if (angle < 0)
                angle += 360;

            angle = angle % 360;

           return angle;
        }


        public double YAngle()
        {
            double radians = Math.Atan2( X, Y);
            double angle = radians * (180 / Math.PI);


            if (angle < 0)
                angle += 360;

            angle = angle % 360;

            return angle;
        }

        public static Vector2dInt VectorFromAngle(double angle) {

            double rad = angle * (Math.PI / 180) % 360;
            return new Vector2dInt((int)Math.Cos(rad), (int)Math.Sin(rad));
        }


        public static Vector2dInt VectorFromYAngle(double angle)
        {

            double rad = angle * (Math.PI / 180) % 360;
            return new Vector2dInt( (int)Math.Sin(rad), (int)Math.Cos(rad));
        }



        public Vector2dInt CalculateNormal() {



            return this.Rotate(90);
            /*
            switch (angle) {

                case 0:
                    return Vector2d.up;
                    break;
                case 90:
                    return Vector2d.left;
                    break;
                case 180:
                    return Vector2d.down;
                    break;
                case 270:
                    return Vector2d.Right;
                    break;

            }*/

           // throw new Exception("Invalid Angle parameter");
        
        }

        public override bool Equals(Object other)
        {
            return Equals( other as Vector2dInt);
        }

        public bool Equals(Vector2dInt other)
        {
            return other != null && (other == this || (X== other.X && Y == other.Y));
        }

        public override string ToString()
        {
            return $"({X},{Y})";
        }

        public override int GetHashCode()
        {

            int hash = 130;

            hash = hash * 433 + X.GetHashCode();
            hash = hash * 433 + Y.GetHashCode();
            return hash;

        }


    }
}
