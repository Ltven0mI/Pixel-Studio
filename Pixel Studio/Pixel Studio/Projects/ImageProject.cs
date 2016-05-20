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


        public ImageProject(int width, int height)
        {
            Image = ImageUtil.CreateBitmap(width, height, Color.White);
        }


        public Bitmap GetImage()
        {
            return Image;
        }
    }
}
