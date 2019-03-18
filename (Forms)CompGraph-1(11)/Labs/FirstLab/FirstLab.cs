using System;
using System.Drawing;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
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
            var p2 = FindIntersection(first, c2);

            Drawer.DrawLine(Source, p1, p2, Color.Green);

            var second = GetTangent(c1, r1, c2, r2, -1);

            p1 = FindIntersection(second, c1);
            p2 = FindIntersection(second, c2);

            Drawer.DrawLine(Source, new Vector2D(p1, p2), Color.Red);
        }

        private void DrawTangentsExperiment(Point c1, int r1, Point c2, int r2)
        {
            var first = GetTangent(c1, r1, c2, r2);

            var p11 = FindIntersectionWithoutDividing(first, c1, r1);
            var p21 = FindIntersectionWithoutDividing(first, c2, r2);

            Drawer.DrawLine(Source, p11, p21, Color.Blue);
            
            var second = GetTangent(c1, r1, c2, r2, -1);

            p11 = FindIntersectionWithoutDividing(second, c1, r1);
            p21 = FindIntersectionWithoutDividing(second, c2, r2);

            Drawer.DrawLine(Source, new Vector2D(p11, p21), Color.Yellow);
        }

        private Point FindIntersectionWithoutDividing(Line3Points tangent, Point center, int r)
        {
            var vector = new Vector2(tangent.Ay, -tangent.Bx);

            var v2 = new Vector2(center.X - 0, center.Y - tangent.C);

            var skal = Vector2.Dot(vector, v2);

            vector = Vector2.Normalize(vector) * r;

            if (skal > 0)
            {
                vector.X -= center.X;
                vector.Y -= center.Y;
            }
            else
            {
                vector.X += center.X;
                vector.Y += center.Y;
            }

            return new Point((int) vector.X, (int) vector.Y);
        }

        private Point FindIntersection(Line3Points tangent, Point center)
        {
            var Np = new Line3Points(tangent.Ay, -tangent.Bx, 0);

            var Fp = new Line3Points(Np.Ay * Np.Bx, -(Np.Ay * center.Y + Np.Bx * center.X), 0);

            var x = (Pow(tangent.Bx) * center.X - tangent.Ay * (tangent.Bx * center.Y + tangent.C)) /
                    (Pow(tangent.Ay) + Pow(tangent.Bx));
            var point = GetY(tangent, (int) x);

            Console.WriteLine($"{Fp.Ay}a - {Fp.Bx} - {Fp.C}");
            Console.WriteLine($"{point}, center: {center}");
            return point;
        }

        private double Pow(double number, int pow = 2)
        {
            return Math.Pow(number, pow);
        }


        private Point GetY(Line3Points tangent, int x)
        {
            return tangent.Bx.Equals(0)
                ? new Point(x, (int) (tangent.Ay * x + tangent.C))
                : new Point(x, (int) -((tangent.Ay * x + tangent.C) / tangent.Bx));
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
            return new Line3Points((float) a, (float) b, (float) c);
        }
    }
}