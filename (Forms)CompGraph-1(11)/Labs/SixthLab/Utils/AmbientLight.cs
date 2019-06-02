namespace _Forms_CompGraph_1_11_.Labs.SixthLab.Utils
{
    class AmbientLight : LightSource
    {
        public AmbientLight()
        {
        }

        public AmbientLight(float intense) : base(intense)
        {
        }

        public override string ToString()
        {
            return $"(Ambient light) {base.ToString()}";
        }
    }
}