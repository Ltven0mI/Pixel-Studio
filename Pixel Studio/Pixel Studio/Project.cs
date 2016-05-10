using Pixel_Studio.Components;
using Pixel_Studio.Controls;
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
        public ProjectHandler ProjectHandler { get; set; }
        public Canvas Canvas { get { if (ProjectHandler != null) return ProjectHandler.Canvas; else return null; } }

        public enum ProjectType { Image, Animation, Tileset }

        public ProjectType projectType { get; private set; }
        public ProjectObject ProjectObject { get; private set; }


        private float offsetX;
        public float OffsetX
        {
            get { return offsetX; }
            set
            {
                offsetX = value;
                OnOffsetChanged(new EventArgs());
            }
        }

        private float offsetY;
        public float OffsetY
        {
            get { return offsetY; }
            set
            {
                offsetY = value;
                OnOffsetChanged(new EventArgs());
            }
        }

        private float scale;
        public float Scale
        {
            get { return scale; }
            set
            {
                scale = value;
                OnScaleChanged(new EventArgs());
            }
        }


        public int DrawX { get; private set; }
        public int DrawY { get; private set; }
        public int DrawWidth { get; private set; }
        public int DrawHeight { get; private set; }


        // Events //
        public event EventHandler OffsetChanged;
        public event EventHandler ScaleChanged;


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

            Scale = 8;
        }


        public void Draw(PaintEventArgs e)
        {
            float lockedOffX = offsetX;
            float lockedOffY = offsetY;

            Bitmap image = ProjectObject.GetImage();
            DrawWidth = (int)(image.Width * Scale);
            DrawHeight = (int)(image.Height * Scale);
            DrawX = (int)((e.ClipRectangle.Width / 2f - DrawWidth / 2f) + OffsetX * Scale);
            DrawY = (int)((e.ClipRectangle.Height / 2f - DrawHeight / 2f) + OffsetY * Scale);

            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;

            e.Graphics.DrawImage(image, DrawX, DrawY, DrawWidth, DrawHeight);
        }


        // Event Methods //
        protected virtual void OnOffsetChanged(EventArgs e)
        {
            if (Canvas != null)
            {
                float halfWidth = DrawWidth / 2f;
                float halfHeight = DrawHeight / 2f;
                if (DrawWidth > Canvas.Width)
                {
                    if (offsetX * scale - halfWidth > 0)
                    {
                        offsetX = halfWidth / scale;
                    }
                    else if (offsetX * scale + halfWidth < 0)
                    {
                        offsetX = -halfWidth / scale;
                    }
                }
                else
                {
                    if (offsetX * scale - Canvas.Width / 2f > 0)
                    {
                        offsetX = (Canvas.Width / 2f) / scale;
                    }
                    else if (offsetX * scale + Canvas.Width / 2f < 0)
                    {
                        offsetX = -(Canvas.Width / 2f) / scale;
                    }
                }

                if (DrawHeight > Canvas.Height)
                {
                    if (offsetY * scale - halfHeight > 0)
                    {
                        offsetY = halfHeight / scale;
                    }
                    else if (offsetY * scale + halfHeight < 0)
                    {
                        offsetY = -halfHeight / scale;
                    }
                }
                else
                {
                    if (offsetY * scale - Canvas.Height / 2f > 0)
                    {
                        offsetY = (Canvas.Height / 2f) / scale;
                    }
                    else if (offsetY * scale + Canvas.Height / 2f < 0)
                    {
                        offsetY = -(Canvas.Height / 2f) / scale;
                    }
                }
            }

            OffsetChanged?.Invoke(this, e);
        }

        protected virtual void OnScaleChanged(EventArgs e)
        {
            ScaleChanged?.Invoke(this, e);
        }
    }
}
