using System;
using System.Drawing;
using System.Collections.Generic;
using _Forms_CompGraph_1_11_.Utils;

namespace _Forms_CompGraph_1_11_.Labs.FourthLab
{
    struct PointInfo
    {
        public DoublePoint2D Point;
        public sbyte Relation;

        public PointInfo(DoublePoint2D point, sbyte relation)
        {
            Point = point;
            Relation = relation;
        }

        public PointInfo(DoublePoint2D point)
        {
            Point = point;
            Relation = 0;
        }
    }

    public class FourthLab : LabBase
    {
        public FourthLab(Bitmap source) : base(source)
        {

        }

        public override void Draw(LabParameters labParameters)
        {
            if (!(labParameters is FourthLabParameters parameters))
                throw new ArgumentException($"{nameof(labParameters)} has wrong type: {labParameters.GetType().Name}");

            DrawLab(parameters.WindowPoints, parameters.FigurePoints);
        }

        private void DrawLab(DoublePoint2D[] windowPoints, DoublePoint2D[] figurePoints)
        {
            List<PointInfo> realPoints = new List<PointInfo>(figurePoints.Length);

            //Measure step
            for (var fp = 0; fp < figurePoints.Length; fp++)
            {
                realPoints.Add(new PointInfo(figurePoints[fp]));

                for (var wp = 0; wp < windowPoints.Length; wp++)
                {
                    var wp1 = (wp + 1) % windowPoints.Length;
                    var fp1 = (fp + 1) % figurePoints.Length;
                    if (IsCrossing(windowPoints[wp], windowPoints[wp1], figurePoints[fp], figurePoints[fp1]))
                        realPoints.Add(CrossingPoint(windowPoints[wp], windowPoints[wp1], figurePoints[fp], figurePoints[fp1]));
                }
            }
            //realPoints.Add(new PointInfo(figurePoints[0]));

            //Draw step
            //Draw window
            for (var wp = 0; wp < windowPoints.Length; wp++)
                Drawer.DrawLine(Source, windowPoints[wp], windowPoints[(wp + 1) % windowPoints.Length], Color.Black);

            //Draw figure
            var rpOrigin = realPoints.IndexOf(realPoints.Find(x => x.Relation == 1));
            var rp = rpOrigin;
            var relation = 0;
            do
            {
                relation = (realPoints[rp].Relation == -1) ? -1 : 0;
                Drawer.DrawLine(Source, realPoints[rp].Point, realPoints[(rp + 1) % realPoints.Count].Point, (relation == 0) ? Color.Purple : Color.Aqua);

                rp = ++rp % realPoints.Count;
            } while (rp != rpOrigin);
        }

        /// <summary>
        /// Возвращает точку пересечения двух отрезков
        /// </summary>
        /// <param name="a">Начало отрезка окна</param>
        /// <param name="b">Конец отрезка окна</param>
        /// <param name="c">Начало отрезка фигуры</param>
        /// <param name="d">Конец отрезка фигуры</param>
        /// <returns></returns>
        private PointInfo CrossingPoint(DoublePoint2D a, DoublePoint2D b, DoublePoint2D c, DoublePoint2D d)
        {
            DoublePoint2D crossing = new DoublePoint2D();
            var z1 = VectorMult(d - c, a - c);
            var z2 = VectorMult(d - c, b - c);
            sbyte relation;

            crossing.X = a.X + (b - a).X * Math.Abs(z1) / Math.Abs(z2 - z1);
            crossing.Y = a.Y + (b - a).Y * Math.Abs(z1) / Math.Abs(z2 - z1);

            //Определяем, входящая или выходящая точка
            relation = (VectorMult(b - a, a - c) > 0) ? (sbyte)1 : (sbyte)-1;

            return new PointInfo(crossing, relation);
        }

        /// <summary>
        /// Возвращает значение, определяющее пересекаются ли 2 отрезка
        /// </summary>
        /// <param name="a">Начало 1 отрезка</param>
        /// <param name="b">Конец 1 отрезка</param>
        /// <param name="c">Начало 2 отрезка</param>
        /// <param name="d">Конец 2 отрезка</param>
        /// <returns>Пересекаются ли 2 отрезка?</returns>
        private bool IsCrossing(DoublePoint2D a, DoublePoint2D b, DoublePoint2D c, DoublePoint2D d)
        {
            var z1 = VectorMult(b - a, c - a);
            var z2 = VectorMult(b - a, d - a);

            if ((z1 == 0) || (z2 == 0) || (Math.Sign(z1) == Math.Sign(z2)))
                return false;

            z1 = VectorMult(d - c, a - c);
            z2 = VectorMult(d - c, b - c);

            if ((z1 == 0) || (z2 == 0) || (Math.Sign(z1) == Math.Sign(z2)))
                return false;
            return true;
        }

        /// <summary>
        /// Возвращает векторное произведение
        /// </summary>
        /// <param name="a">1-ый вектор</param>
        /// <param name="b">2-ой вектор</param>
        /// <returns>Векторное произведение</returns>
        private int VectorMult(DoublePoint2D a, DoublePoint2D b)
        {
            return (int)(a.X * b.Y - a.Y * b.X);
        }
    }
}