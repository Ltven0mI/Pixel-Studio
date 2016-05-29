using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pixel_Studio.Components;

namespace Pixel_Studio.Controls
{
    public abstract partial class ProjectControl : UserControl
    {
        [Browsable(false)]
        public ProjectHandler ProjectHandler { get; private set; }
        [Browsable(false)]
        public List<Project> Projects { get { return ProjectHandler?.Projects; } }
        [Browsable(false)]
        public Project ActiveProject { get { return ProjectHandler?.ActiveProject; } }
        [Browsable(false)]
        public bool ContainsMouse { get; private set; }

        // Event Handlers //
        public event EventHandler ContainsMouseChanged;


        public ProjectControl()
        {
            InitializeComponent();
        }


        public void SetProjectHandler(ProjectHandler projectHandler)
        {
            if (ProjectHandler != null)
            {
                ProjectHandler.ProjectAdded -= OnProjectAdded;
                ProjectHandler.ProjectRemoved -= OnProjectRemoved;
            }
            if (projectHandler != null)
            {
                projectHandler.ProjectAdded += OnProjectAdded;
                projectHandler.ProjectRemoved += OnProjectRemoved;
            }
            ProjectHandler = projectHandler;
        }

        // Event Handler Methods //
        protected virtual void OnProjectAdded(object sender, ProjectHandler.ProjectEventArgs e) { }
        protected virtual void OnProjectRemoved(object sender, ProjectHandler.ProjectEventArgs e) { }


        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            ContainsMouse = true;
            ContainsMouseChanged?.Invoke(this, new EventArgs());
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            ContainsMouse = false;
            ContainsMouseChanged?.Invoke(this, new EventArgs());
        }
    }
}
