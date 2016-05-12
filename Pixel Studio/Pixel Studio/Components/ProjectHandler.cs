using Pixel_Studio.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixel_Studio.Components
{
    public partial class ProjectHandler : Component
    {
        // Controls //
        private Canvas canvas;
        public Canvas Canvas
        {
            get { return canvas; }
            set
            {
                if (value != null)
                    value.ProjectHandler = this;
                else if (canvas != null)
                    canvas.ProjectHandler = null;
                canvas = value;
            }
        }


        // Projects //
        [Browsable(false)]
        public List<Project> Projects { get; private set; }
        [Browsable(false)]
        public Project ActiveProject { get; private set; }


        public ProjectHandler()
        {
            InitializeComponent();
            Projects = new List<Project>();
        }


        public void Redraw()
        {
            if (canvas != null)
                canvas.Invalidate();
        }


        public void SetActiveProject(Project project)
        {
            if (Projects.Contains(project))
            {
                if (ActiveProject != null)
                    ActiveProject.IsActive = false;
                ActiveProject = project;
                project.IsActive = true;
                Redraw();
            }
        }


        public void AddProject(Project project)
        {
            project.ProjectHandler = this;
            Projects.Add(project);
            SetActiveProject(project);
        }
    }
}
