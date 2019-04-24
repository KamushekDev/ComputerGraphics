namespace _Forms_CompGraph_1_11_.Labs.SixthLab.Utils
{
    abstract class LightSource : IAddable
    {
        public float Intense { get; set; }

        public LightSource(float intensity)
        {
            Intense = intensity;
        }

        public override string ToString()
        {
            return $"[{nameof(Intense)}: {Intense}]";
        }
    }
}