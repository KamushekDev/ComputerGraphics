using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
        private readonly HashSet<Control> _formatErrors;
        private int _currentLab = 1;

        public Form1()
        {
            InitializeComponent();

            _formatErrors = new HashSet<Control>();
            _image = new Bitmap(pbScene.Size.Width, pbScene.Size.Height);
            _center = new Point(pbScene.Size.Width / 2, pbScene.Size.Height / 2);

            SetDefaults();

            lblWidth.Text = $"Width: {-_center.X}  -  {_center.X}";
            lblHeight.Text = $"Height: {-_center.Y}  -  {_center.Y}";
            ClearImage();
            btnDraw_Click(null, null);
            UpdateImage();
        }


        #region Labs

        private void SetDefaults()
        {
            switch (_currentLab)
            {
                case 1:
                    SetDefaultsForFirstLab();
                    break;
                case 2:
                    SetDefaultsForSecondLab();
                    break;
                case 3:
                    throw new NotImplementedException();
                case 4:
                    throw new NotImplementedException();
                case 5:
                    throw new NotImplementedException();
                case 6:
                    throw new NotImplementedException();
                case 7:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentOutOfRangeException($"{nameof(_currentLab)} hasn't been in 1-7 interval");
            }
        }

        private void ParseParameters()
        {
            switch (_currentLab)
            {
                case 1:
                    _labParameters = ParseFirstLabParameters();
                    break;
                case 2:
                    _labParameters = ParseSecondLabParameters();
                    break;
                case 3:
                    throw new NotImplementedException();
                case 4:
                    throw new NotImplementedException();
                case 5:
                    throw new NotImplementedException();
                case 6:
                    throw new NotImplementedException();
                case 7:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentOutOfRangeException($"{nameof(_currentLab)} hasn't been in 1-7 interval");
            }
        }

        private void MouseClicked(Point position, MouseEventArgs e)
        {
            switch (_currentLab)
            {
                case 1:
                    UpdateFirstLabCoords(position, e);
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"{nameof(_currentLab)} hasn't been in 1-7 interval");
            }

            btnDraw_Click(null, null);
        }

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

        private void UpdateFirstLabCoords(Point center, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                tbFirstX.Text = center.X.ToString();
                tbFirstY.Text = center.Y.ToString();
            }
            else if (e.Button == MouseButtons.Right)
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
            var splinePoints = new List<Point2D>();

            var point = new Point2D(int.Parse(FirstPointXTextBox.Text),
                int.Parse(FirstPointYTextBox.Text));
            for (var i = 0; i < FirstPointCountNumericUpDown.Value; i++)
                splinePoints.Add(point);

            point = new Point2D(int.Parse(SecondPointXTextBox.Text),
                int.Parse(SecondPointYTextBox.Text));
            for (var i = 0; i < SecondPointCountNumericUpDown.Value; i++)
                splinePoints.Add(point);

            point = new Point2D(int.Parse(ThirdPointXTextBox.Text),
                int.Parse(ThirdPointYTextBox.Text));
            for (var i = 0; i < ThirdPointCountNumericUpDown.Value; i++)
                splinePoints.Add(point);

            point = new Point2D(int.Parse(FourthPointXTextBox.Text),
                int.Parse(FourthPointYTextBox.Text));
            for (var i = 0; i < FourthPointCountNumericUpDown.Value; i++)
                splinePoints.Add(point);

            point = new Point2D(int.Parse(FifthPointXTextBox.Text),
                int.Parse(FifthPointYTextBox.Text));
            for (var i = 0; i < FifthPointCountNumericUpDown.Value; i++)
                splinePoints.Add(point);

            point = new Point2D(int.Parse(SixthPointXTextBox.Text),
                int.Parse(SixthPointYTextBox.Text));
            for (var i = 0; i < SixthPointCountNumericUpDown.Value; i++)
                splinePoints.Add(point);

            point = new Point2D(int.Parse(SeventhPointXTextBox.Text),
                int.Parse(SeventhPointYTextBox.Text));
            for (var i = 0; i < SeventhPointCountNumericUpDown.Value; i++)
                splinePoints.Add(point);

            if (SplinePointsSortNeeded.Checked)
                splinePoints.Sort();

            var splinePow = (int) SplineDegreeNumericUpDown.Value;

            return new SecondLabParameters(splinePoints.ToArray(), splinePow);
        }

        #endregion

        #endregion

        #region Events

        private void btnDraw_Click(object sender, EventArgs e)
        {
            if (_formatErrors.Count > 0)
            {
                MessageBox.Show(
                    $"Исправьте ошибки в полях: {string.Join(", ", _formatErrors.Select(x => x.Name))}");
                return;
            }

            ParseParameters();
            Draw();
        }

        private void pbScene_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left && e.Button != MouseButtons.Right)
                return;

            var newCenter = e.Location;

            newCenter.X = newCenter.X - _center.X;
            newCenter.Y = _center.Y - newCenter.Y;
            MouseClicked(newCenter, e);
        }

        private void pbScene_MouseMove(object sender, MouseEventArgs e)
        {
            var newCenter = e.Location;
            newCenter.X = newCenter.X - _center.X;
            newCenter.Y = _center.Y - newCenter.Y;
            toolTip.SetToolTip(pbScene, newCenter.ToString());
        }

        private void NumericTextBox_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!(sender is TextBox textBox))
                throw new ArgumentException($"{nameof(sender)} has the wrong type.");

            var result = int.TryParse(textBox.Text, out _);

            if (result)
            {
                NumericTextBoxErrorProvider.SetError(textBox, null);
                _formatErrors.Remove(textBox);
            }
            else
            {
                NumericTextBoxErrorProvider.SetError(textBox, "Text should be a number!");
                _formatErrors.Add(textBox);
            }
        }

        private void TabControlLabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentLab = ((sender as TabControl)?.SelectedIndex ?? 0) + 1;
            SetDefaults();
            btnDraw_Click(null, null);
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