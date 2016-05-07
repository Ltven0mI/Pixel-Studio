using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixel_Studio.Projects
{
    public class ImageProject : ProjectObject
    {
        public Bitmap GetImage()
        {
            Bitmap result = new Bitmap(100, 100);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.FillRectangle(new SolidBrush(Color.Blue), 0, 0, 100, 100);
            }
            return result;
        }
    }
}
