using _Forms_CompGraph_1_11_.Utils;

namespace _Forms_CompGraph_1_11_.Labs.ThirdLab
{
    class ThirdLabParameters : LabParameters
    {
        public DoublePoint2D[] AreaPointsXY { get; set; }
        public double[] AreaPointZ { get; set; }
        public double XRotateDegree { get; set; }
        public double YRotateDegree { get; set; }

        public ThirdLabParameters(DoublePoint2D[] areaPointsXY, double[] areaPointZ, double xRotateDegree, double yRotateDegree)
        {
            AreaPointsXY = areaPointsXY;
            AreaPointZ = areaPointZ;
            XRotateDegree = xRotateDegree;
            YRotateDegree = yRotateDegree;
        }
    }
}
