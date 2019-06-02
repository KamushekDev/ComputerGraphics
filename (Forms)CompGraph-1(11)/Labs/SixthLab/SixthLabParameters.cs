using _Forms_CompGraph_1_11_.Labs.SixthLab.Utils;
using _Forms_CompGraph_1_11_.Utils;

namespace _Forms_CompGraph_1_11_.Labs.SixthLab
{
    class SixthLabParameters : LabParameters
    {
        public bool Initial { get; set; }
        public LightSource[] LightSources { get; set; }
        public GraphicalObject[] GraphicalObjects { get; set; }

        public DoublePoint3D CameraPosition { get; set; }
        public DoublePoint2D CameraRotation { get; set; }
    }
}