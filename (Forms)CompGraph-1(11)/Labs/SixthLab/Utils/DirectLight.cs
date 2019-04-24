using _Forms_CompGraph_1_11_.Utils;

namespace _Forms_CompGraph_1_11_.Labs.SixthLab.Utils
{
    class DirectLight : LightSource
    {
        public DoublePoint3D Vector { get; set; }

        public override string ToString()
        {
            return $"(Direct light) {nameof(Vector)}: {Vector.ToString()}. {base.ToString()}";
        }
    }
}