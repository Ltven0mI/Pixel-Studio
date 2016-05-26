using Pixel_Studio.Utilities;
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
        private int Width;
        private int Height;


        public ImageProject(int width, int height)
        {
            Image = ImageUtil.CreateBitmap(width, height, Color.FromArgb(0, 0, 0, 0));
            Width = width;
            Height = height;
        }


        public Bitmap GetImage()
        {
            return Image;
        }

        public void Revert()
        {
            Image = ImageUtil.CreateBitmap(Width, Height, Color.FromArgb(0, 0, 0, 0));
        }
    }
}
