using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pixel_Studio.Projects;

namespace Pixel_Studio.Controls
{
    public partial class LayerView : ProjectControl
    {
        public int TabHeight { get; set; } = 25;


        public LayerView()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (ActiveProject != null)
            {
                switch (ActiveProject.projectType)
                {
                    case Project.ProjectType.Image:
                        DrawImageProject(e);
                        break;
                }
            }
        }

        private void DrawImageProject(PaintEventArgs e)
        {
            ImageProject imageProject = (ImageProject)ActiveProject.ProjectObject;
            for (int i=0; i<imageProject.Layers.Count; i++)
            {
                ImageLayer layer = imageProject.Layers[i];
                layer.TabRectangle = new Rectangle(0, (-i+imageProject.Layers.Count-1) * TabHeight, Size.Width / 2, TabHeight);
                e.Graphics.FillRectangle(new SolidBrush(Color.Red), layer.TabRectangle);
                e.Graphics.DrawString(layer.Name, DefaultFont, new SolidBrush(Color.White), layer.TabRectangle);
            }
        }
    }
}
