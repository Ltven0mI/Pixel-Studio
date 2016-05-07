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
            Projects.Add(new Project(Project.ProjectType.Image));
            ActiveProject = Projects[0];
        }
    }
}
