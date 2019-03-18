using System;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace _Forms_CompGraph_1_11_
{
    public partial class Form1 : Form
    {
        private Bitmap _image;
        private readonly Point _center;
        private FirstLabEntryData _firstLabEntryData;

        public Form1()
        {
            InitializeComponent();

            #region TextboxFill

            tbFirstX.Text = "0";
            tbFirstY.Text = "0";
            tbFirstRadius.Text = "30";

            tbSecondX.Text = "150";
            tbSecondY.Text = "0";
            tbSecondRadius.Text = "70";

            tbAdditional.Text = "0";

            #endregion

            _image = new Bitmap(pbScene.Size.Width, pbScene.Size.Height);
            _center = new Point(pbScene.Size.Width / 2, pbScene.Size.Height / 2);
            lblWidth.Text = $"X: {-_center.X}  -  {_center.X}";
            lblHeight.Text = $"Y: {-_center.Y}  -  {_center.Y}";
            ClearImage();
            UpdateImage();
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            SetDefaultsForFirstLab();
            FirstLab();
        }

        private void SetDefaultsForFirstLab()
        {
            _firstLabEntryData = new FirstLabEntryData()
            {
                FirstCenter = new Point(int.Parse(tbFirstX.Text), int.Parse(tbFirstY.Text)),
                FirstRadius = int.Parse(tbFirstRadius.Text),
                SecondCenter = new Point(int.Parse(tbSecondX.Text), int.Parse(tbSecondY.Text)),
                SecondRadius = int.Parse(tbSecondRadius.Text)
            };
        }

        #region DrawSequence

        #region DrawVertex

        /// <summary>
        /// Рисует точку заданного цвета
        /// </summary>
        /// <param name="point">Координаты точки</param>
        /// <param name="color">Цвет точки</param>
        private void DrawVertex(Point point, Color color)
        {
            point = ToCenterCoordinates(point);

            _image.SetPixel(point.X, point.Y, color); //Отрисовка точки
            //UpdateImage();
        }

        /// <summary>
        /// Рисует черную точку
        /// </summary>
        /// <param name="point">Координаты точки</param>
        private void DrawVertex(Point point)
        {
            DrawVertex(point, Color.Black);
        }

        /// <summary>
        /// Рисует точку заданного цвета
        /// </summary>
        /// <param name="pointX">Абсцисса точки</param>
        /// <param name="pointY">Ордината точки</param>
        /// <param name="color">Цвет точки</param>
        private void DrawVertex(int pointX, int pointY, Color color)
        {
            DrawVertex(new Point(pointX, pointY), color);
        }

        /// <summary>
        /// Рисует черную точку
        /// </summary>
        /// <param name="pointX">Абсцисса точки</param>
        /// <param name="pointY">Ордината точки</param>
        private void DrawVertex(int pointX, int pointY)
        {
            DrawVertex(new Point(pointX, pointY), Color.Black);
        }

        #endregion

        #region DrawLine

        /// <summary>
        /// Рисует линию заданного цвета
        /// </summary>
        /// <param name="firstPoint">Начальная точка линии</param>
        /// <param name="secondPoint">Конечная точка линии</param>
        /// <param name="color">Цвет линии</param>
        private void DrawLine(Point firstPoint, Point secondPoint, Color color)
        {
            //Нормирование координат
            firstPoint = ToCenterCoordinates(firstPoint);
            secondPoint = ToCenterCoordinates(secondPoint);

            using (var graphics = Graphics.FromImage(_image))
            {
                using (var pen = new Pen(color, 1))
                {
                    graphics.DrawLine(pen, firstPoint.X, firstPoint.Y, secondPoint.X, secondPoint.Y); //Отрисовка линии
                }
            }

            //UpdateImage();
        }

        /// <summary>
        /// Рисует линию
        /// </summary>
        /// <param name="firstPoint">Начальная точка линии</param>
        /// <param name="secondPoint">Конечная точка линии</param>
        private void DrawLine(Point firstPoint, Point secondPoint)
        {
            DrawLine(firstPoint, secondPoint, Color.Black);
        }

        /// <summary>
        /// Рисует линию заданного цвета
        /// </summary>
        /// <param name="firstX">Абсцисса начальной точки линии</param>
        /// <param name="firstY">Ордината начальной точки линии</param>
        /// <param name="secondX">Абсцисса конечной точки линии</param>
        /// <param name="secondY">Ордината конечной точки линии</param>
        /// <param name="color">Цвет линии</param>
        private void DrawLine(int firstX, int firstY, int secondX, int secondY, Color color)
        {
            DrawLine(new Point(firstX, firstY), new Point(secondX, secondY), color);
        }

        /// <summary>
        /// Рисует линию
        /// </summary>
        /// <param name="firstX">Абсцисса начальной точки линии</param>
        /// <param name="firstY">Ордината начальной точки линии</param>
        /// <param name="secondX">Абсцисса конечной точки линии</param>
        /// <param name="secondY">Ордината конечной точки линии</param>
        private void DrawLine(int firstX, int firstY, int secondX, int secondY)
        {
            DrawLine(new Point(firstX, firstY), new Point(secondX, secondY), Color.Black);
        }

        #endregion

        #region DrawCircle

        /// <summary>
        /// Рисует окружность заданного цвета
        /// </summary>
        /// <param name="center">Координаты центра окружности</param>
        /// <param name="radius">Радиус окружности</param>
        /// <param name="color">Цвет окружности</param>
        private void DrawCircle(Point center, int radius, Color color)
        {
            var buffer = new Point(0, radius);
            var delta = 1 - 2 * radius;

            while (buffer.Y >= 0)
            {
                DrawVertex(center.X + buffer.X, center.Y + buffer.Y);
                DrawVertex(center.X + buffer.X, center.Y - buffer.Y);
                DrawVertex(center.X - buffer.X, center.Y + buffer.Y);
                DrawVertex(center.X - buffer.X, center.Y - buffer.Y);
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

            //UpdateImage();
        }

        /// <summary>
        /// Рисует окружность черного цвета
        /// </summary>
        /// <param name="center">Координаты центра окружности</param>
        /// <param name="radius">Радиус окружности</param>
        private void DrawCircle(Point center, int radius)
        {
            DrawCircle(center, radius, Color.Black);
        }

        /// <summary>
        /// Рисует окружность заданного цвета
        /// </summary>
        /// <param name="centerX">Абсцисса центра окружности</param>
        /// <param name="centerY">Ордината центра окружности</param>
        /// <param name="radius">Радиус окружности</param>
        /// <param name="color">Цвет окружности</param>
        private void DrawCircle(int centerX, int centerY, int radius, Color color)
        {
            DrawCircle(new Point(centerX, centerY), radius, color);
        }

        /// <summary>
        /// Рисует окружность черного цвета
        /// </summary>
        /// <param name="centerX">Абсцисса центра окружности</param>
        /// <param name="centerY">Ордината центра окружности</param>
        /// <param name="radius">Радиус окружности</param>
        private void DrawCircle(int centerX, int centerY, int radius)
        {
            DrawCircle(new Point(centerX, centerY), radius, Color.Black);
        }

        #endregion

        #endregion

        /// <summary>
        /// Рендер изображения
        /// </summary>
        private void UpdateImage()
        {
            pbScene.Image = _image;
        }

        /// <summary>
        /// Очистка буффера
        /// </summary>
        private void ClearImage()
        {
            _image = new Bitmap(pbScene.Size.Width, pbScene.Size.Height);
            DrawLine(0, -_image.Size.Height / 2, 0, _image.Size.Height / 2, Color.Gray);
            DrawLine(-_image.Size.Width / 2, 0, _image.Size.Width / 2, 0, Color.Gray);
        }

        /// <summary>
        /// Иницализирует первую лабу
        /// </summary>
        private void FirstLab()
        {
            ClearImage();

            DrawCircle(_firstLabEntryData.FirstCenter, _firstLabEntryData.FirstRadius);
            DrawCircle(_firstLabEntryData.SecondCenter, _firstLabEntryData.SecondRadius);
            DrawTangents(_firstLabEntryData.FirstCenter, _firstLabEntryData.FirstRadius, _firstLabEntryData.SecondCenter, _firstLabEntryData.SecondRadius);

            UpdateImage();
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

        private void DrawTangents(Point c1, int r1, Point c2, int r2)
        {
            var first = GetTangent(c1, r1, c2, r2);

            var p1 = FindIntersection(first, c1);
            if (first.B.Equals(0))
                DrawLine(p1.X, c1.Y, p1.X, c2.Y, Color.Green);
            else
            {
                var p2 = FindIntersection(first, c2);
                DrawLine(p1, p2, Color.Green);
            }

            var second = GetTangent(c1, r1, c2, r2, -1);

            p1 = FindIntersection(second, c1);
            if (second.B.Equals(0))
                DrawLine(p1.X, c1.Y, p1.X, c2.Y, Color.Red);
            else
            {
                var p2 = FindIntersection(second, c2);
                DrawLine(p1, p2, Color.Red);
            }
        }

        private double Pow(double number, int pow = 2)
        {
            return Math.Pow(number, pow);
        }

        private Point FindIntersection(Line3Points tangent, Point center)
        {
            var Np = new Line3Points(tangent.A, -tangent.B, 0);

            var Fp = new Line3Points(Np.A * Np.B, -(Np.A*center.Y + Np.B*center.X),0);

            var x = (Pow(tangent.B) * center.X - tangent.A * (tangent.B * center.Y + tangent.C)) /
                    (Pow(tangent.A) + Pow(tangent.B));
            var point = GetY(tangent, (int) x);

            Console.WriteLine($"{Fp.A}a - {Fp.B} - {Fp.C}");
            Console.WriteLine($"{point}, center: {center}");
            return point;
        }

        private Point GetY(Line3Points tangent, int x)
        {
            return tangent.B.Equals(0)
                ? new Point(x, (int) (tangent.A * x + tangent.C))
                : new Point(x, (int) -((tangent.A * x + tangent.C) / tangent.B));
        }

        struct Line3Points
        {
            public double A { get; }
            public double B { get; }
            public double C { get; }

            public Line3Points(double a, double b, double c)
            {
                A = a;
                B = b;
                C = c;
            }
        }

        class FirstLabEntryData
        {
            public Point FirstCenter { get; set; }
            public int FirstRadius { get; set; }
            public Point SecondCenter { get; set; }
            public int SecondRadius { get; set; }
        }

        private void pbScene_MouseClick(object sender, MouseEventArgs e)
        {
            if (_firstLabEntryData == null)
                SetDefaultsForFirstLab();



            var newCenter = e.Location;

            newCenter.X = newCenter.X - _center.X;
            newCenter.Y = _center.Y - newCenter.Y;

            if (e.Button == MouseButtons.Left)
            {
                tbFirstX.Text = newCenter.X.ToString();
                tbFirstY.Text = newCenter.Y.ToString();
                _firstLabEntryData.FirstCenter = newCenter;
            }
            else if (e.Button == MouseButtons.Right)
            {
                tbSecondX.Text = newCenter.X.ToString();
                tbSecondY.Text = newCenter.Y.ToString();
                _firstLabEntryData.SecondCenter = newCenter;
            }

            FirstLab();
        }

        /// <summary>
        /// Нормирование координат относительно центра
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        private Point ToCenterCoordinates(Point point)
        {
            return new Point(point.X+_center.X, _center.Y-point.Y);
        }

        private void pbScene_MouseMove(object sender, MouseEventArgs e)
        {
            var newCenter = e.Location;
            newCenter.X = newCenter.X - _center.X;
            newCenter.Y = _center.Y - newCenter.Y;
            toolTip.SetToolTip(pbScene, newCenter.ToString());
        }
    }
}