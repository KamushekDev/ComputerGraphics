namespace _Forms_CompGraph_1_11_.Utils
{
    internal struct ColorRGB
    {
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }

        public ColorRGB(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }

        public static ColorRGB operator +(ColorRGB a, ColorRGB b)
        {
            var r = a.R + b.R;
            if (r > 255)
                r = 255;

            var g = a.G + b.G;
            if (g > 255)
                g = 255;

            var bl = a.B + b.B;
            if (bl > 255)
                bl = 255;

            return new ColorRGB((byte)r, (byte)g, (byte)bl);
        }

        public static ColorRGB operator *(float a, ColorRGB b)
        {
            var r = a * b.R;
            if (r > 255)
                r = 255;

            var g = a * b.G;
            if (g > 255)
                g = 255;

            var bl = a * b.B;
            if (bl > 255)
                bl = 255;

            return new ColorRGB((byte)r, (byte)g, (byte)bl);
        }

        public override string ToString()
        {
            return $"[{R}, {G}, {B}]";
        }
    }
}
