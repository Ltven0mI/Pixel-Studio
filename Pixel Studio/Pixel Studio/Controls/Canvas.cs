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


        // Mouse Positions //
        public int LastCanvasX { get; private set; }
        public int LastCanavsY { get; private set; }

        public int LastProjectX { get; private set; }
        public int LastProjectY { get; private set; }


        public Canvas()
        {
            InitializeComponent();
            DoubleBuffered = true;

            ThemeManager.ThemeLoaded += ThemeManager_ThemeLoaded;
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            ActiveProject.Draw(e);
        }


        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            UpdateLastMousePos(e.X, e.Y);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.Button == MouseButtons.Middle)
            {
                if (ActiveProject != null)
                {
                    ActiveProject.OffsetX += (e.X - LastCanvasX) / ActiveProject.Scale;
                    ActiveProject.OffsetY += (e.Y - LastCanavsY) / ActiveProject.Scale;
                    Invalidate();
                }
            }

            UpdateLastMousePos(e.X, e.Y);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            UpdateLastMousePos(e.X, e.Y);
        }

        private void UpdateLastMousePos(int x, int y)
        {
            LastCanvasX = x;
            LastCanavsY = y;
            if (ActiveProject != null)
            {
                LastProjectX = (int)((x - ActiveProject.DrawX) / ActiveProject.Scale);
                LastProjectY = (int)((y - ActiveProject.DrawY) / ActiveProject.Scale);
            }
            System.Diagnostics.Debug.WriteLine(LastCanvasX + " " + LastCanavsY + " " + LastProjectX + " " + LastProjectY);
        }


        // Theme Loading //
        private void OnLoadedTheme()
        {
            BackColor = ThemeManager.ActiveTheme.CanvasBackColor;
        }

        private void ThemeManager_ThemeLoaded(object sender, EventArgs e)
        {
            OnLoadedTheme();
        }
    }
}
