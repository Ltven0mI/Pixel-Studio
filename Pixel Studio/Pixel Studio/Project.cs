using Pixel_Studio.Components;
using Pixel_Studio.Controls;
using Pixel_Studio.Utilities;
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

        public string Name { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        // Layers and Frames //
        public List<ProjectLayer> Layers { get; private set; }
        public ProjectLayer ActiveLayer { get; private set; }
        public ProjectFrame ActiveFrame { get { return ActiveLayer?.ActiveFrame; } }

        public ProjectHistory History { get; private set; }


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


        public Project(string name, int width, int height)
        {
            Name = name;
            Width = width;
            Height = height;

            Scale = 8;
            InitLayers();
            History = new ProjectHistory(this);
        }


        public void Draw(PaintEventArgs e)
        {
            //e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            //e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;

            //Bitmap image = ProjectObject.GetImage();
            //e.Graphics.DrawImage(image, DrawX, DrawY, DrawWidth, DrawHeight);
            //ProjectObject.Draw(e, DrawX, DrawY, DrawWidth, DrawHeight);

            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
            foreach (ProjectLayer layer in Layers)
            {
                e.Graphics.DrawImage(layer.ActiveFrame.Image, DrawX, DrawY, DrawWidth, DrawHeight);
            }
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
                DrawWidth = (int)(Width * Scale);
                DrawHeight = (int)(Height * Scale);
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


        public void Revert()
        {
            InitLayers();
        }


        // Layer Methods //
        public void InitLayers()
        {
            Layers = new List<ProjectLayer>();
            NewLayer("Background");
        }

        public void SetActiveLayer(ProjectLayer layer)
        {
            if (Layers.Contains(layer))
                ActiveLayer = layer;
            else
                throw new ArgumentException("Layer does not exist in Project!");
        }

        public void NewLayer(string name)
        {
            ProjectLayer newLayer = new ProjectLayer(name, Width, Height);
            newLayer.Index = Layers.Count;
            Layers.Add(newLayer);
            SetActiveLayer(newLayer);
        }
    }


    public class ProjectLayer
    {
        public List<ProjectFrame> Frames { get; private set; }
        public ProjectFrame ActiveFrame { get; private set; }

        public string Name;
        private int Width;
        private int Height;

        public int Index;


        public ProjectLayer(string name, int width, int height)
        {
            Name = name;
            Width = width;
            Height = height;
            InitFrames();
        }


        public void InitFrames()
        {
            Frames = new List<ProjectFrame>();
            NewFrame();
        }


        public void SetActiveFrame(ProjectFrame frame)
        {
            if (Frames.Contains(frame))
                ActiveFrame = frame;
            else
                throw new ArgumentException("Frame does not exist in Layer!");
        }

        public void NewFrame()
        {
            ProjectFrame newFrame = new ProjectFrame(Width, Height);
            newFrame.Index = Frames.Count;
            Frames.Add(newFrame);
            SetActiveFrame(newFrame);
        }
    }


    public class ProjectFrame
    {
        public Bitmap Image { get; private set; }
        public int Index;

        public ProjectFrame(int width, int height)
        {
            Image = ImageUtil.CreateBitmap(width, height, Color.FromArgb(0, 255, 255, 255));
        }
    }


    public class ProjectHistory
    {
        private Project Project;

        private List<Change> UndoPool;
        private List<Change> RedoPool;


        public ProjectHistory(Project project)
        {
            Project = project;
            UndoPool = new List<Change>();
            RedoPool = new List<Change>();
        }


        public void Undo()
        {
            if (UndoPool.Count > 0)
            {
                Change change = UndoPool[UndoPool.Count - 1];
                UndoPool.RemoveAt(UndoPool.Count - 1);
                RedoPool.Add(change);

                if (UndoPool.Count > 0)
                {
                    UndoPool[UndoPool.Count - 1].RevertTo(Project);
                }
                else
                {
                    Project.Revert();
                }
            }
        }

        public void Redo()
        {
            if (RedoPool.Count > 0)
            {
                Change change = RedoPool[RedoPool.Count - 1];
                RedoPool.RemoveAt(RedoPool.Count - 1);
                UndoPool.Add(change);
                change.RevertTo(Project);
            }
        }


        public void AddChange(Change change)
        {
            UndoPool.Add(change);
            RedoPool.Clear();
        }


        // Change Types //
        public interface Change
        {
            void RevertTo(Project project);
        }

        // Graphical Change //
        public class GraphicalChange : Change
        {
            public Bitmap Image;
            public int X { get; private set; }
            public int Y { get; private set; }

            public int LayerIndex { get; private set; }
            public int FrameIndex { get; private set; }


            public GraphicalChange(Bitmap image, int x, int y, int layerIndex, int frameIndex)
            {
                Image = image;
                X = x;
                Y = y;
                LayerIndex = layerIndex;
                FrameIndex = frameIndex;
            }

            public void RevertTo(Project project)
            {
                using (Graphics g = Graphics.FromImage(project.Layers[LayerIndex].Frames[FrameIndex].Image))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
                    g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
                    g.DrawImage(Image, X, Y);
                }
            }
        }
    }
}
