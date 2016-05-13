using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pixel_Studio.Tools
{
    public class Pencil : Tool
    {
        public override void MouseDown(MouseButtons btn, int x, int y)
        {
            base.MouseDown(btn, x, y);
            System.Diagnostics.Debug.WriteLine("Mouse Down, X:" + x + ", Y:" + y);
        }
    }
}
