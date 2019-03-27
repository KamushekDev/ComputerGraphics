using System;
using System.Drawing;
using _Forms_CompGraph_1_11_.Utils;

namespace _Forms_CompGraph_1_11_.Labs.SecondLab
{
    public class SecondLab : LabBase
    {
        public SecondLab(Bitmap source) : base(source)
        {

        }

        public override void Draw(LabParameters labParameters)
        {
            if (!(labParameters is SecondLabParameters parameters))
                throw new ArgumentException($"{nameof(labParameters)} has wrong type: {labParameters.GetType().Name}");

            for (int i = 0; i < parameters.SplainPoints.Length; i++)
                //Drawer.DrawDot(Source, parameters.SplainPoints[i], Color.DarkRed);
                Drawer.DrawCircle(Source, new Point(parameters.SplainPoints[i].X, parameters.SplainPoints[i].Y), 3, Color.DarkRed);
            //Draw_spline(parameters.SplainPoints);
            //DetailedRecreatedBSplainOnSecretDocuments(parameters.SplainPoints, parameters.SplainPow);
            calculatePath(parameters.SplainPoints, parameters.SplainPow);
        }

        #region CopyFromBugor
        private double basisFunc(double[] knots, int order, int degree, double t)
        {
            if (degree == 0)
            {
                if (knots[order] <= t && t < knots[order + 1])
                    return 1;
                return 0;
            }
            double a = (knots[order + degree] - knots[order]) == 0 ? 0 : (t - knots[order]) / (knots[order + degree] - knots[order]);
            double b = (knots[order + degree + 1] - knots[order + 1]) == 0 ? 0 : (knots[order + degree + 1] - t) / (knots[order + degree + 1] - knots[order + 1]);

            return a * basisFunc(knots, order, degree - 1, t) + b * basisFunc(knots, order + 1, degree - 1, t);
        }

        private DoublePoint2D DeBoor(double t, Point2D[] splainControlPoints, double[] knots, int splainDegree)
        {
            DoublePoint2D V = new DoublePoint2D();
            for (int i = 0; i < splainControlPoints.Length; i++)
            {
                double scale = basisFunc(knots, i, splainDegree, t);
                V.X += splainControlPoints[i].X * scale;
                V.Y += splainControlPoints[i].Y * scale;
            }
            return V;
        }

        public void calculatePath(Point2D[] splainControlPoints, int splainDegree)
        {
            double[] knots = new double[splainDegree + splainControlPoints.Length + 1];
            double d = 1.0 / (1 + knots.Length - 2 * splainDegree);
            /*int i;
            for (i = splainDegree; i < knots.Length - splainDegree; i++)
                knots[i] = (i + 1 - splainDegree) * d;
            for (; i < knots.Length; i++)
                knots[i] = 1;*/

            for (int i = 0; i < splainDegree; i++)
            {
                knots[i] = 0;
                knots[knots.Length - i - 1] = 1;
            }
            knots[knots.Length - splainDegree - 1] = 1;
            for (int i = 1; i < (knots.Length - 2 * splainDegree-1); i++)
                knots[i + splainDegree] = (float)(i + 1) / (float)(knots.Length - 2 * splainDegree + 1);

            DoublePoint2D v = new DoublePoint2D();
            double delta = 1 / 100.0;
            for (double t = 0; t < 1; t += delta)
            {
                DoublePoint2D p = DeBoor(t, splainControlPoints, knots, splainDegree);
                //if (v != null) { g.drawLine((int)v[0], (int)v[1], (int)p[0], (int)p[1]); }
                if ((v.X != 0) && (v.Y != 0) && (p.X != 0) && (p.Y != 0))
                    Drawer.DrawLine(Source, new Vector2D((int)v.X, (int)v.Y, (int)p.X, (int)p.Y));

                v = p;
            }
        }
        #endregion

        #region Garbage
        /*private float WikiBoor(int fs,float pos, float[] knots, Point2D[] splainControlPoints, int splainDegree)
        {
            float[] d = new float[splainDegree + 1];
            for (int i = 0; i < d.Length; i++)
                d[i] = splainControlPoints[i + fs - splainDegree].X;
            for(int i=1;i<splainDegree+1;i++)
                for (int j=splainDegree;j<)
        }*/

        private void IDolbalEtuFigannuyuGrafiku(Point2D[] splainControlPoints, int splainPow)
        {
            //float t = time;// * (splainControlPoints.Length - 1); //time ranges from 0 to 1
            float[] knots = new float[] { 0, 1 / 6, 2 / 6, 3 / 6, 4 / 6, 5 / 6, 1 };
            //float db = deBoor(0, splainPow, t, knots);
        }

        private float deBoor(int i, int k, float t, float[] knots)
        {
            //i - knot span index
            //k - degree
            // t - time [0-knots.Length-1]
            //knots - the knots array
            if (k == 0)
            {
                if (knots[i] <= t && t < knots[i + 1])
                    return 1;
                else
                    return 0;
            }

            return ((t - knots[i]) / (knots[i + k] - knots[i])) * deBoor(i, k - 1, t, knots) + ((knots[i + k + 1] - t) / (knots[i + k + 1] - knots[i + 1])) * deBoor(i + 1, k - 1, t, knots);
        }

