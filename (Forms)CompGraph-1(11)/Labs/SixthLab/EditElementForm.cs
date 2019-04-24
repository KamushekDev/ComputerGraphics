using System;
using System.Globalization;
using System.Windows.Forms;
using Newtonsoft.Json;
using _Forms_CompGraph_1_11_.Labs.SixthLab.Utils;

namespace _Forms_CompGraph_1_11_.Labs.SixthLab
{
    public partial class EditElementForm : Form
    {
        public IAddable ReturnedElement { get; private set; }

        public EditElementForm(IAddable element)
        {
            InitializeComponent();
            _elementType = element.GetType();
            ElementTB.Text = JsonConvert.SerializeObject(element, Formatting.Indented);
        }

        private void SaveElementBtn_Click(object sender, EventArgs e)
        {
            try
            {
                ReturnedElement = (IAddable) JsonConvert.DeserializeObject(ElementTB.Text, _elementType,
                    new JsonSerializerSettings
                    {
                        Culture = CultureInfo.CurrentCulture
                    });
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private Type _elementType;
    }
}