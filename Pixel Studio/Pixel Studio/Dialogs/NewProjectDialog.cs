using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pixel_Studio.Dialogs
{
    public partial class NewProjectDialog : Form
    {
        public string ProjectName { get { return nameTextBox.Text; } }
        public int ProjectWidth { get { return (int)widthInput.Value; } }
        public int ProjectHeight { get { return (int)heightInput.Value; } }


        public NewProjectDialog()
        {
            InitializeComponent();
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            if (ProjectName.Length > 0)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show(this, "Name cannot be empty!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
