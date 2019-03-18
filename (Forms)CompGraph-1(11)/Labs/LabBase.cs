using System.Drawing;

namespace _Forms_CompGraph_1_11_.Labs
{
    public abstract class LabBase
    {
        protected Bitmap Source;

        protected LabBase(Bitmap source)
        {
            Source = source;
        }

        public abstract void Draw(LabParameters parameters);

    }
}
