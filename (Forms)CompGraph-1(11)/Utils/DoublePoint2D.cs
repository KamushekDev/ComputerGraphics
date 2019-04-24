using System.Drawing;

namespace _Forms_CompGraph_1_11_.Utils
{
    public struct DoublePoint2D
    {
        public double X { get; set; }
        public double Y { get; set; }

        public DoublePoint2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public PointF ToPointF()
        {
            return new PointF((float) X, (float) Y);
        }

        public static DoublePoint2D operator +(DoublePoint2D a, DoublePoint2D b)
        {
            return new DoublePoint2D(a.X + b.X, a.Y + b.Y);
        }

        public static DoublePoint2D operator -(DoublePoint2D a, DoublePoint2D b)
        {
            return new DoublePoint2D(a.X - b.X, a.Y - b.Y);
        }

        public static DoublePoint2D operator *(DoublePoint2D point, int factor)
        {
            return new DoublePoint2D(point.X * factor, point.Y * factor);
        }

        public static DoublePoint2D operator /(DoublePoint2D point, int divider)
        {
            return new DoublePoint2D(point.X / divider, point.Y / divider);
        }

        public override string ToString()
        {
            return $"[{nameof(X)}: {X}, {nameof(Y)}: {Y}]";
        }
    }
}