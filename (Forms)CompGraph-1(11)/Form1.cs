using System;
using System.Drawing;
using System.Windows.Forms;
using _Forms_CompGraph_1_11_.Labs;
using _Forms_CompGraph_1_11_.Labs.FirstLab;
using _Forms_CompGraph_1_11_.Labs.SecondLab;
using _Forms_CompGraph_1_11_.Utils;

namespace _Forms_CompGraph_1_11_
{
    public partial class Form1 : Form
    {
        private readonly Bitmap _image;
        private readonly Point _center;
        private LabBase _labBase;
        private LabParameters _labParameters;

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

            tbAdditional.Text = "2";

            #endregion

            _image = new Bitmap(pbScene.Size.Width, pbScene.Size.Height);
            _center = new Point(pbScene.Size.Width / 2, pbScene.Size.Height / 2);

            //SetDefaultsForFirstLab();
            SetDefaultsForSecondLab();

            lblWidth.Text = $"X: {-_center.X}  -  {_center.X}";
            lblHeight.Text = $"Y: {-_center.Y}  -  {_center.Y}";
            ClearImage();
            UpdateImage();
        }

        private void btnDraw_Click(object sender, EventArgs e)
        {
            //_labParameters = ParseFirstLabParameters();
            _labParameters = ParseSecondLabParameters();
            Draw();
        }

        #region Labs
        #region FirstLab
        private FirstLabParameters ParseFirstLabParameters()
        {
            return new FirstLabParameters(
                new Point(int.Parse(tbFirstX.Text), int.Parse(tbFirstY.Text)),
                int.Parse(tbFirstRadius.Text),
                new Point(int.Parse(tbSecondX.Text), int.Parse(tbSecondY.Text)),
                int.Parse(tbSecondRadius.Text)
            );
        }

        private void SetDefaultsForFirstLab()
        {
            _labBase = new FirstLab(_image);
            _labParameters = ParseFirstLabParameters();
        }

        private void UpdateFirstLabCoords(Point center, bool firstCircle)
        {
            if (firstCircle)
            {
                tbFirstX.Text = center.X.ToString();
                tbFirstY.Text = center.Y.ToString();
            }
            else
            {
                tbSecondX.Text = center.X.ToString();
                tbSecondY.Text = center.Y.ToString();
            }
        }
        #endregion

        #region SecondLab
        private void SetDefaultsForSecondLab()
        {
            _labBase = new SecondLab(_image);
            _labParameters = ParseSecondLabParameters();
        }

        private SecondLabParameters ParseSecondLabParameters()
        {
            Point2D[] splainPoints = new Point2D[7];
            /*splainPoints[0] = new Point2D(-30, 20);
            splainPoints[1] = new Point2D(-150, 80);
            splainPoints[2] = new Point2D(100, -10);
            splainPoints[3] = new Point2D(0, -200);
            splainPoints[4] = new Point2D(50, 100);
            splainPoints[5] = new Point2D(90, 150);
            splainPoints[6] = new Point2D(-175, -10);
            //splainPoints[6] = new Point2D(90, 150);
            //splainPoints[7] = new Point2D(90, 150);*/

            splainPoints[0] = new Point2D(-200, -200);
            splainPoints[1] = new Point2D(-150, 80);
            splainPoints[2] = new Point2D(0, -100);
            splainPoints[3] = new Point2D(75, 200);
            splainPoints[4] = new Point2D(125, 10);
            splainPoints[5] = new Point2D(180, 120);
            splainPoints[6] = new Point2D(200, -85);

            int splainPow = int.Parse(tbAdditional.Text);

            return new SecondLabParameters(splainPoints, splainPow);
        }

        #endregion
        #endregion

        #region Events

        private void pbScene_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right)
                return;

            var newCenter = e.Location;

            newCenter.X = newCenter.X - _center.X;
            newCenter.Y = _center.Y - newCenter.Y;

            UpdateFirstLabCoords(newCenter, e.Button == MouseButtons.Left);

            btnDraw_Click(null, null);
        }

        private void pbScene_MouseMove(object sender, MouseEventArgs e)
        {
            var newCenter = e.Location;
            newCenter.X = newCenter.X - _center.X;
            newCenter.Y = _center.Y - newCenter.Y;
            toolTip.SetToolTip(pbScene, newCenter.ToString());
        }

        #endregion

        #region Image

        private void Draw()
        {
            ClearImage();
            _labBase.Draw(_labParameters);
            UpdateImage();
        }

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
            using (var graphics = Graphics.FromImage(_image))
            {
                graphics.Clear(Color.White);
            }

            Drawer.DrawLine(_image, new Vector2D(0, -_image.Size.Height / 2, 0, _image.Size.Height / 2),
                Color.DarkGray);
            Drawer.DrawLine(_image, new Vector2D(-_image.Size.Width / 2, 0, _image.Size.Width / 2, 0), Color.DarkGray);

            UpdateImage();
        }

        #endregion
    }
}