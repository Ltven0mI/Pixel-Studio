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
        }


        public void Draw(PaintEventArgs e)
        {
            DrawX = 10;
            DrawY = 100;
            DrawWidth = 300;
            DrawHeight = 200;

            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;

            e.Graphics.DrawRectangle(new Pen(Color.Red), DrawX, DrawY, DrawWidth, DrawHeight);
            e.Graphics.DrawImage(ProjectObject.GetImage(), DrawX, DrawY, DrawWidth, DrawHeight);
        }
    }
}
