using _Forms_CompGraph_1_11_.Utils;

namespace _Forms_CompGraph_1_11_.Labs.SixthLab
{
    class SixthLabLightSource
    {
        public object LightSource { get; set; }
        public string Type { get; set; }

        public SixthLabLightSource(DoublePoint3D point, float intensity,string type)
        {
            if (type == "Point")
                LightSource = new PointLightSource(point, intensity);
            Type = type;
        }

        public SixthLabLightSource(float intensity)
        {
            LightSource = new AmbientLightSource(intensity);
            Type = "Ambient";
        }
    }

    struct PointLightSource
    {
        public DoublePoint3D Point { get; set; }
        public float Intensity { get; set; }

        public PointLightSource(DoublePoint3D point, float intensity)
        {
            Point = point;
            Intensity = intensity;
        }
    }


    struct AmbientLightSource
    {
        public float Intensity { get; set; }

        public AmbientLightSource(float intensity)
        {
            Intensity = intensity;
        }
    }
}
