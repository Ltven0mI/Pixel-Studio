using Pixel_Studio.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pixel_Studio.Projects
{
    public class ImageProject : ProjectObject
    {
        public List<ImageLayer> Layers { get; private set; }
        public ImageLayer ActiveLayer { get; private set; }

        private int Width;
        private int Height;


        public ImageProject(int width, int height)
        {
            Width = width;
            Height = height;
            InitLayers();
        }


        public void Draw(PaintEventArgs e, int x, int y, int width, int height)
        {
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
            foreach (ImageLayer layer in Layers)
            {
                e.Graphics.DrawImage(layer.Image, x, y, width, height);
            }
        }


        public Bitmap GetImage()
        {
            return ActiveLayer.Image;
        }


        public void Revert()
        {
            InitLayers();
        }


        public void NewLayer(string name)
        {
            Layers.Add(new ImageLayer(name, Width, Height));
            ActiveLayer = Layers[Layers.Count-1];
        }


        private void InitLayers()
        {
            Layers = new List<ImageLayer>();
            Layers.Add(new ImageLayer("Background", Width, Height));
            ActiveLayer = Layers[0];
        }
    }


    public class ImageLayer
    {
        public Bitmap Image;
        public string Name;

        public ImageLayer(string name, int width, int height)
        {
            Name = name;
            Image = ImageUtil.CreateBitmap(width, height, Color.FromArgb(0, 255, 255, 255));
        }
    }
}
