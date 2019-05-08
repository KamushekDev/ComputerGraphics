using _Forms_CompGraph_1_11_.Utils;

namespace _Forms_CompGraph_1_11_.Labs.FourthLab
{
    class FourthLabParameters:LabParameters
    {
        public DoublePoint2D[] WindowPoints { get; set; }
        public DoublePoint2D[] FigurePoints { get; set; }

        public FourthLabParameters(DoublePoint2D[] windowPoints, DoublePoint2D[] figurePoints)
        {
            WindowPoints = windowPoints;
            FigurePoints = figurePoints;
        }
    }
}
