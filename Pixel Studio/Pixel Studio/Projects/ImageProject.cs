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
        private Bitmap Image;


        public ImageProject()
        {
            Image = new Bitmap(16, 16);
            using (Graphics g = Graphics.FromImage(Image))
            {
                g.FillRectangle(new SolidBrush(Color.Blue), 0, 0, 16, 16);
            }
        }


        public Bitmap GetImage()
        {
            return Image;
        }
    }
}
