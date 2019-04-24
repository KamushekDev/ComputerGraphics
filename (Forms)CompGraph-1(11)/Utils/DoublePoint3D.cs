using System;

namespace _Forms_CompGraph_1_11_.Utils
{
    public struct DoublePoint3D
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public DoublePoint3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public DoublePoint3D(DoublePoint2D xy, double z)
        {
            X = xy.X;
            Y = xy.Y;
            Z = z;
        }

        public DoublePoint3D(DoublePoint2D xy)
        {
            X = xy.X;
            Y = xy.Y;
            Z = 0;
        }

        public double Length()
        {
            return Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        public DoublePoint3D VecMultiply(DoublePoint3D vector)
        {
            var x = Y * vector.Z - vector.Y * Z;
            var y = x * vector.Z - vector.X * Z;
            var z = X * vector.Y - vector.X * Y;
            return new DoublePoint3D(x, y, z);
        }

        public double ScalMultiply(DoublePoint3D vector)
        {
            return X * vector.X + Y * vector.Y + Z * vector.Z;
        }

        public DoublePoint3D RotateX(double angle)
        {
            var x = X;
            var y = Y * Math.Cos(angle) - Z * Math.Sin(angle);
            var z = Y * Math.Sin(angle) + Z * Math.Cos(angle);

            return new DoublePoint3D(x, y, z);
        }

        public DoublePoint3D RotateY(double angle)
        {
            var x = X * Math.Cos(angle) + Z * Math.Sin(angle);
            var y = Y;
            var z = -X * Math.Sin(angle) + Z * Math.Cos(angle);

            return new DoublePoint3D(x, y, z);
        }

        public override string ToString()
        {
            return $"[{X}, {Y}, {Z}]";
        }

        #region Operators
        public static DoublePoint3D operator +(DoublePoint3D a, DoublePoint3D b)
        {
            return new DoublePoint3D(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static DoublePoint3D operator -(DoublePoint3D a, DoublePoint3D b)
        {
            return new DoublePoint3D(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static DoublePoint3D operator -(DoublePoint3D a)
        {
            return new DoublePoint3D(-a.X, -a.Y, -a.Z);
        }

        public static DoublePoint3D operator *(DoublePoint3D a, double b)
        {
            return new DoublePoint3D(a.X * b, a.Y * b, a.Z * b);
        }

        public static DoublePoint3D operator *(double a, DoublePoint3D b)
        {
            return new DoublePoint3D(a * b.X, a * b.Y, a * b.Z);
        }

        public static DoublePoint3D operator /(DoublePoint3D a, double b)
        {
            return new DoublePoint3D(a.X / b, a.Y / b, a.Z / b);
        }
        #endregion
    }
}
