using Pixel_Studio.Controls;
using Pixel_Studio.Tools;
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


        // Tools //
        [Browsable(false)]
        public List<Tool> Tools { get; private set; }
        [Browsable(false)]
        public Tool ActiveTool { get; private set; }


        public ProjectHandler()
        {
            InitializeComponent();
            Projects = new List<Project>();
            Tools = new List<Tool>();
            AddTool(new Pencil());
        }


        public void Redraw()
        {
            if (canvas != null)
                canvas.Invalidate();
        }


        // Project Methods //
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


        // Tool Methods //
        public void SetActiveTool(Tool tool)
        {
            if (Tools.Contains(tool))
            {
                if (ActiveTool != null)
                    ActiveTool.IsActive = false;
                ActiveTool = tool;
                tool.IsActive = true;
                Redraw();
            }
        }

        public void AddTool(Tool tool)
        {
            tool.ProjectHandler = this;
            Tools.Add(tool);
            SetActiveTool(tool);
        }
    }
}
