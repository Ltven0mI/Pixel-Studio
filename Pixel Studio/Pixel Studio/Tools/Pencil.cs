using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pixel_Studio.Tools
{
    public class Pencil : Tool
    {
        private Pen BorderPenFore;
        private Pen BorderPenBack;


        public Pencil() : base()
        {
            BorderPenFore = new Pen(Color.FromArgb(100, 255, 255, 255), 2);
            BorderPenFore.Alignment = PenAlignment.Outset;
            BorderPenFore.DashPattern = new float[] { 2, 2 };

            BorderPenBack = new Pen(Color.FromArgb(100, 0, 0, 0), 2);
            BorderPenBack.Alignment = PenAlignment.Outset;
        }


        public override void MouseDown(MouseButtons btn, int x, int y, Graphics g)
        {
            base.MouseDown(btn, x, y, g);
            if (btn == MouseButtons.Left)
            {
                g.FillRectangle(new SolidBrush(Color.Orange), x, y, 1, 1);
            }
        }

        public override void MouseDragged(MouseButtons btn, int x1, int y1, int x2, int y2, Graphics g)
        {
            base.MouseDragged(btn, x1, y1, x2, y2, g);
            if (btn == MouseButtons.Left)
                g.DrawLine(new Pen(Color.Orange), x1, y1, x2, y2);
        }

        public override void MouseUp(MouseButtons btn, int x, int y, Graphics g)
        {
            base.MouseUp(btn, x, y, g);
            if (btn == MouseButtons.Left)
                Canvas.ActiveProject.History.AddChange(new Project.ProjectHistory.GraphicalChange(new Bitmap(Canvas.ActiveProject.ProjectObject.GetImage()), 0, 0, 0));
        }

        public override void OnPaint(PaintEventArgs e, int x, int y)
        {
            base.OnPaint(e, x, y);
            if (Canvas.ActiveProject != null)
            {
                Project project = Canvas.ActiveProject;
                float scale = Canvas.ActiveProject.Scale;
                int drawX = (int)Math.Ceiling(project.DrawX + x * scale);
                int drawY = (int)Math.Ceiling(project.DrawY + y * scale);
                int drawSize = (int)Math.Floor(scale);
                e.Graphics.DrawRectangle(BorderPenBack, drawX, drawY, drawSize, drawSize);
                e.Graphics.DrawRectangle(BorderPenFore, drawX, drawY, drawSize, drawSize);
            }
        }
    }
}
