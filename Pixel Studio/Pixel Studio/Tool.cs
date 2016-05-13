using Pixel_Studio.Components;
using Pixel_Studio.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pixel_Studio
{
    public abstract class Tool
    {
        public ProjectHandler ProjectHandler { get; set; }
        protected Canvas Canvas { get { return ProjectHandler?.Canvas; } }


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


        // Events //
        public event EventHandler IsActiveChanged;


        // Callbacks //
        public virtual void MouseDown(MouseButtons btn, int x, int y, Graphics g) { }
        public virtual void MouseDragged(MouseButtons btn, int x1, int y1, int x2, int y2, Graphics g) { }
        public virtual void MouseUp(MouseButtons btn, int x, int y, Graphics g) { }

        public virtual void OnPaint(PaintEventArgs e) { }


        // Event Methods //
        protected virtual void OnIsActiveChanged(EventArgs e)
        {
            IsActiveChanged?.Invoke(this, e);
        }
    }
}
