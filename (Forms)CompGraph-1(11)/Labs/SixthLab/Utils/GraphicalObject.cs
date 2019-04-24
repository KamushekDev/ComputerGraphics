using _Forms_CompGraph_1_11_.Utils;

namespace _Forms_CompGraph_1_11_.Labs.SixthLab.Utils
{
    abstract class GraphicalObject : IAddable
    {
        public ColorRGB Color { get; set; }
        public int Specular { get; set; }
        public float Transparency { get; set; }
        public float Reflective { get; set; }

        public override string ToString()
        {
            return
                $"[{nameof(Color)}: {Color.ToString()}, {nameof(Specular)}: {Specular.ToString()}, {nameof(Transparency)}: {Transparency.ToString()}, {nameof(Reflective)}: {Reflective.ToString()}]";
        }
    }
}