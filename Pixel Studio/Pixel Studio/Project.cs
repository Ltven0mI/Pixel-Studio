using Pixel_Studio.Projects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pixel_Studio
{
    public class Project
    {
        public enum ProjectType { Image, Animation, Tileset }

        public ProjectType projectType { get; private set; }
        public ProjectObject ProjectObject { get; private set; }

        public float OffsetX;
        public float OffsetY;
        public float Scale;

        public int DrawX { get; private set; }
        public int DrawY { get; private set; }
        public int DrawWidth { get; private set; }
        public int DrawHeight { get; private set; }


        public Project(ProjectType projectType)
        {
            this.projectType = projectType;
            switch (projectType)
            {
                case ProjectType.Image:
                    ProjectObject = new ImageProject();
                    break;
                case ProjectType.Animation:
                    break;
                case ProjectType.Tileset:
                    break;
            }

            Scale = 2;
        }


        public void Draw(PaintEventArgs e)
        {
            Bitmap image = ProjectObject.GetImage();
            DrawWidth = (int)(image.Width * Scale);
            DrawHeight = (int)(image.Height * Scale);
            DrawX = (int)((e.ClipRectangle.Width / 2f - DrawWidth / 2f) + OffsetX * Scale);
            DrawY = (int)((e.ClipRectangle.Height / 2f - DrawHeight / 2f) + OffsetY * Scale);

            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;

            e.Graphics.DrawImage(image, DrawX, DrawY, DrawWidth, DrawHeight);
        }
    }
}
