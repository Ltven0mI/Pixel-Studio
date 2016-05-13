using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pixel_Studio.Tools
{
    public class Pencil : Tool
    {
        public override void MouseDown(MouseButtons btn, int x, int y, Graphics g)
        {
            base.MouseDown(btn, x, y, g);
            if (btn == MouseButtons.Left)
                g.FillRectangle(new SolidBrush(Color.Orange), x, y, 1, 1);
        }

        public override void MouseDragged(MouseButtons btn, int x1, int y1, int x2, int y2, Graphics g)
        {
            base.MouseDragged(btn, x1, y1, x2, y2, g);
            if (btn == MouseButtons.Left)
                g.DrawLine(new Pen(Color.Orange), x1, y1, x2, y2);
        }
    }
}
