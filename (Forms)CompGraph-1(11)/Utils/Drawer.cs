using System.Drawing;
using Point = System.Drawing.Point;

namespace _Forms_CompGraph_1_11_.Utils
{
    public static partial class Drawer
    {
        private static bool IsPointInBitmap(Bitmap source, in Point2D center, int radius = 0)
        {
            if (source.Width <= center.X + radius || center.X - radius < 0)
                return false;
            if (source.Height <= center.Y + radius || center.Y - radius < 0)
                return false;

            return true;
        }

        private static bool IsPointInBitmap(Bitmap source, int x, int y, int radius = 0)
        {
            if (source.Width <= x + radius || x - radius < 0)
                return false;
            if (source.Height <= y + radius || y - radius < 0)
                return false;

            return true;
        }


        private static bool IsPointInBitmap(Bitmap source, in Vector2D vector)
        {
            return IsPointInBitmap(source, vector.X0, vector.Y0) && IsPointInBitmap(source, vector.X1, vector.Y1);
        }

        private static Point2D GetSourceCoordinates(Bitmap source, in Point2D point)
        {
            return new Point2D(point.X + source.Width / 2, source.Height / 2 - point.Y);
        }

        private static PointF GetSourceCoordinates(Bitmap source, in PointF point) //На снос (или нет)
        {
            return new PointF(point.X + source.Width / 2, source.Height / 2 - point.Y);
        }

        private static Vector2D GetSourceCoordinates(Bitmap source, in Vector2D vector)
        {
            return new Vector2D(source.Width / 2 + vector.X0, source.Height / 2 - vector.Y0,
                source.Width / 2 + vector.X1, source.Height / 2 - vector.Y1);
        }

        private static Point2D GetSourceCoordinates(Bitmap source, in int x, in int y)
        {
            return new Point2D(x + source.Width / 2, source.Height / 2 - y);
        }

        /// <summary>
        /// Рисует чёрный пиксель на указанной точке
        /// </summary>
        /// <param name="source">aka Холст</param>
        /// <param name="dotPoint">Координаты точки, относительно центра</param>
        /// <returns>Удалось ли закрасить пиксель</returns>
        public static bool DrawDot(Bitmap source, in Point2D dotPoint)
        {
            return DrawDot(source, dotPoint, Color.Black);
        }

        /// <summary>
        /// Рисуут пиксель заданного цвета на указанной точке
        /// </summary>
        /// <param name="source">aka Холст</param>
        /// <param name="dotPoint">Координаты точки, относительно центра</param>
        /// <param name="color">Цвет точки</param>
        /// <returns></returns>
        public static bool DrawDot(Bitmap source, in Point2D dotPoint, Color color)
        {
            var point = GetSourceCoordinates(source, in dotPoint);

            if (!IsPointInBitmap(source, in point))
                return false;

            source.SetPixel(point.X, point.Y, color);
            return true;
        }


        public static bool DrawDot(Bitmap source, in int x, in int y, Color color)
        {
            var point = GetSourceCoordinates(source, x, y);

            if (!IsPointInBitmap(source, in point))
                return false;

            source.SetPixel(point.X, point.Y, color);
            return true;
        }

        public static bool DrawLine(Bitmap source, in Vector2D lineVector)
        {
            return DrawLine(source, lineVector, Color.Black);
        }

        public static bool DrawLine(Bitmap source, in Point2D first, in Point2D second)
        {
            return DrawLine(source, new Vector2D(first, second), Color.Black);
        }

        public static bool DrawLine(Bitmap source, in Point2D first, in Point2D second, Color color)
        {
            return DrawLine(source, new Vector2D(first, second), color);
        }

        public static bool DrawLine(Bitmap source, in Vector2D lineVector, in Color color)
        {
            var vector = GetSourceCoordinates(source, in lineVector);

            if (!IsPointInBitmap(source, in vector))
                return false;

            using (var graphics = Graphics.FromImage(source))
            {
                using (var pen = new Pen(color, 1))
                {
                    graphics.DrawLine(pen, vector.X0, vector.Y0, vector.X1, vector.Y1);
                }
            }

            return true;
        }

        public static bool DrawCircle(Bitmap source, Point center, int radius)
        {
            return DrawCircle(source, center, radius, Color.Black);
        }

        public static bool DrawCircle(Bitmap source, Point center, int radius, Color color)
        {
            var buffer = new Point2D(0, radius);
            var delta = 1 - 2 * radius;

            while (buffer.Y >= 0)
            {
                DrawDot(source, center.X + buffer.X, center.Y + buffer.Y, color);
                DrawDot(source, center.X + buffer.X, center.Y - buffer.Y, color);
                DrawDot(source, center.X - buffer.X, center.Y + buffer.Y, color);
                DrawDot(source, center.X - buffer.X, center.Y - buffer.Y, color);
                var error = 2 * (delta + buffer.Y) - 1;
                if ((delta < 0) && (error <= 0))
                {
                    delta += 2 * ++buffer.X + 1;
                    continue;
                }

                if (delta > 0 && error > 0)
                {
                    delta -= 2 * --buffer.Y + 1;
                    continue;
                }

                delta += 2 * (++buffer.X - buffer.Y--);
            }

            return true;
        }

        #region DrawCurve
        /// <summary>
        /// Рисует кривую заданного цвета по заданным точкам с заданной упругостью
        /// </summary>
        /// <param name="source"></param>
        /// <param name="points">Точки кривой</param>
        /// <param name="tension">Упругость кривой</param>
        /// <param name="color">Цвет кривой</param>
        /// <returns></returns>
        public static bool DrawCurve(Bitmap source, PointF[] points, float tension, Color color)
        {
            for (int i = 0; i < points.Length; i++)
                points[i] = GetSourceCoordinates(source, points[i]);

            using (var graphics = Graphics.FromImage(source))
            {
                using (var pen = new Pen(color, 1))
                {
                    graphics.DrawCurve(pen, points, tension); //Перепелить (оставленно для fast-теста)
                }
            }

            return true;
        }

        /// <summary>
        /// Рисует кривую черного цвета по заданным точкам с заданной упругостью
        /// </summary>
        /// <param name="source"></param>
        /// <param name="points">Точки кривой</param>
        /// <param name="tension">Упругость кривой</param>
        /// <returns></returns>
        public static bool DrawCurve(Bitmap source, PointF[] points, float tension)
        {
            return DrawCurve(source, points, tension, Color.Black);
        }

        /// <summary>
        /// Рисует кривую заданного цвета по заданным точкам
        /// </summary>
        /// <param name="source"></param>
        /// <param name="points">Точки кривой</param>
        /// <param name="color">Цвет кривой</param>
        /// <returns></returns>
        public static bool DrawCurve(Bitmap source, PointF[] points, Color color)
        {
            return DrawCurve(source, points, 0.1f, color);
        }

        /// <summary>
        /// Рисует кривую черного цвета по заданным точкам
        /// </summary>
        /// <param name="source"></param>
        /// <param name="points">Точки кривой</param>
        /// <returns></returns>
        public static bool DrawCurve(Bitmap source, PointF[] points)
        {
            return DrawCurve(source, points, 0.1f, Color.Black);
        }
        #endregion
    }
}