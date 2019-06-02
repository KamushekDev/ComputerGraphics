namespace _Forms_CompGraph_1_11_.Labs.SixthLab.Utils
{
    abstract class LightSource : IAddable
    {
        public float Intense { get; set; }

        protected LightSource()
        {
            
        }

        protected LightSource(float intense)
        {
            Intense = intense;
        }

        public override string ToString()
        {
            return $"[{nameof(Intense)}: {Intense}]";
        }
    }
}