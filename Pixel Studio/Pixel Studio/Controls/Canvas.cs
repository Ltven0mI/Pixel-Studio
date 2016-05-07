using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pixel_Studio.Controls
{
    public partial class Canvas : UserControl
    {
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
            new Project(Project.ProjectType.Image).Draw(e);
        }


        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
        }
    }
}
