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
using System.Drawing.Drawing2D;

namespace Pixel_Studio.Controls
{
    public partial class ProjectView : UserControl
    {
        [Browsable(false)]
        private ProjectHandler projectHandler;
        public ProjectHandler ProjectHandler
        {
            get { return projectHandler; }
            set
            {
                projectHandler = value;

                if (projectHandler != null)
                {
                    projectHandler.ProjectRemoved += ProjectHandler_ProjectRemoved;
                    projectHandler.ProjectAdded += ProjectHandler_ProjectAdded;
                }
                else
                {
                    projectHandler.ProjectRemoved -= ProjectHandler_ProjectRemoved;
                    projectHandler.ProjectAdded -= ProjectHandler_ProjectAdded;
                }
            }
        }

        private void ProjectHandler_ProjectAdded(object sender, ProjectHandler.ProjectEventArgs e)
        {
            UpdateVisibleProjectCount();
        }

        private void ProjectHandler_ProjectRemoved(object sender, ProjectHandler.ProjectEventArgs e)
        {
            UpdateVisibleProjectCount();
        }

        private List<Project> Projects { get { return ProjectHandler?.Projects; } }
        public Project ActiveProject { get { return ProjectHandler?.ActiveProject; } }
        public Project ActiveCloseProject { get; private set; }
        public Project FocusedProject { get; private set; }
        public Project FocusedCloseProject { get; private set; }


        public int TabWidth { get; set; } = 110;
        public int BottomLine { get; set; } = 3;

        public StringFormat StringFormat;


        private bool LeftDown;

        private int VisibleProjectCount;


        public ProjectView()
        {
            InitializeComponent();
            DoubleBuffered = true;

            StringFormat = new StringFormat();
            StringFormat.Alignment = StringAlignment.Near;
            StringFormat.LineAlignment = StringAlignment.Center;
            StringFormat.Trimming = StringTrimming.None;
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (ProjectHandler != null)
            {
                //e.Graphics.SmoothingMode = SmoothingMode.None;
                //e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                for (int i = 0; i < Math.Min(ProjectHandler.Projects.Count, VisibleProjectCount); i++)
                {
                    Project project = ProjectHandler.Projects[i];

                    Color tabColor = ThemeManager.ActiveTheme.BackColor;
                    Color textColor = ThemeManager.ActiveTheme.ForeColor;
                    Color closeColor = ThemeManager.ActiveTheme.BackColor;
                    if (project == ActiveProject)
                    {
                        tabColor = ThemeManager.ActiveTheme.TabActiveColor;
                        if (project == ActiveCloseProject && FocusedCloseProject != null)
                            closeColor = ThemeManager.ActiveTheme.TabFocusedCloseActiveColor;
                        else
                            closeColor = ThemeManager.ActiveTheme.TabFocusedCloseFocusedColor;
                    }
                    else if (project == FocusedProject)
                    {
                        tabColor = ThemeManager.ActiveTheme.TabFocusedColor;
                        if (project == ActiveCloseProject && project == FocusedCloseProject)
                            closeColor = ThemeManager.ActiveTheme.TabActiveCloseActiveColor;
                        else
                            closeColor = ThemeManager.ActiveTheme.TabFocusedCloseFocusedColor;
                    }

                    int closeBtnSize = Size.Height - BottomLine - 6;

                    project.TabRectangle = new Rectangle(i * TabWidth, 0, TabWidth, Size.Height - BottomLine);
                    project.TabCloseRectangle = new Rectangle(project.TabRectangle.X + project.TabRectangle.Width - closeBtnSize - 3, 3, closeBtnSize, closeBtnSize);
                    e.Graphics.FillRectangle(new SolidBrush(tabColor), project.TabRectangle);

                    if (project == FocusedCloseProject || project == ActiveCloseProject)
                        e.Graphics.FillRectangle(new SolidBrush(closeColor), project.TabCloseRectangle);

                    if (project == ActiveProject || project == FocusedProject)
                        e.Graphics.DrawImage(Properties.Resources.tab_close_active, project.TabCloseRectangle);

                    Rectangle strinRect = new Rectangle(project.TabRectangle.X, project.TabRectangle.Y, project.TabRectangle.Width - project.TabCloseRectangle.Width - 6, project.TabRectangle.Height);
                    e.Graphics.DrawString(project.Name, DefaultFont, new SolidBrush(textColor), strinRect, StringFormat);
                }

                //ContextMenuStrip projectContextMenu = new ContextMenuStrip();
                //foreach (Project project in Projects)
                //{
                //    projectContextMenu.Items.Add(project.Name);
                //}

                //projectContextMenu.Show(this, e.Location);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            e.Graphics.FillRectangle(new SolidBrush(ThemeManager.ActiveTheme.BackColor), e.ClipRectangle);
            e.Graphics.FillRectangle(new SolidBrush(ThemeManager.ActiveTheme.TabActiveColor), 0, Size.Height - BottomLine, Size.Width, BottomLine);
        }


        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                LeftDown = true;

                if (FocusedCloseProject != null)
                    ActiveCloseProject = FocusedCloseProject;
                else if (FocusedProject != null)
                    ProjectHandler.SetActiveProject(FocusedProject);
                Invalidate();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (ActiveProject != null)
            {
                int index = e.X / TabWidth;
                if (index < VisibleProjectCount)
                {
                    if (LeftDown)
                    {
                        if (index != ActiveProject.Index && ActiveCloseProject == null)
                            ProjectHandler.MoveProject(ActiveProject, index);
                    }

                    if (index >= 0 && index < Projects.Count)
                    {
                        if (!LeftDown && (FocusedProject != null && index != FocusedProject.Index || FocusedProject == null))
                            FocusedProject = Projects[index];
                        if ((!LeftDown || ActiveCloseProject != null && ActiveCloseProject == FocusedProject) && FocusedProject.TabCloseRectangle.Contains(e.X, e.Y))
                            FocusedCloseProject = FocusedProject;
                        else
                            FocusedCloseProject = null;
                        Invalidate();
                    }
                }
                else if (!LeftDown)
                {
                    FocusedProject = null;
                    FocusedCloseProject = null;
                    Invalidate();
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (e.Button == MouseButtons.Left)
            {
                LeftDown = false;
                if (FocusedCloseProject != null && ActiveCloseProject != null && FocusedCloseProject == ActiveCloseProject)
                    ProjectHandler.RemoveProject(ActiveCloseProject);
                FocusedProject = null;
                FocusedCloseProject = null;
                ActiveCloseProject = null;
                Invalidate();
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            FocusedProject = null;
            Invalidate();
        }


        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            UpdateVisibleProjectCount();
            Invalidate();
        }


        // Update Variables //
        private void UpdateVisibleProjectCount()
        {
            VisibleProjectCount = Size.Width / TabWidth;
        }
    }
}
