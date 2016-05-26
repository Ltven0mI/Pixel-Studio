using Pixel_Studio.Controls;
using Pixel_Studio.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
                if (canvas != null)
                    canvas.SetProjectHandler(this);
                if (value != null)
                    value.SetProjectHandler(this);
                canvas = value;
            }
        }

        private ProjectView projectView;
        public ProjectView ProjectView
        {
            get { return projectView; }
            set
            {
                if (projectView != null)
                    projectView.SetProjectHandler(null);
                if (value != null)
                    value.SetProjectHandler(this);
                projectView = value;
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


        // Events //
        public event EventHandler<ProjectEventArgs> ActiveProjectChanged;
        public event EventHandler<ProjectEventArgs> ProjectAdded;
        public event EventHandler<ProjectEventArgs> ProjectRemoved;


        public ProjectHandler()
        {
            InitializeComponent();
            Projects = new List<Project>();
            Tools = new List<Tool>();
            AddTool(new Pencil());
        }


        public void Undo()
        {
            ActiveProject?.History.Undo();
            Canvas.Invalidate();
        }

        public void Redo()
        {
            ActiveProject?.History.Redo();
            Canvas.Invalidate();
        }


        public void Redraw()
        {
            if (canvas != null)
                canvas.Invalidate();
            if (projectView != null)
                projectView.Invalidate();
        }


        private void UpdateProjectIndices()
        {
            for (int i=0; i < Projects.Count; i++)
            {
                Projects[i].Index = i;
            }
        }


        // Project Methods //
        public void SetActiveProject(Project project)
        {
            if (Projects.Contains(project))
            {
                if (ActiveProject != null)
                    ActiveProject.IsActive = false;
                ActiveProject = project;
                if (ActiveProject != null)
                    ActiveProject.IsActive = true;
                ActiveProjectChanged?.Invoke(this, new ProjectEventArgs(project));
                Redraw();
            }
        }

        public void SetActiveProject(int index)
        {
            if (index >= 0 && index < Projects.Count)
            {
                Project project = Projects[index];
                if (ActiveProject != null)
                    ActiveProject.IsActive = false;
                ActiveProject = project;
                if (ActiveProject != null)
                    ActiveProject.IsActive = true;
                ActiveProjectChanged?.Invoke(this, new ProjectEventArgs(project));
                Redraw();
            }
                SetActiveProject(Projects[index]);
        }

        public void AddProject(Project project)
        {
            project.ProjectHandler = this;
            project.Index = Projects.Count;
            Projects.Add(project);
            SetActiveProject(project);
            ProjectAdded?.Invoke(this, new ProjectEventArgs(project));
        }

        public void RemoveProject(Project project)
        {
            if (Projects.Contains(project))
            {
                if (project == ActiveProject)
                {
                    if (project.Index > 0)
                    {
                        if (project.Index < Projects.Count - 1)
                            SetActiveProject(Projects[project.Index + 1]);
                        else
                            SetActiveProject(Projects[project.Index - 1]);
                    }
                    else
                    {
                        SetActiveProject(null);
                        if (Projects.Count > 1)
                            SetActiveProject(Projects[1]);
                    }
                }

                project.Index = -1;
                Projects.Remove(project);
                UpdateProjectIndices();

                if (Projects.Count == 0)
                    ActiveProject = null;

                ProjectRemoved?.Invoke(this, new ProjectEventArgs(project));
                Redraw();
            }
        }

        public void MoveProject(Project project, int index)
        {
            if (Projects.Contains(project) && index >= 0 && index < Projects.Count)
            {
                Projects.Remove(project);
                Projects.Insert(index, project);
                UpdateProjectIndices();
                Redraw();
            }
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


        // Event Args //
        public class ProjectEventArgs : EventArgs
        {
            public Project Project { get; private set; }

            public ProjectEventArgs(Project project)
            {
                Project = project;
            }
        }
    }
}
