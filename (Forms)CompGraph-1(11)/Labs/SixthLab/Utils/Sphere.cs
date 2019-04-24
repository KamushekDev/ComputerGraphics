using _Forms_CompGraph_1_11_.Utils;

namespace _Forms_CompGraph_1_11_.Labs.SixthLab.Utils
{
    class Sphere:GraphicalObject
    {
        public DoublePoint3D Center { get; set; }
        public double Radius { get; set; }

        public Sphere(DoublePoint3D center, double radius, ColorRGB color, int specular, float reflection, float transperansy)
        {
            Center = center;
            Radius = radius;
            Color = color;
            Specular = specular;
            Reflective = reflection;
            Transparency = transperansy;
        }

        public override string ToString()
        {
            return $"(Sphere) {nameof(Center)}: {Center}, {nameof(Radius)}: {Radius}. {base.ToString()}";
        }
    }
}
