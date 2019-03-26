using System.Drawing;

namespace _Forms_CompGraph_1_11_.Utils
{
    public struct Point2D
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point2D(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static implicit operator Point2D(Point point)
        {
            return new Point2D(point.X, point.Y);
        }

        public static Point2D operator +(Point2D a, Point2D b)
        {
            return new Point2D(a.X + b.X, a.Y + b.Y);
        }

        public static Point2D operator -(Point2D a, Point2D b)
        {
            return new Point2D(a.X - b.X, a.Y - b.Y);
        }

        public static Point2D operator *(Point2D point, int factor)
        {
            return new Point2D(point.X * factor, point.Y * factor);
        }

        public static DoublePoint2D operator *(Point2D point, float factor)
        {
            return new DoublePoint2D(point.X * factor, point.Y * factor);
        }
    }
}