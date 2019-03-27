using _Forms_CompGraph_1_11_.Utils;

namespace _Forms_CompGraph_1_11_.Labs.SecondLab
{
    class SecondLabParameters : LabParameters
    {
        public Point2D[] SplainPoints { get; set; }
        public int SplainPow { get; set; }

        public SecondLabParameters(Point2D[] splainPoints, int splainPow)
        {
            SplainPoints = splainPoints;
            SplainPow = splainPow;
        }
    }
}
