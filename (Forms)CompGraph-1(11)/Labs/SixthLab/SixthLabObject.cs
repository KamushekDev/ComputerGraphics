using _Forms_CompGraph_1_11_.Utils;

namespace _Forms_CompGraph_1_11_.Labs.SixthLab
{
    class SixthLabObject
    {
        public object Object { get; set; }
        public string Type { get; set; }

        public SixthLabObject(DoublePoint3D center,double radius, byte[] color, float reflective, float transparency)
        {
            Object = new Sphere(center, radius, color, reflective, transparency);
            Type = "Sphere";
        }

        public SixthLabObject(DoublePoint3D[] points, byte[] color, float reflective, float transparency)
        {
            Object = new Hexahendron(points, color, reflective, transparency);
            Type = "Hexahendron";
        }
    }

    struct Sphere
    {
        public DoublePoint3D Center { get; set; }
        public double Radius { get; set; }
        public byte[] Color { get; set; }
        public float Reflective { get; set; }
        public float Transparency { get; set; }

        public Sphere(DoublePoint3D center, double radius, byte[] color, float reflective, float transparency)
        {
            Center = center;
            Radius = radius;
            Color = color;
            Reflective = reflective;
            Transparency = transparency;
        }
    }

    struct Hexahendron
    {
        public DoublePoint3D[] Points { get; set; }
        public byte[] Color { get; set; }
        public float Reflective { get; set; }
        public float Transparency { get; set; }

        public Hexahendron(DoublePoint3D[] points, byte[] color, float reflective, float transparency)
        {
            Points = points;
            Color = color;
            Reflective = reflective;
            Transparency = transparency;
        }
    }
}
