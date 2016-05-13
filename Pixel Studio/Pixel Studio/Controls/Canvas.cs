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
        private Project ActiveProject { get { return ProjectHandler?.ActiveProject; } }
        private Tool ActiveTool { get { return ProjectHandler?.ActiveTool; } }


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
            if (ActiveProject != null)
                ActiveProject.Draw(e);
        }


        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (ActiveProject != null)
            {
                ActiveProject.UpdateOffsetBounds();
                ActiveProject.UpdateLockedOffset();
                ActiveProject.UpdateDrawBounds();
            }
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (ActiveTool != null)
            {
                System.Diagnostics.Debug.WriteLine("mouse button down");
                int projectX = (int)((e.X - ActiveProject.DrawX) / ActiveProject.Scale);
                int projectY = (int)((e.Y - ActiveProject.DrawY) / ActiveProject.Scale);
                ActiveTool.MouseDown(e.Button, projectX, projectY);
            }

            //if (e.Button == MouseButtons.Left)
            //{
            //    if (ActiveProject != null)
            //    {
            //        int projectX = (int)((e.X - ActiveProject.DrawX) / ActiveProject.Scale);
            //        int projectY = (int)((e.Y - ActiveProject.DrawY) / ActiveProject.Scale);
            //        ActiveProject.ProjectObject.GetImage().SetPixel(projectX, projectY, Color.Orange);
            //        Invalidate();
            //    }
            //}

            UpdateLastMousePos(e.X, e.Y);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (ActiveProject != null)
            {
                if (e.Button == MouseButtons.Middle)
                {
                    ActiveProject.OffsetX += (e.X - LastCanvasX);
                    ActiveProject.OffsetY += (e.Y - LastCanavsY);
                    Invalidate();
                }

                if (e.Button == MouseButtons.Left)
                {
                    int projectX = (int)((e.X - ActiveProject.DrawX) / ActiveProject.Scale);
                    int projectY = (int)((e.Y - ActiveProject.DrawY) / ActiveProject.Scale);
                    if (projectX != LastProjectX || projectY != LastProjectY)
                    {
                        using (Graphics g = Graphics.FromImage(ActiveProject.ProjectObject.GetImage()))
                        {
                            g.DrawLine(new Pen(Color.Orange), LastProjectX, LastProjectY, projectX, projectY);
                        }
                        Invalidate();
                    }
                }
            }
                

            UpdateLastMousePos(e.X, e.Y);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (e.Button == MouseButtons.Middle)
            {
                if (ActiveProject != null)
                {
                    ActiveProject.LockOffset();
                    Invalidate();
                }
            }

            UpdateLastMousePos(e.X, e.Y);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (ActiveProject != null)
            {
                ActiveProject.Scale += ActiveProject.Scale * 0.1f * e.Delta / 120;
                Invalidate();
            }
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
