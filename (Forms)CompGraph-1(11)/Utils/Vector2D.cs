using System.Numerics;

namespace _Forms_CompGraph_1_11_.Utils
{
    /// <summary>
    /// Целочисленный вектор
    /// </summary>
    public struct Vector2D
    {
        private readonly Vector<int> _vector;

        public int X => _vector[2] - _vector[0];

        public int Y => _vector[3] - _vector[1];

        public int X0 => _vector[0];
        public int X1 => _vector[2];
        public int Y0 => _vector[1];
        public int Y1 => _vector[3];

        public Vector2D(int x, int y) : this(0, 0, x, y)
        {
        }

        public Vector2D(int x0, int y0, int x1, int y1)
        {
            _vector = new Vector<int>(new[] {x0, y0, x1, y1});
        }

        public Vector2D(Point2D first, Point2D second)
        {
            _vector = new Vector<int>(new[] {first.X, first.Y, second.X, second.Y});
        }

        private Vector2D(Vector<int> vector)
        {
            _vector = vector;
        }

        public static implicit operator Vector2D(Vector<int> vector)
        {
            return new Vector2D(vector);
        }

        public static Vector2D operator +(Vector2D a, Vector2D b)
        {
            return a._vector + b._vector;
        }

        public static Vector2D operator -(Vector2D a, Vector2D b)
        {
            return a._vector - b._vector;
        }

        public static Vector2D operator *(Vector2D a, Vector2D b)
        {
            return a._vector * b._vector;
        }

        public static Vector2D operator /(Vector2D a, Vector2D b)
        {
            return a._vector / b._vector;
        }

        public static Vector2D operator -(Vector2D a)
        {
            return -a._vector;
        }
    }
}