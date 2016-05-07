using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pixel_Studio.Components;

namespace Pixel_Studio.Controls
{
    public partial class Canvas : UserControl
    {
        [Browsable(false)]
        public ProjectHandler ProjectHandler { get; set; }
        private Project ActiveProject { get { return ProjectHandler.ActiveProject; } }


        public Canvas()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }


        private void Canvas_Load(object sender, EventArgs e)
        {
            BackColor = ThemeManager.ActiveTheme.CanvasBackColor;
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            ActiveProject.Draw(e);
        }
    }
}
