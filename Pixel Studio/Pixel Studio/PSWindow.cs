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

            ThemeManager.ThemeLoaded += ThemeManager_ThemeLoaded;
            OnLoadedTheme();
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openThemeDialog.ShowDialog(this) == DialogResult.OK)
            {
                System.Diagnostics.Debug.WriteLine(openThemeDialog.FileName);
                ThemeManager.LoadTheme(openThemeDialog.FileName);
            }
        }


        // Theme Loading //
        private void OnLoadedTheme()
        {
            BackColor = ThemeManager.ActiveTheme.BackColor;
            ForeColor = ThemeManager.ActiveTheme.ForeColor;
        }

        private void ThemeManager_ThemeLoaded(object sender, EventArgs e)
        {
            OnLoadedTheme();
        }
    }
}
