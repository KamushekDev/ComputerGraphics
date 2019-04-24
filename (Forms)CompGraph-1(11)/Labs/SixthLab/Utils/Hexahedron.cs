using _Forms_CompGraph_1_11_.Utils;

namespace _Forms_CompGraph_1_11_.Labs.SixthLab.Utils
{
    class Hexahedron:GraphicalObject
    {
        public DoublePoint3D[] Points { get; set; } = new DoublePoint3D[8];

        public DoublePoint2D Rotation { get; set; }

        public override string ToString()
        {
            return $"(Hexahedron) {nameof(Points)}: [{string.Join(", ", Points)}, {nameof(Rotation)}: {Rotation}]. {base.ToString()}";
        }
    }
}
