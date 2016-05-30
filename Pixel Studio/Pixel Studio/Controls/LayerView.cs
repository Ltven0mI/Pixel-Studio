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
        public int TabWidth { get; set; } = 400;
        public int TabHeight { get; set; } = 25;
        public int TabPadding { get; set; } = 2;


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
                Rectangle rect = new Rectangle(0, (-i + imageProject.Layers.Count) * (TabHeight + TabPadding), TabWidth, TabHeight);
                layer.TabRectangle = rect;

                Color topColor = ThemeManager.ActiveTheme.LayerButtonIdleTop;
                Color botColor = ThemeManager.ActiveTheme.LayerButtonIdleBot;
                Color altColor = ThemeManager.ActiveTheme.LayerButtonIdleAlt;

                if (imageProject.ActiveLayer == layer)
                {
                    topColor = ThemeManager.ActiveTheme.LayerButtonActiveTop;
                    botColor = ThemeManager.ActiveTheme.LayerButtonActiveBot;
                    altColor = ThemeManager.ActiveTheme.LayerButtonActiveAlt;
                }

                e.Graphics.FillRectangle(new SolidBrush(topColor), rect.X, rect.Y, rect.Width, rect.Height-3);
                e.Graphics.FillRectangle(new SolidBrush(botColor), rect.X, rect.Y + rect.Height - 2, rect.Width, 2);
                e.Graphics.FillRectangle(new SolidBrush(altColor), rect.X, rect.Y + rect.Height - 3, rect.Width, 1);
                e.Graphics.DrawString(layer.Name, DefaultFont, new SolidBrush(Color.White), layer.TabRectangle);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(ThemeManager.ActiveTheme.BackColor), e.ClipRectangle);
        }
    }
}
