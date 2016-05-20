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
        public const float MIN_SCALE = 0.001f;

        public ProjectHandler ProjectHandler { get; set; }
        public Canvas Canvas { get { return ProjectHandler?.Canvas; } }

        public int Index { get; set; }

        public enum ProjectType { Image, Animation }

        public string Name { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

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
                if (scale < MIN_SCALE) scale = MIN_SCALE;
                OnScaleChanged(new EventArgs());
            }
        }


        private bool isActive;
        public bool IsActive
        {
            get { return isActive; }
            set
            {
                isActive = value;
                OnIsActiveChanged(new EventArgs());
            }
        }


        public int DrawX { get; private set; }
        public int DrawY { get; private set; }
        public int DrawWidth { get; private set; }
        public int DrawHeight { get; private set; }


        public Rectangle TabRectangle { get; set; }
        public Rectangle TabCloseRectangle { get; set; }


        public float LockedOffsetX { get; private set; }
        public float LockedOffsetY { get; private set; }


        private float OffsetXMin;
        private float OffsetXMax;
        private float OffsetYMin;
        private float OffsetYMax;


        // Events //
        public event EventHandler OffsetChanged;
        public event EventHandler ScaleChanged;
        public event EventHandler IsActiveChanged;


        public Project(ProjectType projectType, string name, int width, int height)
        {
            this.projectType = projectType;
            Name = name;
            Width = width;
            Height = height;
            switch (projectType)
            {
                case ProjectType.Image:
                    ProjectObject = new ImageProject(Width, Height);
                    break;
                case ProjectType.Animation:
                    break;
            }

            Scale = 8;
        }


        public void Draw(PaintEventArgs e)
        {
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;

            Bitmap image = ProjectObject.GetImage();
            e.Graphics.DrawImage(image, DrawX, DrawY, DrawWidth, DrawHeight);
        }


        public void LockOffset()
        {
            OffsetX = LockedOffsetX;
            OffsetY = LockedOffsetY;
        }


        // Value Updaters //
        public void UpdateOffsetBounds()
        {
            if (Canvas != null)
            {
                float halfDrawWidth = DrawWidth / 2f;
                float halfDrawHeight = DrawHeight / 2f;
                float halfCanvasWidth = Canvas.Size.Width / 2f;
                float halfCanvasHeight = Canvas.Size.Height / 2f;
                if (DrawWidth > Canvas.Width)
                {
                    OffsetXMin = -halfDrawWidth;
                    OffsetXMax = halfDrawWidth;
                }
                else
                {
                    OffsetXMin = -halfCanvasWidth;
                    OffsetXMax = halfCanvasWidth;
                }

                if (DrawHeight > Canvas.Height)
                {
                    OffsetYMin = -halfDrawHeight;
                    OffsetYMax = halfDrawHeight;
                }
                else
                {
                    OffsetYMin = -halfCanvasHeight;
                    OffsetYMax = halfCanvasHeight;
                }
            }
        }

        public void UpdateLockedOffset()
        {
            LockedOffsetX = OffsetX;
            LockedOffsetY = OffsetY;

            if (LockedOffsetX < OffsetXMin) LockedOffsetX = OffsetXMin;
            if (LockedOffsetX > OffsetXMax) LockedOffsetX = OffsetXMax;
            if (LockedOffsetY < OffsetYMin) LockedOffsetY = OffsetYMin;
            if (LockedOffsetY > OffsetYMax) LockedOffsetY = OffsetYMax;
        }

        public void UpdateDrawBounds()
        {
            if (Canvas != null)
            {
                Bitmap image = ProjectObject.GetImage();
                DrawWidth = (int)(image.Width * Scale);
                DrawHeight = (int)(image.Height * Scale);
                DrawX = (int)((Canvas.Size.Width / 2f - DrawWidth / 2f) + LockedOffsetX);
                DrawY = (int)((Canvas.Size.Height / 2f - DrawHeight / 2f) + LockedOffsetY);
            }
        }


        // Event Methods //
        protected virtual void OnOffsetChanged(EventArgs e)
        {
            UpdateOffsetBounds();
            UpdateLockedOffset();
            UpdateDrawBounds();

            OffsetChanged?.Invoke(this, e);
        }

        protected virtual void OnScaleChanged(EventArgs e)
        {
            UpdateOffsetBounds();
            UpdateLockedOffset();
            UpdateDrawBounds();

            ScaleChanged?.Invoke(this, e);
        }

        protected virtual void OnIsActiveChanged(EventArgs e)
        {
            IsActiveChanged?.Invoke(this, e);
            if (IsActive)
            {
                UpdateOffsetBounds();
                UpdateLockedOffset();
                UpdateDrawBounds();
            }
        }
    }
}
