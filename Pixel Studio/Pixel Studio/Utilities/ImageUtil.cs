using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixel_Studio.Utilities
{
    public static class ImageUtil
    {
        public static Bitmap CreateBitmap(int width, int height, Color fillColor)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
                g.FillRectangle(new SolidBrush(fillColor), 0, 0, width, height);
            return result;
        }
    }
}