        private void Draw_spline(Point2D[] splainControlPoints)
        {
            //Для 
            Point2D[] splainPoints = new Point2D[splainControlPoints.Length + 2];
            for (int i = 0; i < splainControlPoints.Length; i++)
                splainPoints[i + 1] = splainControlPoints[i];
            splainPoints[0] = splainPoints[1];
            splainPoints[splainPoints.Length - 1] = splainPoints[splainPoints.Length - 2];

            for (int i = 1; i < splainControlPoints.Length; i++)// в цикле по всем четвёркам точек
            {
                Coeficient coeficient = new Coeficient(new float[4], new float[4]);
                coeficient.Measure(splainPoints, i);// считаем коэффициенты
                PointF[] points = new PointF[100];// создаём массив промежуточных точек
                for (int j = 0; j < 100; j++)
                {
                    float t = (float)((float)j / 100);// шаг интерполяции
                    // передаём массиву точек значения по методу beta-spline
                    points[j].X = (coeficient.A[0] + t * (coeficient.A[1] + t * (coeficient.A[2] + t * coeficient.A[3])));
                    points[j].Y = (coeficient.B[0] + t * (coeficient.B[1] + t * (coeficient.B[2] + t * coeficient.B[3])));
                }

                Drawer.DrawCurve(Source, points);
            }
        }

        private void DetailedRecreatedBSplainOnSecretDocuments(Point2D[] splainControlPoints, int splainPow)
        {
            //Заполняем массив угловых точек (равномерно от 0 до 1)
            float[] U = new float[splainControlPoints.Length + splainPow + 2];
            /*for (int i = 0; i < U.Length - 1; i++)
                U[i] = (float)i / (float)(U.Length - 1);
            U[U.Length - 1] = 1; //Чтоб последний элемент всегда = 1*/
            for (int i = 0; i < splainPow; i++)
            {
                U[i] = 0;
                U[U.Length - 1 - i] = 1;
            }
            for (int i = 0; i < (U.Length - 2 * splainPow); i++)
                U[i + splainPow] = (float)(i + 1) / (float)(U.Length - 2 * splainPow + 1);

            //Высчитываем коэффициенты при контрольных точках
            //double[] N = new double[splainControlPoints.Length * splainPow + splainPow];
            //for (int i = 0; i < N.Length; i++) //Будет рекурсия, чтоб не высчитывать повторно запиливаем нерасчитанные в минус (формулы без рекурсии мне влом выводить (это ОЧЕНЬ времязатратно))
            //    N[i] = -1;

            float d = 1000f;
            DoublePoint2D[] C = new DoublePoint2D[(int)d];

            for (int i = 0; i < C.Length; i++)
            {

                C[i] = splainControlPoints[0] * N(0, splainPow, i / d, U) + splainControlPoints[1] * N(1, splainPow, i / d, U)
                    + splainControlPoints[1] * N(2, splainPow, i / d, U) + splainControlPoints[1] * N(3, splainPow, i / d, U)
                    + splainControlPoints[1] * N(4, splainPow, i / d, U) + splainControlPoints[1] * N(5, splainPow, i / d, U)
                    + splainControlPoints[1] * N(6, splainPow, i / d, U) + splainControlPoints[1] * N(7, splainPow, i / d, U);
            }
            PointF[] points = new PointF[C.Length];
            for (int i = 0; i < points.Length; i++)
                points[i] = C[i].ToPointF();

            Drawer.DrawCurve(Source, points);
        }

        private float N(int order, int pow, float u, float[] U)
        {
            if (pow == 0)
                return ((U[order] <= u) && (u <= U[order + 1])) ? 1 : 0;

            //float n1 = (u - U[order]) / (U[order + pow] - U[order]) * N(order, pow - 1, u, U);
            //float n2 = (U[order + pow + 1] - u) / (U[order + pow + 1] - U[order + 1]) * N(order + 1, pow - 1, u, U);
            float n1 = (u - U[order]) / (U[order + pow - 1] - U[order]) * N(order, pow - 1, u, U);
            if (float.IsNaN(n1) || float.IsInfinity(n1))
                n1 = 0;
            float n2 = (U[order + pow] - u) / (U[order + pow] - U[order + 1]) * N(order + 1, pow - 1, u, U);
            if (float.IsNaN(n2) || float.IsInfinity(n2))
                n2 = 0;

            return n1 + n2;
        }

        /// <summary>
        /// Хранит и вычисляет коэффициенты сплайна
        /// </summary>
        private struct Coeficient
        {
            public float[] A;
            public float[] B;

            public Coeficient(float[] a, float[] b)
            {
                A = a;
                B = b;
            }

            /// <summary>
            /// Вычисление коэффициентов для определенной точки сплайна
            /// </summary>
            /// <param name="splainPoints">Контрольные точки сплайна</param>
            /// <param name="i">Индекс обрабатываемой точки сплайна</param>
            public void Measure(Point2D[] splainPoints, int i)
            {
                A[3] = (-splainPoints[i - 1].X + 3 * splainPoints[i].X - 3 * splainPoints[i + 1].X + splainPoints[i + 2].X) / 6;
                A[2] = (splainPoints[i - 1].X - 2 * splainPoints[i].X + splainPoints[i + 1].X) / 2;
                A[1] = (-splainPoints[i - 1].X + splainPoints[i + 1].X) / 2;
                A[0] = (splainPoints[i - 1].X + 4 * splainPoints[i].X + splainPoints[i + 1].X) / 6;
                B[3] = (-splainPoints[i - 1].Y + 3 * splainPoints[i].Y - 3 * splainPoints[i + 1].Y + splainPoints[i + 2].Y) / 6;
                B[2] = (splainPoints[i - 1].Y - 2 * splainPoints[i].Y + splainPoints[i + 1].Y) / 2;
                B[1] = (-splainPoints[i - 1].Y + splainPoints[i + 1].Y) / 2;
                B[0] = (splainPoints[i - 1].Y + 4 * splainPoints[i].Y + splainPoints[i + 1].Y) / 6;
            }
        }
        #endregion
    }
}