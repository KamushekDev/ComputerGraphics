using _Forms_CompGraph_1_11_.Utils;

namespace _Forms_CompGraph_1_11_.Labs.SixthLab.Utils
{
    class PointLight:LightSource
    {
        public DoublePoint3D Coord { get; set; }

        public PointLight(DoublePoint3D coord, float intense):base(intense)
        {
            Coord = coord;
        }

        public override string ToString()
        {
            return $"(Point light) {nameof(Coord)}: {Coord.ToString()}. {base.ToString()}";
        }
    }
}
