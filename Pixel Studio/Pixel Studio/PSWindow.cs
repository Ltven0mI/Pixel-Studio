using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pixel_Studio
{
    public partial class PSWindow : Form
    {
        public PSWindow()
        {
            InitializeComponent();
        }

        private void PSWindow_Load(object sender, EventArgs e)
        {
            BackColor = ThemeManager.ActiveTheme.BackColor;
            ForeColor = ThemeManager.ActiveTheme.ForeColor;
        }
    }
}
