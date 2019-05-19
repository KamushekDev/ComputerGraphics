using System;
using _Forms_CompGraph_1_11_.Utils;

namespace _Forms_CompGraph_1_11_.Labs.SixthLab.Utils
{
    class Hexahedron : GraphicalObject
    {
        public DoublePoint3D[] Points { get; set; } = new DoublePoint3D[8];

        public DoublePoint2D Rotation { get; set; }

        public Hexahedron()
        {
            
        }

        public Hexahedron(DoublePoint3D[] points, DoublePoint2D rotation, ColorRGB color, int specular,
            float transparency, float reflective) :
            base(color, specular, transparency, reflective)
        {
            if (Points.Length != 8)
                throw new ArgumentException(
                    $"{nameof(points)} has wrong number of points! Expected 8 point, actually {points.Length}");

            points.CopyTo(Points, 0);
            Rotation = rotation;
        }

        public override string ToString()
        {
            return
                $"(Hexahedron) {nameof(Points)}: [{string.Join(", ", Points)}, {nameof(Rotation)}: {Rotation}]. {base.ToString()}";
        }
    }
}