using _Forms_CompGraph_1_11_.Labs;
using _Forms_CompGraph_1_11_.Labs.FirstLab;
using _Forms_CompGraph_1_11_.Labs.FourthLab;
using _Forms_CompGraph_1_11_.Labs.SecondLab;
using _Forms_CompGraph_1_11_.Labs.SixthLab;
using _Forms_CompGraph_1_11_.Labs.SixthLab.Utils;
using _Forms_CompGraph_1_11_.Labs.ThirdLab;
using _Forms_CompGraph_1_11_.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DoublePoint3D = _Forms_CompGraph_1_11_.Utils.DoublePoint3D;

namespace _Forms_CompGraph_1_11_
{
    public partial class Form1 : Form
    {
        private readonly Bitmap _image;
        private readonly Point _center;
        private LabBase _labBase;
        private LabParameters _labParameters;
        private readonly HashSet<Control> _formatErrors;
        private int _currentLab = 6;

        readonly object[] objectTypes =
        {
            new ElementType {Type = typeof(AmbientLight)},
            new ElementType {Type = typeof(DirectLight)},
            new ElementType {Type = typeof(PointLight)},
            new ElementType {Type = typeof(Sphere)},
            new ElementType {Type = typeof(Hexahedron)}
        };

        class ElementType
        {
            public Type Type { get; set; }
            public override string ToString() => Type.Name;
        }

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
            //btnDraw_Click(null, null);
            UpdateImage();
            ObjectTypesCB.Items.AddRange(objectTypes);

            tabControlLabs.SelectedIndex = _currentLab - 1;
            CameraRotationXLbl.Text = CameraXAngleTB.Value.ToString();
            CameraRotationYLbl.Text = CameraYAngleTB.Value.ToString();
        }


        #region Labs

