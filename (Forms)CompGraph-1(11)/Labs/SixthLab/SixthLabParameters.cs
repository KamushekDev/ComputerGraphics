using _Forms_CompGraph_1_11_.Utils;

namespace _Forms_CompGraph_1_11_.Labs.SixthLab
{
    class SixthLabParameters : LabParameters
    {
        public SixthLabObject[] Objects { get; set; }
        public SixthLabLightSource[] LightSources { get; set; }

        public DoublePoint3D CameraPosition { get; set; }
        public DoublePoint2D CameraAngles { get; set; }
        public byte SuperSampling { get; set; }
    }
}
