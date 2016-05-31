using Pixel_Studio.Controls;
using Pixel_Studio.Dialogs;
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


        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (ModifierKeys.HasFlag(Keys.Control))
            {
                if (e.KeyCode == Keys.Z)
                {
                    if (ModifierKeys.HasFlag(Keys.Shift))
                        projectHandler.Redo();
                    else
                        projectHandler.Undo();
                    e.Handled = true;
                }
                if (e.KeyCode == Keys.Y)
                {
                    projectHandler.Redo();
                    e.Handled = true;
                }
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

        private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewProjectDialog dialog = new NewProjectDialog();
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                projectHandler.AddProject(new Project(dialog.ProjectName, dialog.ProjectWidth, dialog.ProjectHeight));
            }
        }

        private void newLayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (projectHandler != null)
            {
                projectHandler.NewLayer("test");
            }
        }
    }
}