        private void SetDefaults()
        {
            if (_formatErrors.Count > 0)
            {
                MessageBox.Show(
                    $"Исправьте ошибки в полях: {string.Join(", ", _formatErrors.Select(x => x.Name))}");
                return;
            }

            switch (_currentLab)
            {
                case 1:
                    SetDefaultsForFirstLab();
                    break;
                case 2:
                    SetDefaultsForSecondLab();
                    break;
                case 3:
                    SetDefaultsForThirdLab();
                    break;
                case 4:
                    SetDefaultsForFourthLab();
                    break;
                case 5:

                    break;
                case 6:
                    SetDefaultsForSixthLab();
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"{nameof(_currentLab)} hasn't been in 1-5 interval");
            }
        }

        private void ParseParameters()
        {
            if (_formatErrors.Count > 0)
            {
                MessageBox.Show(
                    $"Исправьте ошибки в полях: {string.Join(", ", _formatErrors.Select(x => x.Name))}");
                return;
            }

            switch (_currentLab)
            {
                case 1:
                    _labParameters = ParseFirstLabParameters();
                    break;
                case 2:
                    _labParameters = ParseSecondLabParameters();
                    break;
                case 3:
                    _labParameters = ParseThirdLabParameters();
                    break;
                case 4:
                    _labParameters = ParseFourthLabParameters();
                    break;
                case 5:

                    break;
                case 6:
                    _labParameters = ParseSixthLabParameters();
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"{nameof(_currentLab)} hasn't been in 1-5 interval");
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
                default:
                    throw new ArgumentOutOfRangeException($"{nameof(_currentLab)} hasn't been in 1-5 interval");
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

        #region ThirdLab

        private void SetDefaultsForThirdLab()
        {
            FirstPresetThirdLab();
            _labBase = new ThirdLab(_image);
            _labParameters = ParseThirdLabParameters();
        }

        private ThirdLabParameters ParseThirdLabParameters()
        {
            var areaPointsXY = new List<DoublePoint2D>();
            var areaPointZ = new List<double>();
            var xRotateDegree = double.Parse(tbRotateX.Text);
            var yRotateDegree = double.Parse(tbRotateY.Text);

            for (var row = 0; row < dgvAreaPoints.Rows.Count - 1; row++)
            {
                try
                {
                    areaPointsXY.Add(new DoublePoint2D(double.Parse(dgvAreaPoints.Rows[row].Cells[0].Value.ToString()),
                        double.Parse(dgvAreaPoints.Rows[row].Cells[1].Value.ToString())));
                    areaPointZ.Add(double.Parse(dgvAreaPoints.Rows[row].Cells[2].Value.ToString()));
                }
                catch (NullReferenceException)
                {
                    dgvWindowPoints.Rows.RemoveAt(row);
                    row--;
                    continue;
                }
            }

            return new ThirdLabParameters(areaPointsXY.ToArray(), areaPointZ.ToArray(), xRotateDegree, yRotateDegree);
        }

        private void FirstPresetThirdLab()
        {
            dgvAreaPoints.Rows.Clear();
            dgvAreaPoints.Rows.Add(4);

            dgvAreaPoints.Rows[0].Cells[0].Value = 0;
            dgvAreaPoints.Rows[0].Cells[1].Value = 0;
            dgvAreaPoints.Rows[0].Cells[2].Value = 100;

            dgvAreaPoints.Rows[1].Cells[0].Value = 0;
            dgvAreaPoints.Rows[1].Cells[1].Value = 100;
            dgvAreaPoints.Rows[1].Cells[2].Value = 100;

            dgvAreaPoints.Rows[2].Cells[0].Value = 0;
            dgvAreaPoints.Rows[2].Cells[1].Value = 100;
            dgvAreaPoints.Rows[2].Cells[2].Value = 0;

            dgvAreaPoints.Rows[3].Cells[0].Value = 100;
            dgvAreaPoints.Rows[3].Cells[1].Value = 0;
            dgvAreaPoints.Rows[3].Cells[2].Value = 0;

            tbRotateX.Text = "0";
            tbRotateY.Text = "0";
        }

        private void SecondPresetThirdLab()
        {
            dgvAreaPoints.Rows.Clear();
            dgvAreaPoints.Rows.Add(6);

            dgvAreaPoints.Rows[0].Cells[0].Value = 0;
            dgvAreaPoints.Rows[0].Cells[1].Value = 0;
            dgvAreaPoints.Rows[0].Cells[2].Value = 100;

            dgvAreaPoints.Rows[1].Cells[0].Value = 0;
            dgvAreaPoints.Rows[1].Cells[1].Value = 100;
            dgvAreaPoints.Rows[1].Cells[2].Value = 100;

            dgvAreaPoints.Rows[2].Cells[0].Value = 100;
            dgvAreaPoints.Rows[2].Cells[1].Value = 100;
            dgvAreaPoints.Rows[2].Cells[2].Value = 100;

            dgvAreaPoints.Rows[3].Cells[0].Value = 100;
            dgvAreaPoints.Rows[3].Cells[1].Value = 100;
            dgvAreaPoints.Rows[3].Cells[2].Value = 0;

            dgvAreaPoints.Rows[4].Cells[0].Value = 100;
            dgvAreaPoints.Rows[4].Cells[1].Value = 0;
            dgvAreaPoints.Rows[4].Cells[2].Value = 0;

            dgvAreaPoints.Rows[5].Cells[0].Value = 100;
            dgvAreaPoints.Rows[5].Cells[1].Value = 0;
            dgvAreaPoints.Rows[5].Cells[2].Value = 100;

            tbRotateX.Text = "0";
            tbRotateY.Text = "0";
        }

        #endregion

        #region FourthLab

        private void SetDefaultsForFourthLab()
        {
            _labBase = new FourthLab(_image);
            _labParameters = ParseFourthLabParameters();
            FirstPresetFourthLab();
        }

        private FourthLabParameters ParseFourthLabParameters()
        {
            var windowPoints = new List<DoublePoint2D>();
            var figurePoints = new List<DoublePoint2D>();

            for (var row = 0; row < dgvWindowPoints.Rows.Count - 1; row++)
            {
                string x, y;
                try
                {
                    x = dgvWindowPoints.Rows[row].Cells[0].Value.ToString();
                    y = dgvWindowPoints.Rows[row].Cells[1].Value.ToString();
                }
                catch (NullReferenceException)
                {
                    dgvWindowPoints.Rows.RemoveAt(row);
                    row--;
                    continue;
                }

                windowPoints.Add(new DoublePoint2D(double.Parse(x), double.Parse(y)));
            }

            for (var row = 0; row < dgvFigurePoints.Rows.Count - 1; row++)
            {
                string x, y;
                try
                {
                    x = dgvFigurePoints.Rows[row].Cells[0].Value.ToString();
                    y = dgvFigurePoints.Rows[row].Cells[1].Value.ToString();
                }
                catch (NullReferenceException)
                {
                    dgvFigurePoints.Rows.RemoveAt(row);
                    row--;
                    continue;
                }

                figurePoints.Add(new DoublePoint2D(double.Parse(x), double.Parse(y) + 0.1));
            }

            return new FourthLabParameters(windowPoints.ToArray(), figurePoints.ToArray());
        }

        private void FirstPresetFourthLab()
        {
            dgvWindowPoints.Rows.Clear();
            dgvFigurePoints.Rows.Clear();

            dgvWindowPoints.Rows.Add(4);
            dgvFigurePoints.Rows.Add(5);

            //Window
            dgvWindowPoints.Rows[0].Cells[0].Value = 0;
            dgvWindowPoints.Rows[0].Cells[1].Value = 300;

            dgvWindowPoints.Rows[1].Cells[0].Value = 200;
            dgvWindowPoints.Rows[1].Cells[1].Value = -200;

            dgvWindowPoints.Rows[2].Cells[0].Value = 0;
            dgvWindowPoints.Rows[2].Cells[1].Value = 0;

            dgvWindowPoints.Rows[3].Cells[0].Value = -200;
            dgvWindowPoints.Rows[3].Cells[1].Value = -200;

            //Figure
            dgvFigurePoints.Rows[0].Cells[0].Value = -200;
            dgvFigurePoints.Rows[0].Cells[1].Value = 200;

            dgvFigurePoints.Rows[1].Cells[0].Value = 0;
            dgvFigurePoints.Rows[1].Cells[1].Value = 100;

            dgvFigurePoints.Rows[2].Cells[0].Value = 200;
            dgvFigurePoints.Rows[2].Cells[1].Value = -100;

            dgvFigurePoints.Rows[3].Cells[0].Value = -300;
            dgvFigurePoints.Rows[3].Cells[1].Value = -100;

            dgvFigurePoints.Rows[4].Cells[0].Value = -100;
            dgvFigurePoints.Rows[4].Cells[1].Value = 0;
        }

        #endregion

        #region SixthLab

        private SixthLabParameters ParseSixthLabParameters()
        {
            var parameters = new SixthLabParameters();
            var objects = new object[ElementsLB.Items.Count];
            ElementsLB.Items.CopyTo(objects, 0);
            parameters.GraphicalObjects = objects.Where(x => x is GraphicalObject).Cast<GraphicalObject>().ToArray();
            parameters.LightSources = objects.Where(x => x is LightSource).Cast<LightSource>().ToArray();
            parameters.CameraPosition = new DoublePoint3D
            {
                X = double.Parse(CameraX.Text),
                Y = double.Parse(CameraY.Text),
                Z = double.Parse(CameraZ.Text)
            };

            parameters.CameraRotation = new DoublePoint2D
            {
                X = CameraXAngleTB.Value * Math.PI / 180,
                Y = CameraYAngleTB.Value * Math.PI / 180
            };

            return parameters;
        }

        private void SetDefaultsForSixthLab()
        {
            _labBase = new SixthLab(_image);

            var elements = new IAddable[7];

            elements[0] = new AmbientLight(0.2f);

            elements[1] = new PointLight(
                new DoublePoint3D(2, 1, 0),
                0.6f
            );

            elements[2] = new DirectLight(
                new DoublePoint3D(1, 4, 4),
                0.2f
            );

            elements[3] = new Sphere(
                new DoublePoint3D(0, -1, 3),
                1,
                new ColorRGB(255, 0, 0),
                500,
                0.2f,
                0f);

            elements[4] = new Sphere(
                new DoublePoint3D(2, 0, 4),
                1,
                new ColorRGB(0, 0, 255),
                500,
                0.3f,
                0f);

            elements[5] = new Sphere(
                new DoublePoint3D(-2, 0, 4),
                1,
                new ColorRGB(0, 255, 0), 
                10,
                0.4f,
                0f);

            elements[6] = new Sphere(
                new DoublePoint3D(0, -5001, 0), 
                5000, 
                new ColorRGB(255, 255, 0), 
                1000,
                0.5f,
                0f);

            ElementsLB.Items.AddRange(elements);

            CameraX.Text = "0";
            CameraY.Text = "5";
            CameraZ.Text = "2";

            CameraXAngleTB.Value = 90;
            CameraYAngleTB.Value = 180;

            _labParameters = ParseSixthLabParameters();
        }

        #endregion

        #endregion

        #region Events

        private void btnDraw_Click(object sender, EventArgs e)
        {
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

        private void AddElementBtn_Click(object sender, EventArgs e)
        {
            AddElement();
        }

        private void ElementsLB_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (ElementsLB.SelectedIndex >= 0)
                ElementsLB.Items.RemoveAt(ElementsLB.SelectedIndex);
        }

        private void AddElement()
        {
            if (ObjectTypesCB.SelectedItem != null)
            {
                var elementType = ((ElementType) ObjectTypesCB.SelectedItem).Type;
                var emptyElement = (IAddable) Activator.CreateInstance(elementType);
                EditElement(emptyElement);
            }
        }

        private void EditElement(IAddable element)
        {
            var editWindow = new EditElementForm(element);


            if (editWindow.ShowDialog(this) == DialogResult.OK)
            {
                var newElement = editWindow.ReturnedElement;
                ElementsLB.Items.Remove(element);
                ElementsLB.Items.Add(newElement);
            }

            editWindow.Dispose();
        }

        private void ElementsLB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) Keys.Enter && ElementsLB.SelectedIndex >= 0)
                EditElement((IAddable) ElementsLB.SelectedItem);
        }

        private void CameraXAngleTB_ValueChanged(object sender, EventArgs e)
        {
            CameraRotationXLbl.Text = (sender as TrackBar)?.Value.ToString() ?? "0";
        }

        private void CameraYAngleTB_ValueChanged(object sender, EventArgs e)
        {
            CameraRotationYLbl.Text = (sender as TrackBar)?.Value.ToString() ?? "0";
        }
    }
}