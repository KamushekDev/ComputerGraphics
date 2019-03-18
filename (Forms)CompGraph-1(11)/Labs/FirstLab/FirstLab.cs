using System;
using System.Drawing;
using _Forms_CompGraph_1_11_.Utils;

namespace _Forms_CompGraph_1_11_.Labs.FirstLab
{
    public class FirstLab : LabBase
    {
        public FirstLab(Bitmap source) : base(source)
        {
        }

        public override void Draw(LabParameters labParameters)
        {
            if (!(labParameters is FirstLabParameters parameters))
                throw new ArgumentException($"{nameof(labParameters)} has wrong type: {labParameters.GetType().Name}");

            Drawer.DrawCircle(Source, parameters.FirstCenter, parameters.FirstRadius);
            Drawer.DrawCircle(Source, parameters.SecondCenter, parameters.SecondRadius);

            DrawTangents(parameters.FirstCenter, parameters.FirstRadius,
                parameters.SecondCenter, parameters.SecondRadius);
        }

        private void DrawTangents(Point c1, int r1, Point c2, int r2)
        {
            var first = GetTangent(c1, r1, c2, r2);

            var p1 = FindIntersection(first, c1);
            if (first.B.Equals(0))
                Drawer.DrawLine(Source, new Vector2D(p1.X, c1.Y, p1.X, c2.Y), Color.Green);
            else
            {
                var p2 = FindIntersection(first, c2);
                Drawer.DrawLine(Source, p1, p2, Color.Green);
            }

            var second = GetTangent(c1, r1, c2, r2, -1);

            p1 = FindIntersection(second, c1);
            if (second.B.Equals(0))
                Drawer.DrawLine(Source, new Vector2D(p1.X, c1.Y, p1.X, c2.Y), Color.Red);
            else
            {
                var p2 = FindIntersection(second, c2);
                Drawer.DrawLine(Source, new Vector2D(p1, p2), Color.Red);
            }
        }

        private Point FindIntersection(Line3Points tangent, Point center)
        {
            var Np = new Line3Points(tangent.A, -tangent.B, 0);

            var Fp = new Line3Points(Np.A * Np.B, -(Np.A * center.Y + Np.B * center.X), 0);

            var x = (Pow(tangent.B) * center.X - tangent.A * (tangent.B * center.Y + tangent.C)) /
                    (Pow(tangent.A) + Pow(tangent.B));
            var point = GetY(tangent, (int) x);

            Console.WriteLine($"{Fp.A}a - {Fp.B} - {Fp.C}");
            Console.WriteLine($"{point}, center: {center}");
            return point;
        }

        private double Pow(double number, int pow = 2)
        {
            return Math.Pow(number, pow);
        }


        private Point GetY(Line3Points tangent, int x)
        {
            return tangent.B.Equals(0)
                ? new Point(x, (int) (tangent.A * x + tangent.C))
                : new Point(x, (int) -((tangent.A * x + tangent.C) / tangent.B));
        }

        private Line3Points GetTangent(Point c1, int r1, Point c2, int r2, int k = 1)
        {
            int dx = c2.X - c1.X,
                dy = c2.Y - c1.Y,
                dr = r2 - r1;
            double d = Math.Sqrt(dx * dx + dy * dy),
                X = dx / d,
                Y = dy / d,
                R = dr / d,
                a = R * X - Y * k * Math.Sqrt(1 - R * R),
                b = R * Y + X * k * Math.Sqrt(1 - R * R),
                c = r1 - (a * c1.X + b * c1.Y);
            return new Line3Points(a, b, c);
        }
    }
}