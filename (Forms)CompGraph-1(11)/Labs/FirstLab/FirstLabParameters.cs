using System.Drawing;

namespace _Forms_CompGraph_1_11_.Labs.FirstLab
{
    class FirstLabParameters : LabParameters
    {
        public FirstLabParameters(Point firstCenter, int firstRadius, Point secondCenter, int secondRadius)
        {
            FirstCenter = firstCenter;
            FirstRadius = firstRadius;
            SecondCenter = secondCenter;
            SecondRadius = secondRadius;
        }

        public Point FirstCenter { get; set; }
        public int FirstRadius { get; set; }
        public Point SecondCenter { get; set; }
        public int SecondRadius { get; set; }
    }
}