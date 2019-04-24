using _Forms_CompGraph_1_11_.Utils;

namespace _Forms_CompGraph_1_11_.Labs.SixthLab.Utils
{
    class Sphere : GraphicalObject
    {
        public DoublePoint3D Center { get; set; }
        public double Radius { get; set; }

        public Sphere(DoublePoint3D center, double radius, ColorRGB color, int specular, float reflective,
            float transparency) :
            base(color, specular, transparency, reflective)
        {
            Center = center;
            Radius = radius;
        }

        public override string ToString()
        {
            return $"(Sphere) {nameof(Center)}: {Center}, {nameof(Radius)}: {Radius}. {base.ToString()}";
        }
    }
}