using System;
using System.Drawing;
using System.Windows.Forms;
using _Forms_CompGraph_1_11_.Utils;

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
            Drawer.DrawLine(_image, new Vector2D(0, -_image.Size.Height / 2, 0, _image.Size.Height / 2),
                Color.DarkGray);
            Drawer.DrawLine(_image, new Vector2D(-_image.Size.Width / 2, 0, _image.Size.Width / 2, 0), Color.DarkGray);
        }

        /// <summary>
        /// Иницализирует первую лабу
        /// </summary>
        private void FirstLab()
        {
            ClearImage();

            Drawer.DrawCircle(_image, _firstLabEntryData.FirstCenter, _firstLabEntryData.FirstRadius);
            Drawer.DrawCircle(_image, _firstLabEntryData.SecondCenter, _firstLabEntryData.SecondRadius);
            DrawTangents(_firstLabEntryData.FirstCenter, _firstLabEntryData.FirstRadius,
                _firstLabEntryData.SecondCenter, _firstLabEntryData.SecondRadius);

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
                Drawer.DrawLine(_image, new Vector2D(p1.X, c1.Y, p1.X, c2.Y), Color.Green);
            else
            {
                var p2 = FindIntersection(first, c2);
                Drawer.DrawLine(_image, p1, p2, Color.Green);
            }

            var second = GetTangent(c1, r1, c2, r2, -1);

            p1 = FindIntersection(second, c1);
            if (second.B.Equals(0))
                Drawer.DrawLine(_image, new Vector2D(p1.X, c1.Y, p1.X, c2.Y), Color.Red);
            else
            {
                var p2 = FindIntersection(second, c2);
                Drawer.DrawLine(_image, new Vector2D(p1, p2), Color.Red);
            }
        }

        private double Pow(double number, int pow = 2)
        {
            return Math.Pow(number, pow);
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
            return new Point(point.X + _center.X, _center.Y - point.Y);
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