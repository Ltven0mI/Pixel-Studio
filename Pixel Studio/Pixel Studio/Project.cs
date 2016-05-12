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

        private float relativeOffsetX;
        public float RelativeOffsetX
        {
            get { return relativeOffsetX; }
            set
            {
                relativeOffsetX = value;
                OnOffsetChanged(new EventArgs());
            }
        }

        private float relativeOffsetY;
        public float RelativeOffsetY
        {
            get { return relativeOffsetY; }
            set
            {
                relativeOffsetY = value;
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


        private float OffsetXMin;
        private float OffsetXMax;
        private float OffsetYMin;
        private float OffsetYMax;


        // Events //
        public event EventHandler OffsetChanged;
        public event EventHandler ScaleChanged;
        public event EventHandler IsActiveChanged;


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
            Bitmap image = ProjectObject.GetImage();
            DrawWidth = (int)(image.Width * Scale);
            DrawHeight = (int)(image.Height * Scale);
            DrawX = (int)((e.ClipRectangle.Width / 2f - DrawWidth / 2f) + OffsetX);
            DrawY = (int)((e.ClipRectangle.Height / 2f - DrawHeight / 2f) + OffsetY);

            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;

            e.Graphics.DrawImage(image, DrawX, DrawY, DrawWidth, DrawHeight);
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

        public void UpdateOffsetLock()
        {
            if (OffsetX < OffsetXMin) offsetX = OffsetXMin;
            if (OffsetX > OffsetXMax) offsetX = OffsetXMax;
            if (OffsetY < OffsetYMin) offsetY = OffsetYMin;
            if (OffsetY > OffsetYMax) offsetY = OffsetYMax;
        }


        // Event Methods //
        protected virtual void OnOffsetChanged(EventArgs e)
        {
            UpdateOffsetBounds();
            UpdateOffsetLock();

            OffsetChanged?.Invoke(this, e);
        }

        protected virtual void OnScaleChanged(EventArgs e)
        {
            UpdateOffsetBounds();
            UpdateOffsetLock();

            ScaleChanged?.Invoke(this, e);
        }

        protected virtual void OnIsActiveChanged(EventArgs e)
        {
            IsActiveChanged?.Invoke(this, e);
            if (IsActive) UpdateOffsetBounds();
            System.Diagnostics.Debug.WriteLine("Set ISsActive " + IsActive + " " + (Canvas != null) + " " + (ProjectHandler != null));
        }
    }
}
